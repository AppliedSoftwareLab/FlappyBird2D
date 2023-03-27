using System;
using SDL2;


/**
 * @brief ���� ���� ��������Ʈ ��ü�Դϴ�.
 */
class Sprite
{
    /**
     * @brief ��������Ʈ ��ü�� ����Ʈ �������Դϴ�.
     */
    public Sprite()
    {
        Center_.X = 0.0f;
        Center_.Y = 0.0f;
    }


    /**
     * @brief ��������Ʈ ��ü�� �Ӽ��� ���� Getter/Setter �Դϴ�.
     */
    public Vector2<float> Center
    {
        get => Center_;
        set => Center_ = value;
    }

    public float Width
    {
        get => Width_;
        set => Width_ = value;
    }

    public float Height
    {
        get => Height_;
        set => Height_ = value;
    }

    public float Rotate
    {
        get => Rotate_;
        set => Rotate_ = value;
    }


    /**
     * @brief �ؽ�ó�� �ε��մϴ�.
     * 
     * @note �̹� �ؽ�ó�� �ε��Ǿ� �ִٸ�, ������ �ؽ�ó ���ҽ��� �����մϴ�.
     * 
     * @param Renderer �ؽ�ó ���ҽ��� ������ �� ����� �������Դϴ�.
     * @param TexturePath ��������Ʈ ��ü�� �ؽ�ó ���ҽ� ����Դϴ�.
     * 
     * @return �ؽ�ó �ε��� �����ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    public bool LoadTexture(IntPtr Renderer, string TexturePath)
    {
        IntPtr TextureSurface = SDL_image.IMG_Load(TexturePath);
        if(TextureSurface == IntPtr.Zero)
        {
            return false;
        }

        Texture_ = SDL.SDL_CreateTextureFromSurface(Renderer, TextureSurface);
        SDL.SDL_FreeSurface(TextureSurface);

        return (Texture_ != IntPtr.Zero);
    }


    /**
     * @brief ��������Ʈ�� �Ҵ�� �ؽ�ó�� �Ҵ� �����մϴ�.
     */
    public void ReleaseTexture()
    {
        if(Texture_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(Texture_);
        }
    }


    /**
     * @brief ��������Ʈ ��ü�� ������Ʈ�մϴ�.
     * 
     * @param DeltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float DeltaSeconds)
    {

    }


    /**
     * @brief ��������Ʈ ��ü�� �������մϴ�.
     * 
     * @param Renderer ��ü�� �������� �������Դϴ�.
     */
    public void Render(IntPtr Renderer)
    {
        SDL.SDL_Rect SpriteRect;
        SpriteRect.x = (int)(Center_.X - Width_ / 2.0f);
        SpriteRect.y = (int)(Center_.Y - Height_ / 2.0f);
        SpriteRect.w = (int)(Width_);
        SpriteRect.h = (int)(Height_);

        SDL.SDL_Point SpritePoint;
        SpritePoint.x = (int)(Center_.X);
        SpritePoint.y = (int)(Center_.Y);

        SDL.SDL_RenderCopyEx(
            Renderer,
            Texture_,
            IntPtr.Zero,
            ref SpriteRect,
            (double)(Rotate_),
            ref SpritePoint,
            SDL.SDL_RendererFlip.SDL_FLIP_NONE
        );
    }


    /**
     * @brief ��������Ʈ ��ü�� �߽� ��ǥ�Դϴ�.
     */
    private Vector2<float> Center_;


    /**
     * @brief ��������Ʈ ��ü�� ���� ũ���Դϴ�.
     */
    private float Width_ = 0.0f;


    /**
     * @brief ��������Ʈ ��ü�� ���� ũ���Դϴ�.
     */
    private float Height_ = 0.0f;


    /**
     * @brief ��������Ʈ ��ü�� �����Դϴ�.
     * 
     * @note �̶�, ���� ������ ������ ���ʺй��Դϴ�.
     */
    private float Rotate_ = 0.0f;


    /**
     * @brief ��������Ʈ ��ü�� �ؽ�ó�Դϴ�.
     */
    private IntPtr Texture_;
}