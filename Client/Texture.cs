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
     * @param Renderer �ؽ�ó�� �����ϱ� ���� �������Դϴ�.
     * @param Path �ؽ�ó ���ҽ��� ����Դϴ�.
     */
    public Texture(IntPtr Renderer, string Path)
    {
        IntPtr Surface = SDL_image.IMG_Load(Path);

        if(Surface == IntPtr.Zero)
        {
            throw new Exception("failed to load texture image...");
        }

        Texture_ = SDL.SDL_CreateTextureFromSurface(Renderer, Surface);
        SDL.SDL_FreeSurface(Surface);

        if(Texture_ == IntPtr.Zero)
        {
            throw new Exception("failed to create texture resource...");
        }

        if(SDL.SDL_QueryTexture(Texture_, out uint _, out int _, out Width_, out Height_) != 0)
        {
            throw new Exception("failed to query texture resource info...");
        }
    }


    /**
     * @brief �ؽ�ó ���ҽ��� �����͸� ��������� �����մϴ�.
     */
    public void Release()
    {
        if(Texture_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(Texture_);
        }
    }


    /**
     * @brief �ؽ�ó �Ӽ��� ���� ���� Getter/Setter �Դϴ�.
     */
    public int Width
    {
        get => Width_;
    }

    public int Height
    {
        get => Height_;
    }

    public IntPtr Resource
    {
        get => Texture_;
    }


    /**
     * @brief �ؽ�ó�� ���� ũ���Դϴ�.
     */
    private int Width_ = 0;


    /**
     * @brief �ؽ�ó�� ���� ũ���Դϴ�.
     */
    private int Height_ = 0;


    /**
     * @brief �ؽ�ó ���ҽ��� �������Դϴ�.
     */
    private IntPtr Texture_;
}