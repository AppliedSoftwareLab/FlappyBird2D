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
     * @param Center ������Ʈ ��ü�� �߽� ��ǥ�Դϴ�.
     * @param Width ������Ʈ ��ü�� ���� ũ���Դϴ�.
     * @param Height ������Ʈ ��ü�� ���� ũ���Դϴ�.
     */
    public RigidBody(Vector2<float> Center, float Width, float Height)
    {
        Center_ = Center;
        Width_ = Width;
        Height_ = Height;
    }


    /**
     * @brief ������Ʈ ��ü �Ӽ��� ���� Getter/Setter �Դϴ�.
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
     * @brief ��ü�� �߽� ��ǥ�Դϴ�.
     */
    private Vector2<float> Center_;


    /**
     * @brief ��ü�� ���� ũ���Դϴ�.
     */
    private float Width_ = 0.0f;


    /**
     * @brief ��ü�� ���� ũ���Դϴ�.
     */
    private float Height_ = 0.0f;
}
