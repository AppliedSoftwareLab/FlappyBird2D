/**
 * @brief ������ �ٴ� ������Ʈ�Դϴ�.
 */
class Floor : GameObject
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

    public RigidBody Body
    {
        get => rigidBody_;
    }


    /**
     * @brief �ٴ��� �ٵ� �����մϴ�.
     * 
     * @param center �ٴ� �ٵ��� �߽� ��ǥ�Դϴ�.
     * @param width �ٴ� �ٵ��� ���� ũ���Դϴ�.
     * @param height �ٴ� �ٵ��� ���� ũ���Դϴ�.
     */
    public void CreateBody(Vector2<float> center, float width, float height)
    {
        rigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief ������ �ٴ� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        if (bIsMove_)
        {
            moveLength_ += (deltaSeconds * speed_);

            if (moveLength_ > rigidBody_.Width)
            {
                moveLength_ -= rigidBody_.Width;
            }
        }

        float factor = moveLength_ / rigidBody_.Width;

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
     * @brief ������ �ٴ��� �̵��� �Ÿ��Դϴ�.
     */
    private float moveLength_ = 0.0f;


    /**
     * @brief ������ �ٴ� �̵� �ӵ��Դϴ�.
     */
    private float speed_ = 0.0f;


    /**
     * @brief ���� �ٴ� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody rigidBody_;
}