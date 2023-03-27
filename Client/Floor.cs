using System;
using SDL2;


/**
 * @brief ������ �ٴ� ������Ʈ�Դϴ�.
 */
class Floor : IGameObject
{
    /**
     * @brief ������ �ٴ� ������Ʈ �Ӽ��� ���� Getter/Setter �Դϴ�.
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

    public float Speed
    {
        get => Speed_;
        set => Speed_ = value;
    }

    public bool Movable
    {
        get => bIsMove_;
        set => bIsMove_ = value;
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

        Texture_ = SDL.SDL_CreateTextureFromSurface(Renderer, TextureSurface);
        SDL.SDL_FreeSurface(TextureSurface);
        
        return (Texture_ != IntPtr.Zero);
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param DeltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float DeltaSeconds)
    {
        if (bIsMove_)
        {
            AccumulateTime_ += DeltaSeconds;
        }

        if(AccumulateTime_ > Speed_)
        {
            AccumulateTime_ -= Speed_;
        }
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     * 
     * @param Renderer ������Ʈ�� ȭ�鿡 �׸��� ���� �������Դϴ�.
     */
    public void Render(IntPtr Renderer)
    {
        float Lerp = AccumulateTime_ / Speed_;

        int w, h;
        SDL.SDL_QueryTexture(Texture_, out uint format, out int access, out w, out h);

        SDL.SDL_Rect LeftSrcRect;
        LeftSrcRect.x = (int)((float)w * Lerp);
        LeftSrcRect.y = 0;
        LeftSrcRect.w = (int)((float)w * (1.0f - Lerp));
        LeftSrcRect.h = h;

        SDL.SDL_Rect LeftDstRect;
        LeftDstRect.x = (int)(Center_.X - Width_ / 2.0f);
        LeftDstRect.y = (int)(Center_.Y - Height_ / 2.0f);
        LeftDstRect.w = (int)(Width_ * (1.0f - Lerp));
        LeftDstRect.h = (int)(Height_);

        SDL.SDL_RenderCopy(
            Renderer,
            Texture_,
            ref LeftSrcRect,
            ref LeftDstRect
        );

        SDL.SDL_Rect RightSrcRect;
        RightSrcRect.x = 0;
        RightSrcRect.y = 0;
        RightSrcRect.w = (int)((float)w * Lerp);
        RightSrcRect.h = h;

        SDL.SDL_Rect RightDstRect;
        RightDstRect.x = (int)(Center_.X - Width_ / 2.0f + Width_ * (1.0f - Lerp));
        RightDstRect.y = (int)(Center_.Y - Height_ / 2.0f);
        RightDstRect.w = (int)(Width_ * Lerp);
        RightDstRect.h = (int)(Height_);
        
        SDL.SDL_RenderCopy(
             Renderer,
             Texture_,
             ref RightSrcRect,
             ref RightDstRect
         );
    }


    /**
     * @brief ��׶��� ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        if (Texture_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(Texture_);
        }
    }


    /**
     * @brief ������ �ٴ��� ���� �̵� ������ Ȯ���մϴ�.
     */
    bool bIsMove_ = false;


    /**
     * @brief ������ �ٴ� ������Ʈ �߽� ��ǥ�Դϴ�.
     */
    private Vector2<float> Center_;


    /**
     * @brief ������ �ٴ� ������Ʈ ���� ũ���Դϴ�.
     */
    private float Width_ = 0.0f;


    /**
     * @brief ������ �ٴ� ������Ʈ ���� ũ���Դϴ�.
     */
    private float Height_ = 0.0f;


    /**
     * @brief ���� �ٴ� ������Ʈ�� ���� �ð��Դϴ�.
     * 
     * @note ������ �ʴ����Դϴ�.
     */
    private float AccumulateTime_ = 0.0f;


    /**
     * @brief ������ �ٴ� �̵� �ӵ��Դϴ�.
     */
    private float Speed_ = 0.0f;


    /**
     * @brief ������ �ٴ� ������Ʈ �ؽ�ó�Դϴ�.
     */
    private IntPtr Texture_;
}