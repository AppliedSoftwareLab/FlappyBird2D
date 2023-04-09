/**
 * @brief �����̴� ������Ʈ UI�Դϴ�.
 */
class SlideSlate : Slate
{
    /**
     * @brief �����̴� ������Ʈ UI �Ӽ��� ���� Getter/Setter�Դϴ�.
     */
    public bool Movable
    {
        get => bCanMove_;
        set => bCanMove_ = value;
    }

    public float MaxWaitTimeForMove
    {
        set => maxWaitTimeForMove_ = value;
    }

    public float MoveLength
    {
        set => moveLength_ = value;
    }


    /**
     * @brief �����̴� ������Ʈ UI�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        if (bCanMove_)
        {
            waitTimeForMove_ += deltaSeconds;
            if (waitTimeForMove_ > maxWaitTimeForMove_)
            {
                waitTimeForMove_ = 0.0f;
                moveDirection_ *= -1.0f;
            }

            Vector2<float> center = UIBody.Center;
            center.y += (moveDirection_ * deltaSeconds * moveLength_);
            UIBody.Center = center;
        }

        base.Tick(deltaSeconds);
    }


    /**
     * @brief �����̴� ������Ʈ ������Ʈ�� ������ �� �ִ��� Ȯ���մϴ�.
     */
    private bool bCanMove_ = true;


    /**
     * @brief �����̴� ������Ʈ ������Ʈ�� �����̴� �����Դϴ�.
     * 
     * @note +�� �Ʒ� ����, -�� �� �����Դϴ�.
     */
    private float moveDirection_ = 1.0f;


    /**
     * @brief �����̴� ������Ʈ ������Ʈ�� �����̱� ���� ������ �ð��Դϴ�.
     */
    private float waitTimeForMove_ = 0.0f;


    /**
     * @brief �����̴� ������Ʈ ������Ʈ�� �����̱� ���� ��ٸ� �� �ִ� �ִ� �ð��Դϴ�.
     */
    private float maxWaitTimeForMove_ = 0.0f;


    /**
     * @brief �����̴� ������Ʈ ������Ʈ�� �����̴� �Ÿ��Դϴ�.
     */
    private float moveLength_ = 0.0f;
}