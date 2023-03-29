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
     */
    public void Render()
    {
        float factor = accumulateTime_ / speed_;
        RenderManager.Get().DrawHorizonScrollingTexture(ref texture_, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height, factor);
    }


    /**
     * @brief ������ �ٴ� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        texture_.Release();
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