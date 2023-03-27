using System;
using SDL2;


/**
 * @brief ������ ��׶��� ������Ʈ�Դϴ�.
 */
class Background : IGameObject
{
    /**
     * @brief ������ ��׶��� ������Ʈ �Ӽ��� ���� Getter/Setter �Դϴ�.
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
        if (TextureSurface == IntPtr.Zero)
        {
            return false;
        }

        BGTexture_ = SDL.SDL_CreateTextureFromSurface(Renderer, TextureSurface);
        SDL.SDL_FreeSurface(TextureSurface);

        return (BGTexture_ != IntPtr.Zero);
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param DeltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float DeltaSeconds)
    {
        
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     * 
     * @param Renderer ������Ʈ�� ȭ�鿡 �׸��� ���� �������Դϴ�.
     */
    public void Render(IntPtr Renderer)
    {
        SDL.SDL_Rect SpriteRect;
        SpriteRect.x = (int)(Center_.X - Width_ / 2.0f);
        SpriteRect.y = (int)(Center_.Y - Height_ / 2.0f);
        SpriteRect.w = (int)(Width_);
        SpriteRect.h = (int)(Height_);

        SDL.SDL_RenderCopy(
            Renderer,
            BGTexture_,
            IntPtr.Zero,
            ref SpriteRect
        );
    }


    /**
     * @brief ��׶��� ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        if (BGTexture_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(BGTexture_);
        }
    }


    /**
     * @brief ������ ��׶��� ������Ʈ �߽� ��ǥ�Դϴ�.
     */
    private Vector2<float> Center_;


    /**
     * @brief ������ ��׶��� ������Ʈ ���� ũ���Դϴ�.
     */
    private float Width_ = 0.0f;


    /**
     * @brief ������ ��׶��� ������Ʈ ���� ũ���Դϴ�.
     */
    private float Height_ = 0.0f;


    /**
     * @brief ������ ��׶��� ������Ʈ �ؽ�ó�Դϴ�.
     */
    private IntPtr BGTexture_;
}