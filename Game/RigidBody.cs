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
     * @brief �ٸ� ��ü�� �浹�ϴ��� �˻��մϴ�.
     * 
     * @note AABB ����� �浹 ������ �����մϴ�.
     * 
     * @param rigidBody �˻縦 ������ ��ü�Դϴ�.
     * 
     * @return �浹�Ѵٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    public bool IsCollision(ref RigidBody rigidBody)
    {
        Vector2<float> minPosition, maxPosition;
        GetBoundPosition(out minPosition, out maxPosition);

        Vector2<float> otherMinPosition, otherMaxPosition;
        rigidBody.GetBoundPosition(out otherMinPosition, out otherMaxPosition);

        if (maxPosition.x <= otherMinPosition.x || minPosition.x >= otherMaxPosition.x)
        {
            return false;
        }

        if (maxPosition.y <= otherMinPosition.y || minPosition.y >= otherMaxPosition.y)
        {
            return false;
        }

        return true;
    }


    /**
     * @brief ��ü�� ��� ����(�ִ�-�ּ�) ��ǥ�� ����ϴ�.
     * 
     * @param minPosition[out] ��ü�� ��� ���� �� �ּ� ��ǥ�Դϴ�.
     * @param maxPosition[out] ��ü�� ��� ���� �� �ִ� ��ǥ�Դϴ�.
     */
    public void GetBoundPosition(out Vector2<float> minPosition, out Vector2<float> maxPosition)
    {
        minPosition.x = center_.x - width_ / 2.0f;
        minPosition.y = center_.y - height_ / 2.0f;

        maxPosition.x = center_.x + width_ / 2.0f;
        maxPosition.y = center_.y + height_ / 2.0f;
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
