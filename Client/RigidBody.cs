/**
 * @brief ������Ʈ�� ��ü(RigidBody)�Դϴ�.
 * 
 * @note �� ��ü�� AABB ����Դϴ�.
 */
class RigidBody
{
    /**
     * @brief ������Ʈ ��ü�� �������Դϴ�.
     * 
     * @param center ������Ʈ ��ü�� �߽� ��ǥ�Դϴ�.
     * @param width ������Ʈ ��ü�� ���� ũ���Դϴ�.
     * @param height ������Ʈ ��ü�� ���� ũ���Դϴ�.
     */
    public RigidBody(Vector2<float> center, float width, float height)
    {
        center_ = center;
        width_ = width;
        height_ = height;
    }


    /**
     * @brief ������Ʈ ��ü �Ӽ��� ���� Getter/Setter �Դϴ�.
     */
    public Vector2<float> Center
    {
        get => center_;
        set => center_ = value;
    }

    public float Width
    {
        get => width_;
        set => width_ = value;
    }

    public float Height
    {
        get => height_;
        set => height_ = value;
    }


    /**
     * @brief ��ü�� �߽� ��ǥ�Դϴ�.
     */
    private Vector2<float> center_;


    /**
     * @brief ��ü�� ���� ũ���Դϴ�.
     */
    private float width_ = 0.0f;


    /**
     * @brief ��ü�� ���� ũ���Դϴ�.
     */
    private float height_ = 0.0f;
}
