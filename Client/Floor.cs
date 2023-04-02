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

    public RigidBody RigidBody
    {
        get => rigidBody_;
        set => rigidBody_ = value;
    }


    /**
     * @brief ������ �ٴ� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float deltaSeconds)
    {
        if (bIsMove_)
        {
            accumulateTime_ += deltaSeconds;
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

        Texture floorTexture = ContentManager.Get().GetTexture("Floor");

        RenderManager.Get().DrawHorizonScrollingTexture(
            ref floorTexture, 
            rigidBody_.Center, 
            rigidBody_.Width, 
            rigidBody_.Height, 
            factor
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
}