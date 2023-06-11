using System;
using SDL2;
using System.Collections.Generic;

/**
 * @brief ��Ʈ �ؽ�ó ��Ʋ�� �� ���� �����Դϴ�.
 */
struct Glyph
{
    public int codePoint;
    public int x0;
    public int y0;
    public int x1;
    public int y1;
    public float xoffset;
    public float yoffset;
    public float xoffset2;
    public float yoffset2;
    public float xadvance;
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
     * @brief �ڵ� ����Ʈ�� �������Դϴ�.
     */
    private int beginCodePoint_;


    /**
     * @brief �ڵ� ����Ʈ�� �����Դϴ�.
     */
    private int endCodePoint_;


    /**
     * @brief �ؽ�ó ��Ʋ���� ũ���Դϴ�.
     * 
     * @note �ؽ�ó ��Ʋ���� ���� ���� ũ��� �����մϴ�.
     */
    private int textureAtlasSize_;


    /**
     * @brief �ؽ�ó ��Ʋ���� ���ҽ��Դϴ�.
     */
    private IntPtr textureAtlas_;


    /**
     * @brief ��Ʈ�� ũ���Դϴ�.
     */
    private float fontSize_;
}