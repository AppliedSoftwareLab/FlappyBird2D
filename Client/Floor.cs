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
        Texture_ = FloorTexture;
    }


    /**
     * @brief �ٴ��� ��ü�� �����մϴ�.
     * 
     * @note �̹� �����Ǿ� �ִٸ�, ������ ��ü�� ���� ���ϴ�.
     * 
     * @param FloorRigidBody ������ ��ü�Դϴ�.
     */
    public void SetRigidBody(RigidBody FloorRigidBody)
    {
        RigidBody_ = FloorRigidBody;
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

        float Width = Texture_.Width;
        float Height = Texture_.Height;

        SDL.SDL_Rect LeftSrcRect;
        LeftSrcRect.x = (int)(Width * Lerp);
        LeftSrcRect.y = 0;
        LeftSrcRect.w = (int)(Width * (1.0f - Lerp));
        LeftSrcRect.h = (int)Height;

        SDL.SDL_Rect LeftDstRect;
        LeftDstRect.x = (int)(RigidBody_.Center.X - RigidBody_.Width / 2.0f);
        LeftDstRect.y = (int)(RigidBody_.Center.Y - RigidBody_.Height / 2.0f);
        LeftDstRect.w = (int)(RigidBody_.Width * (1.0f - Lerp));
        LeftDstRect.h = (int)(RigidBody_.Height);

        SDL.SDL_RenderCopy(
            Renderer,
            Texture_.Resource,
            ref LeftSrcRect,
            ref LeftDstRect
        );

        SDL.SDL_Rect RightSrcRect;
        RightSrcRect.x = 0;
        RightSrcRect.y = 0;
        RightSrcRect.w = (int)(Width * Lerp);
        RightSrcRect.h = (int)Height;

        SDL.SDL_Rect RightDstRect;
        RightDstRect.x = (int)(RigidBody_.Center.X - RigidBody_.Width / 2.0f + RigidBody_.Width * (1.0f - Lerp));
        RightDstRect.y = (int)(RigidBody_.Center.Y - RigidBody_.Height / 2.0f);
        RightDstRect.w = (int)(RigidBody_.Width * Lerp);
        RightDstRect.h = (int)(RigidBody_.Height);
        
        SDL.SDL_RenderCopy(
             Renderer,
             Texture_.Resource,
             ref RightSrcRect,
             ref RightDstRect
         );
    }


    /**
     * @brief ��׶��� ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        Texture_.Release();
    }


    /**
     * @brief ������ �ٴ��� ���� �̵� ������ Ȯ���մϴ�.
     */
    bool bIsMove_ = false;


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
     * @brief ���� �ٴ� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody RigidBody_;


    /**
     * @brief ������ �ٴ� ������Ʈ �ؽ�ó�Դϴ�.
     */
    private Texture Texture_;
}