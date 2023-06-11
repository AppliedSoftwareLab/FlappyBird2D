using System;
using System.IO;
using System.Text.RegularExpressions;
using SDL2;
using System.Collections.Generic;

/**
 * @brief ��Ʈ �ؽ�ó ��Ʋ�� �� ���� �����Դϴ�.
 */
class Glyph
{
    public int codePoint = 0;
    public int x0 = 0;
    public int y0 = 0;
    public int x1 = 0;
    public int y1 = 0;
    public float xoffset = 0.0f;
    public float yoffset = 0.0f;
    public float xoffset2 = 0.0f;
    public float yoffset2 = 0.0f;
    public float xadvance = 0.0f;
}


/**
 * @brief Ʈ�� Ÿ�� ��Ʈ�� ���� ���� �� �ؽ�ó ��Ʋ�󽺸� �����մϴ�.
 */
class TTFont : IContent
{
    /**
     * @brief Ʈ�� Ÿ�� ��Ʈ�� �۸��� ������ �ؽ�ó ��Ʋ�󽺸� �ε��մϴ�.
     * 
     * @param glyphPath �۸��� ������ ������ ini ���� ����Դϴ�.
     * @param textureAtlasPath Ʈ�� Ÿ�� ��Ʈ�� �ؽ�ó ��Ʋ�� ����Դϴ�.
     * 
     * @thorws
     * - �۸��� ���� ������ ini ������ �ƴ϶�� ���ܸ� �����ϴ�.
     * - �۸��� ���� ������ ��ȿ���� ������ ���ܸ� �����ϴ�.
     * - Ʈ�� Ÿ�� ��Ʈ�� �ؽ�ó ��Ʋ�� ���ҽ� �ε��� �����ϸ� ���ܸ� �����ϴ�.
     * - Ʈ�� Ÿ�� ��Ʈ�� �ؽ�ó ��Ʋ�� ���ҽ� ������ �����ϸ� ���ܸ� �����ϴ�.
     */
    public TTFont(string glyphPath, string textureAtlasPath)
    {
        ParseGlyphInfo(glyphPath);
        LoadTextureAtlas(textureAtlasPath);
    }


    /**
     * @brief Ʈ�� Ÿ�� ��Ʈ �Ӽ��� ���� ���� Getter/Setter �Դϴ�.
     */
    public IntPtr Resource
    {
        get => textureAtlas_;
    }


    /**
     * @brief Ʈ�� Ÿ�� ��Ʈ�� �����͸� ��������� �����մϴ�.
     */
    public void Release()
    {
        if (textureAtlas_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(textureAtlas_);
        }
    }


    /**
     * @brief �ؽ�Ʈ�� ũ�⸦ �����մϴ�.
     * 
     * @param text ������ �ؽ�Ʈ�� ũ���Դϴ�.
     * @param outWidth[out] ��ü �ؽ�Ʈ�� ���� ũ���Դϴ�.
     * @param outHeight[out] ��ü �ؽ�Ʈ�� ���� ũ���Դϴ�.
     * 
     * @throws ��ȿ���� ���� �ؽ�Ʈ�� ũ�⸦ �����ϸ� ���ܸ� �����ϴ�.
     */
    public void MeasureText(string text, out int outWidth, out int outHeight)
    {
        if(!IsValidText(text))
        {
            throw new Exception("invalid text in font...");
        }

        int textWidth = 0;
        int textHeight = -1;

        char[] characters = text.ToCharArray();

        foreach (char character in characters)
        {
            int currentWidth = (int)(glyphs_[character].xadvance);
            int currentHeight = glyphs_[character].y1 - glyphs_[character].y0;

            textWidth += currentWidth;

            if (currentHeight > textHeight)
            {
                textHeight = currentHeight;
            }
        }

        outWidth = textWidth;
        outHeight = textHeight;
    }


    /**
     * @brief �ؽ�Ʈ�� ��ȿ���� �˻��մϴ�.
     * 
     * @param text �˻縦 ������ �ؽ�Ʈ�Դϴ�.
     * 
     * @return �ؽ�Ʈ�� ��ȿ�ϴٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    public bool IsValidText(string text)
    {
        char[] characters = text.ToCharArray();

        foreach(char character in characters)
        {
            if(!glyphs_.ContainsKey(character))
            {
                return false;
            }
        }

        return true;
    }


    /**
     * @brief �۸��� ������ ����ϴ�.
     * 
     * @param character �۸��� ������ ���� �����Դϴ�.
     * 
     * @return ���ڿ� �����ϴ� �۸����� ��ȯ�մϴ�.
     */
    public Glyph GetGlyph(char character)
    {
        if (!glyphs_.ContainsKey(character))
        {
            throw new Exception("invalid character in font...");
        }

        return glyphs_[character];
    }


    /**
     * @brief �۸��� ������ ������ ini ������ �Ľ��մϴ�.
     * 
     * @param glyphPath �۸��� ������ ������ ini ���� ����Դϴ�.
     * 
     * @throws
     * - �۸��� ���� ������ ini ������ �ƴ϶�� ���ܸ� �����ϴ�.
     * - �۸��� ���� ������ ��ȿ���� ������ ���ܸ� �����ϴ�.
     */
    private void ParseGlyphInfo(string glyphPath)
    {
        string glyphFileContent = File.ReadAllText(glyphPath);
        Regex sectionRegex = new Regex(@"\[(.*?)\](.*?)(?=\[|\z)", RegexOptions.Singleline);

        MatchCollection sectionMatches = sectionRegex.Matches(glyphFileContent);

        foreach (Match sectionMatche in sectionMatches)
        {
            string section = sectionMatche.Groups[1].Value.Trim();
            string context = sectionMatche.Groups[2].Value.Trim();

            string[] lines = context.Split(new[] { "\r\n" }, StringSplitOptions.None);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                string[] keyValue = line.Split('=');
                keyValues.Add(keyValue[0], keyValue[1]);
            }

            if (Int32.TryParse(section, out int codePoint))
            {
                glyphs_.Add((char)(codePoint), new Glyph());

                glyphs_[(char)(codePoint)].codePoint = codePoint;
                glyphs_[(char)(codePoint)].x0 = Int32.Parse(keyValues["x0"]);
                glyphs_[(char)(codePoint)].y0 = Int32.Parse(keyValues["y0"]);
                glyphs_[(char)(codePoint)].x1 = Int32.Parse(keyValues["x1"]);
                glyphs_[(char)(codePoint)].y1 = Int32.Parse(keyValues["y1"]);
                glyphs_[(char)(codePoint)].xoffset = float.Parse(keyValues["xoffset"]);
                glyphs_[(char)(codePoint)].yoffset = float.Parse(keyValues["yoffset"]);
                glyphs_[(char)(codePoint)].xoffset2 = float.Parse(keyValues["xoffset2"]);
                glyphs_[(char)(codePoint)].yoffset2 = float.Parse(keyValues["yoffset2"]);
                glyphs_[(char)(codePoint)].xadvance = float.Parse(keyValues["xadvance"]);
            }
            else
            {
                if (section.Equals("Info"))
                {
                    beginCodePoint_ = Int32.Parse(keyValues["BeginCodePoint"]);
                    endCodePoint_ = Int32.Parse(keyValues["EndCodePoint"]);
                    textureAtlasSize_ = Int32.Parse(keyValues["BitmapSize"]);
                    fontSize_ = float.Parse(keyValues["FontSize"]);
                }
            }
        }
    }


    /**
     * @brief Ʈ�� Ÿ�� ��Ʈ�� �ؽ�ó ��Ʋ�󽺸� �ε��մϴ�.
     * 
     * @param textureAtlasPath Ʈ�� Ÿ�� ��Ʈ�� �ؽ�ó ��Ʋ�� ����Դϴ�.
     */
    private void LoadTextureAtlas(string textureAtlasPath)
    {
        IntPtr textureAtlasSurface = SDL_image.IMG_Load(textureAtlasPath);

        if (textureAtlasSurface == IntPtr.Zero)
        {
            throw new Exception("failed to load font texture atlas image...");
        }

        textureAtlas_ = SDL.SDL_CreateTextureFromSurface(RenderManager.Get().GetRendererPtr(), textureAtlasSurface);
        SDL.SDL_FreeSurface(textureAtlasSurface);

        if (textureAtlas_ == IntPtr.Zero)
        {
            throw new Exception("failed to create font texture atlas resource...");
        }
    }


    /**
     * @brief �ڵ� ����Ʈ�� �������Դϴ�.
     */
    private int beginCodePoint_ = 0;


    /**
     * @brief �ڵ� ����Ʈ�� �����Դϴ�.
     */
    private int endCodePoint_ = 0;


    /**
     * @brief ������ �۸��� �����Դϴ�.
     */
    private Dictionary<char, Glyph> glyphs_ = new Dictionary<char, Glyph>();


    /**
     * @brief �ؽ�ó ��Ʋ���� ũ���Դϴ�.
     * 
     * @note �ؽ�ó ��Ʋ���� ���� ���� ũ��� �����մϴ�.
     */
    private int textureAtlasSize_ = 0;


    /**
     * @brief �ؽ�ó ��Ʋ���� ���ҽ��Դϴ�.
     */
    private IntPtr textureAtlas_;


    /**
     * @brief ��Ʈ�� ũ���Դϴ�.
     */
    private float fontSize_ = 0.0f;
}