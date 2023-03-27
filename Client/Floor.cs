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
     * @brief �ٴ� �ؽ�ó�� �����մϴ�.
     * 
     * @note �̹� �ؽ�ó�� �ε��Ǿ� �ִٸ�, ������ �ؽ�ó ���ҽ��� �����մϴ�.
     * 
     * @param FloorTexture ������ ��׶��� �ؽ�ó�Դϴ�.
     * 
     * @throws �ؽ�ó ���ҽ� ������ �����ϸ� ǥ�� ���ܸ� �����ϴ�.
     */
    public void SetTexture(Texture FloorTexture)
    {
        FloorTexture_ = FloorTexture;
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

        float Width = FloorTexture_.Width;
        float Height = FloorTexture_.Height;

        SDL.SDL_Rect LeftSrcRect;
        LeftSrcRect.x = (int)(Width * Lerp);
        LeftSrcRect.y = 0;
        LeftSrcRect.w = (int)(Width * (1.0f - Lerp));
        LeftSrcRect.h = (int)Height;

        SDL.SDL_Rect LeftDstRect;
        LeftDstRect.x = (int)(Center_.X - Width_ / 2.0f);
        LeftDstRect.y = (int)(Center_.Y - Height_ / 2.0f);
        LeftDstRect.w = (int)(Width_ * (1.0f - Lerp));
        LeftDstRect.h = (int)(Height_);

        SDL.SDL_RenderCopy(
            Renderer,
            FloorTexture_.Resource,
            ref LeftSrcRect,
            ref LeftDstRect
        );

        SDL.SDL_Rect RightSrcRect;
        RightSrcRect.x = 0;
        RightSrcRect.y = 0;
        RightSrcRect.w = (int)(Width * Lerp);
        RightSrcRect.h = (int)Height;

        SDL.SDL_Rect RightDstRect;
        RightDstRect.x = (int)(Center_.X - Width_ / 2.0f + Width_ * (1.0f - Lerp));
        RightDstRect.y = (int)(Center_.Y - Height_ / 2.0f);
        RightDstRect.w = (int)(Width_ * Lerp);
        RightDstRect.h = (int)(Height_);
        
        SDL.SDL_RenderCopy(
             Renderer,
             FloorTexture_.Resource,
             ref RightSrcRect,
             ref RightDstRect
         );
    }


    /**
     * @brief ��׶��� ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        FloorTexture_.Release();
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
    private Texture FloorTexture_;
}