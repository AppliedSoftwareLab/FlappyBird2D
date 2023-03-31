using System;
using System.Collections.Generic;
using SDL2;


/**
 * @brief �ؽ�ó ��Ʋ�󽺿� ǥ�õ� ���� �����Դϴ�.
 */
struct Glyph
{
    public ushort codePoint;
    public int x;
    public int y;
    public int width;
    public int height;
    public int xoffset;
    public int yoffset;
    public int xadvance;
}


/**
 * @brief �ؽ�ó ��Ʋ���� ũ���Դϴ�.
 * 
 * @see https://docs.unrealengine.com/4.27/en-US/RenderingAndGraphics/Textures/SupportAndSettings/
 */
enum EResolution
{
    SIZE_16 = 16,
    SIZE_32 = 32,
    SIZE_64 = 64,
    SIZE_128 = 128,
    SIZE_256 = 256,
    SIZE_512 = 512,
    SIZE_1024 = 1024,
    SIZE_2048 = 2048,
    SIZE_4096 = 4096,
    SIZE_8192 = 8192,
}


/**
 * @brief Ʈ�� Ÿ�� ��Ʈ�� �ε��ϰ� ��Ʈ �ؽ�ó ��Ʋ�󽺸� �����մϴ�.
 * 
 * @note �ؽ�ó ��Ʋ���� ũ��� ���簢���Դϴ�.
 */
class TTFont : IContent
{
    /**
     * @brief Ʈ�� Ÿ�� ��Ʈ�� �ε��ϰ� ��Ʈ �ؽ�ó ��Ʋ�󽺸� �����ϴ� Ŭ������ �������Դϴ�.
     * 
     * @note �ؽ�ó ��Ʋ�󽺿� ǥ�õ� ���ڴ� ���� ���ڿ� �� ���ڸ� �����մϴ�.
     * 
     * @param path Ʈ�� Ÿ�� ��Ʈ ���ҽ��� ����Դϴ�.
     * @param beginCodePoint �ؽ�ó ��Ʋ�󽺿� ǥ���� ���� �����Դϴ�.
     * @param endCodePoint �ؽ�ó ��Ʋ�󽺿� ǥ���� �� �����Դϴ�.
     * @param size ��Ʈ�� ũ���Դϴ�.
     * 
     * @throws
     * - Ʈ�� Ÿ�� ��Ʈ ���� �ε��� �����ϸ� ���ܸ� �����ϴ�.
     * - ��Ʈ �ؽ�ó ��Ʋ�� ������ �����ϸ� ���ܸ� �����ϴ�.
     */
    public TTFont(string path, ushort beginCodePoint, ushort endCodePoint, int size)
    {
        beginCodePoint_ = beginCodePoint;
        endCodePoint_ = endCodePoint;

        IntPtr font = SDL_ttf.TTF_OpenFont(path, size);

        if(font == IntPtr.Zero)
        {
            throw new Exception("failed to open true type font...");
        }

        EResolution resolution = TryFindTextureAtlasSize(size, endCodePoint_ - beginCodePoint_ + 1);
        textureAtlas_ = CreateTextureAtlas(font, resolution);
        
        SDL_ttf.TTF_CloseFont(font);
    }


    /**
     * @brief Ʈ�� Ÿ�� ��Ʈ�� �ε��ϰ� ��Ʈ �ؽ�ó ��Ʋ�󽺸� �����ϴ� Ŭ������ Getter�Դϴ�.
     */
    public ushort BeginCodePoint
    {
        get => beginCodePoint_;
    }

    public ushort EndCodePoint
    {
        get => endCodePoint_;
    }

    public IntPtr TextureAtlas
    {
        get => textureAtlas_;
    }


    /**
     * @brief �ش� �ڵ� ����Ʈ�� �����ϰ� �ִ��� �˻��մϴ�.
     * 
     * @param codePoint �˻縦 ������ �ڵ� ����Ʈ�Դϴ�.
     * 
     * @return ��Ʈ�� �Է� ���� �ڵ� ����Ʈ�� �����ϰ� �ִٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    public bool HaveCodePoint(ushort codePoint)
    {
        return (beginCodePoint_ <= codePoint && codePoint <= endCodePoint_);
    }


    /**
     * @brief �ڵ� ����Ʈ�� �����ϴ� �۸��� ������ ����ϴ�.
     * 
     * @param codePoint �۸��� ������ ���� �ڵ� ����Ʈ�Դϴ�.
     * 
     * @throws �ڵ� ����Ʈ�� �����ϴ� �۸��� ������ ������ ���ܸ� �����ϴ�.
     * 
     * @return �ڵ� ����Ʈ�� �����ϴ� �۸��� ������ ��ȯ�մϴ�.
     */
    public Glyph GetGlyph(ushort codePoint)
    {
        if(!HaveCodePoint(codePoint))
        {
            throw new Exception("can't find glyph info from code point...");
        }

        int index = (int)(codePoint - beginCodePoint_);
        return glyphs_[index];
    }


    /**
     * @brief ���ڿ��� ũ�⸦ �����մϴ�.
     * 
     * @param text ������ ���ڿ��Դϴ�.
     * @param width[out] ���ڿ��� ���� ũ���Դϴ�.
     * @param height[out] ���ڿ��� ���� ũ���Դϴ�.
     * @param padding ���ڿ� ������ �����Դϴ�. �⺻ ���� 2�Դϴ�.
     * 
     * @return ������ �����ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    public bool MeasureText(string text, out int width, out int height)
    {
        width = 0;
        height = -1;

        for(int index = 0; index < text.Length; ++index)
        {
            if(!HaveCodePoint((ushort)text[index]))
            {
                return false;
            }

            Glyph glyph = GetGlyph((ushort)text[index]);

            width += glyph.xadvance;
            height = Math.Max(height, glyph.height);
        }

        return true;
    }


    /**
     * @brief ������ �ؽ�ó ��Ʋ�� ���ҽ��� �����մϴ�.
     */
    public void Release()
    {
        if (textureAtlas_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(textureAtlas_);
        }
    }


    /**
     * @brief �ؽ�ó ��Ʋ�󽺸� ������ �� �ִ��� Ȯ���մϴ�.
     * 
     * @param fontSize ��Ʈ�� ũ���Դϴ�.
     * @param resolution �ؽ�ó ��Ʋ���� ũ���Դϴ�.
     * @param countCodePoint �ؽ�ó ��Ʋ�󽺿� ǥ���� �ڵ� ����Ʈ�� ���Դϴ�.
     */
    private bool CanGenerateTextureAtlas(int fontSize, EResolution resolution, int countCodePoint)
    {
        bool bCanGenerateTextureAtlas = true;

        int atlasX = 0, atlasY = 0;
        int atlasSize = (int)resolution;

        for (int count = 0; count < countCodePoint; ++count)
        {
            if(atlasY + fontSize > atlasSize)
            {
                bCanGenerateTextureAtlas = false;
                break;
            }

            if(atlasX + fontSize <= atlasSize)
            {
                atlasX += fontSize;
            }
            else
            {
                atlasX = 0;
                atlasY += fontSize;
            }
        }
        
        return bCanGenerateTextureAtlas;
    }


    /**
     * @brief �ؽ�ó ��Ʋ���� ũ�⸦ ã���ϴ�.
     * 
     * @param fontSize ��Ʈ�� ũ���Դϴ�.
     * @param countCodePoint �ؽ�ó ��Ʋ�󽺿� ǥ���� �ڵ� ����Ʈ�� ���Դϴ�.
     * 
     * @throws �ؽ�ó ��Ʋ���� ũ�⸦ ã�� �� �����ϸ� ���ܸ� �����ϴ�.
     */
    private EResolution TryFindTextureAtlasSize(int fontSize, int countCodePoint)
    {
        EResolution[] resolutions =
{
            EResolution.SIZE_16,
            EResolution.SIZE_32,
            EResolution.SIZE_64,
            EResolution.SIZE_128,
            EResolution.SIZE_256,
            EResolution.SIZE_512,
            EResolution.SIZE_1024,
            EResolution.SIZE_2048,
            EResolution.SIZE_4096,
            EResolution.SIZE_8192,
        };

        bool bIsFindResolution = false;
        EResolution targetResolution = EResolution.SIZE_16;

        foreach (EResolution resolution in resolutions)
        {
            if (CanGenerateTextureAtlas(fontSize, resolution, countCodePoint))
            {
                targetResolution = resolution;
                bIsFindResolution = true;
                break;
            }
        }

        if (!bIsFindResolution)
        {
            throw new Exception("failed to find texture atlas resolution...");
        }

        return targetResolution;
    }


    /**
     * @brief �ؽ�ó ��Ʋ�󽺸� �����մϴ�.
     * 
     * @param font Ʈ�� Ÿ�� ��Ʈ ���ҽ��Դϴ�.
     * @param resolution �ؽ�ó ��Ʋ���� ũ���Դϴ�.
     * 
     * @throws �ؽ�ó ��Ʋ�� ������ �����ϸ� ���ܸ� �����ϴ�.
     * 
     * @return �ؽ�ó ��Ʋ���� �����͸� ��ȯ�մϴ�.
     */
    private IntPtr CreateTextureAtlas(IntPtr font, EResolution resolution)
    {
        int atlasX = 0;
        int atlasY = 0;
        int atlasSize = (int)resolution;
        int maxRowHeight = 0;

        IntPtr atlasSurface = SDL.SDL_CreateRGBSurfaceWithFormat(0, atlasSize, atlasSize, 32, SDL.SDL_PIXELFORMAT_RGBA8888);
        if (atlasSurface == IntPtr.Zero)
        {
            throw new Exception("failed to create texture atlas surface...");
        }

        SDL.SDL_Color color;
        color.r = 255;
        color.g = 255;
        color.b = 255;
        color.a = 255;

        for (ushort codePoint = beginCodePoint_; codePoint <= endCodePoint_; ++codePoint)
        {
            if (SDL_ttf.TTF_GlyphMetrics(font, codePoint, out int minx, out int maxx, out int miny, out int maxy, out int advance) == -1)
            {
                throw new Exception("failed to get glyph info...");
            }

            if (atlasX + advance >= atlasSize)
            {
                atlasX = 0;
                atlasY += (maxRowHeight + 1);
                maxRowHeight = 0;
            }

            Glyph glyph;
            glyph.codePoint = codePoint;
            glyph.x = atlasX;
            glyph.y = atlasY;
            glyph.width = maxx - minx;
            glyph.height = maxy - miny + 1;
            glyph.xoffset = minx;
            glyph.yoffset = miny;
            glyph.xadvance = advance;

            glyphs_.Add(glyph);

            SDL.SDL_Rect glyphRect;
            glyphRect.x = glyph.x;
            glyphRect.y = glyph.y + glyph.yoffset;
            glyphRect.w = glyph.width;
            glyphRect.h = glyph.height;

            IntPtr glyphSurface = SDL_ttf.TTF_RenderGlyph_Blended(font, codePoint, color);

            if (SDL.SDL_BlitSurface(glyphSurface, IntPtr.Zero, atlasSurface, ref glyphRect) != 0)
            {
                throw new Exception("failed to blit surface to surface...");
            }

            SDL.SDL_FreeSurface(glyphSurface);

            atlasX += (glyph.xadvance + 1);
            maxRowHeight = Math.Max(maxRowHeight, glyph.height);
        }

        IntPtr textureAtlas = SDL.SDL_CreateTextureFromSurface(RenderManager.Get().GetRendererPtr(), atlasSurface);
        if(textureAtlas == IntPtr.Zero)
        {
            throw new Exception("failed to create texture atlas...");
        }
        
        return textureAtlas;
    }


    /**
     * @brief �ؽ�ó ��Ʋ�󽺿� ǥ���� ���� �����Դϴ�.
     */
    private ushort beginCodePoint_;


    /**
     * @brief �ؽ�ó ��Ʋ�󽺿� ǥ���� �� �����Դϴ�.
     */
    private ushort endCodePoint_;


    /**
     * @brief �ؽ�ó ��Ʋ���Դϴ�.
     */
    private IntPtr textureAtlas_;


    /**
     * @brief �ؽ�ó ��Ʋ�󽺿� ǥ�õ� ���� �����Դϴ�.
     */
    List<Glyph> glyphs_ = new List<Glyph>();
}