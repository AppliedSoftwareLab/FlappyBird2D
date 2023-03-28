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
        get => speed_;
        set => speed_ = value;
    }

    public bool Movable
    {
        get => bIsMove_;
        set => bIsMove_ = value;
    }

    public Texture Texture
    {
        get => texture_;
        set => texture_ = value;
    }

    public RigidBody RigidBody
    {
        get => rigidBody_;
        set => rigidBody_ = value;
    }


    /**
     * @brief ������ �ٴ� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param DeltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float DeltaSeconds)
    {
        if (bIsMove_)
        {
            accumulateTime_ += DeltaSeconds;
        }

        if(accumulateTime_ > speed_)
        {
            accumulateTime_ -= speed_;
        }
    }


    /**
     * @brief ������ �ٴ� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     * 
     * @param renderer ������Ʈ�� ȭ�鿡 �׸��� ���� �������Դϴ�.
     */
    public void Render(IntPtr renderer)
    {
        float factor = accumulateTime_ / speed_;

        RenderLeftSide(renderer, factor);
        RenderRightSide(renderer, factor);
    }


    /**
     * @brief ������ �ٴ� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        texture_.Release();
    }


    /**
     * @brief �ٴ��� ���� ������ �׸��ϴ�.
     * 
     * @param renderer �ٴ��� ���� ������ �׸� �� ����� �������Դϴ�.
     * @param factor ���� ������ �����Դϴ�.
     */
    private void RenderLeftSide(IntPtr renderer, float factor)
    {
        float width = texture_.Width;
        float height = texture_.Height;

        SDL.SDL_Rect leftSrcRect;
        leftSrcRect.x = (int)(width * factor);
        leftSrcRect.y = 0;
        leftSrcRect.w = (int)(width * (1.0f - factor));
        leftSrcRect.h = (int)height;

        SDL.SDL_Rect leftDstRect;
        leftDstRect.x = (int)(rigidBody_.Center.x - rigidBody_.Width / 2.0f);
        leftDstRect.y = (int)(rigidBody_.Center.y - rigidBody_.Height / 2.0f);
        leftDstRect.w = (int)(rigidBody_.Width * (1.0f - factor));
        leftDstRect.h = (int)(rigidBody_.Height);

        SDL.SDL_RenderCopy(
            renderer,
            texture_.Resource,
            ref leftSrcRect,
            ref leftDstRect
        );
    }


    /**
     * @brief �ٴ��� ������ ������ �׸��ϴ�.
     * 
     * @note ������ ������ �׸� ��, ���� ������ �������� �׸��ϴ�.
     * 
     * @param renderer �ٴ��� ������ ������ �׸� �� ����� �������Դϴ�.
     * @param factor ���� ������ �����Դϴ�.
     */
    private void RenderRightSide(IntPtr renderer, float factor)
    {
        float width = texture_.Width;
        float height = texture_.Height;

        SDL.SDL_Rect rightSrcRect;
        rightSrcRect.x = 0;
        rightSrcRect.y = 0;
        rightSrcRect.w = (int)(width * factor);
        rightSrcRect.h = (int)height;

        SDL.SDL_Rect rightDstRect;
        rightDstRect.x = (int)(rigidBody_.Center.x - rigidBody_.Width / 2.0f + rigidBody_.Width * (1.0f - factor));
        rightDstRect.y = (int)(rigidBody_.Center.y - rigidBody_.Height / 2.0f);
        rightDstRect.w = (int)(rigidBody_.Width * factor);
        rightDstRect.h = (int)(rigidBody_.Height);

        SDL.SDL_RenderCopy(
             renderer,
             texture_.Resource,
             ref rightSrcRect,
             ref rightDstRect
        );
    }


    /**
     * @brief ������ �ٴ��� ���� �̵� ������ Ȯ���մϴ�.
     */
    private bool bIsMove_ = false;


    /**
     * @brief ���� �ٴ� ������Ʈ�� ���� �ð��Դϴ�.
     * 
     * @note ������ �ʴ����Դϴ�.
     */
    private float accumulateTime_ = 0.0f;


    /**
     * @brief ������ �ٴ� �̵� �ӵ��Դϴ�.
     */
    private float speed_ = 0.0f;


    /**
     * @brief ���� �ٴ� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody rigidBody_;


    /**
     * @brief ������ �ٴ� ������Ʈ �ؽ�ó�Դϴ�.
     */
    private Texture texture_;
}