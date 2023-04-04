/**
 * @brief FlappyBird ���� Ÿ��Ʋ ������Ʈ UI�Դϴ�.
 */
class FlappyBirdSlate : Slate
{
    /**
     * @brief FlappyBird ���� Ÿ��Ʋ ������Ʈ UI �Ӽ��� ���� Getter/Setter�Դϴ�.
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
     * @brief FlappyBird ���� Ÿ��Ʋ ������Ʈ UI�� ������Ʈ�մϴ�.
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
     * @brief Ÿ��Ʋ ������Ʈ ������Ʈ�� ������ �� �ִ��� Ȯ���մϴ�.
     */
    private bool bCanMove_ = true;


    /**
     * @brief Ÿ��Ʋ ������Ʈ ������Ʈ�� �����̴� �����Դϴ�.
     * 
     * @note +�� �Ʒ� ����, -�� �� �����Դϴ�.
     */
    private float moveDirection_ = 1.0f;


    /**
     * @brief Ÿ��Ʋ ������Ʈ ������Ʈ�� �����̱� ���� ������ �ð��Դϴ�.
     */
    private float waitTimeForMove_ = 0.0f;


    /**
     * @brief Ÿ��Ʋ ������Ʈ ������Ʈ�� �����̱� ���� ��ٸ� �� �ִ� �ִ� �ð��Դϴ�.
     */
    private float maxWaitTimeForMove_ = 0.0f;


    /**
     * @brief Ÿ��Ʋ ������Ʈ ������Ʈ�� �����̴� �Ÿ��Դϴ�.
     */
    private float moveLength_ = 0.0f;
}