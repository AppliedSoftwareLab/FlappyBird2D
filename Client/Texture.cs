using System;
using SDL2;


/**
 * @brief �ؽ�ó�� �ε��ϰ� �����ϴ� Ŭ�����Դϴ�.
 */
class Texture
{
    /**
     * @brief �ؽ�ó�� �ε��ϰ� �����ϴ� Ŭ������ �������Դϴ�.
     * 
     * @param renderer �ؽ�ó�� �����ϱ� ���� �������Դϴ�.
     * @param path �ؽ�ó ���ҽ��� ����Դϴ�.
     * 
     * @throws
     * - �ؽ�ó �̹��� �ε��� �����ϸ� ���ܸ� �����ϴ�.
     * - �ؽ�ó ���ҽ� ������ �����ϸ� ���ܸ� �����ϴ�.
     */
    public Texture(IntPtr renderer, string path)
    {
        IntPtr surface = SDL_image.IMG_Load(path);

        if(surface == IntPtr.Zero)
        {
            throw new Exception("failed to load texture image...");
        }

        texture_ = SDL.SDL_CreateTextureFromSurface(renderer, surface);
        SDL.SDL_FreeSurface(surface);

        if(texture_ == IntPtr.Zero)
        {
            throw new Exception("failed to create texture resource...");
        }

        if(SDL.SDL_QueryTexture(texture_, out uint _, out int _, out width_, out height_) != 0)
        {
            throw new Exception("failed to query texture resource info...");
        }
    }


    /**
     * @brief �ؽ�ó �Ӽ��� ���� ���� Getter/Setter �Դϴ�.
     */
    public int Width
    {
        get => width_;
    }

    public int Height
    {
        get => height_;
    }

    public IntPtr Resource
    {
        get => texture_;
    }


    /**
     * @brief �ؽ�ó ���ҽ��� �����͸� ��������� �����մϴ�.
     */
    public void Release()
    {
        if(texture_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(texture_);
        }
    }


    /**
     * @brief �ؽ�ó�� ���� ũ���Դϴ�.
     */
    private int width_ = 0;


    /**
     * @brief �ؽ�ó�� ���� ũ���Դϴ�.
     */
    private int height_ = 0;


    /**
     * @brief �ؽ�ó ���ҽ��� �������Դϴ�.
     */
    private IntPtr texture_;
}