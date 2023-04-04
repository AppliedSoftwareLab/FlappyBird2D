/**
 * @brief ���� ���� �� ������Ʈ UI ������Ʈ�Դϴ�.
 */
class BirdSlate : Slate
{
    /**
     * @brief ���� ���� �� ������Ʈ UI ������Ʈ �Ӽ��� ���� Getter/Setter�Դϴ�.
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

    public float ChangeWingStateTime
    {
        set => changeWingStateTime_ = value;
    }


    /**
     * @brief �� ������Ʈ UI ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        if (bCanMove_)
        {
            UpdateSlatePosition(deltaSeconds);
            UpdateWingState(deltaSeconds);
        }

        UpdateUITexture();

        base.Tick(deltaSeconds);
    }


    /**
     * @brief ��ġ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateSlatePosition(float deltaSeconds)
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


    /**
     * @brief ���� ���¸� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateWingState(float deltaSeconds)
    {
        wingStateTime_ += deltaSeconds;

        if (wingStateTime_ > changeWingStateTime_)
        {
            wingStateTime_ = 0.0f;

            if (currWingState_ == Bird.EWing.NORMAL)
            {
                currWingState_ = Bird.GetCountWingState(prevWingState_);
                prevWingState_ = Bird.EWing.NORMAL;
            }
            else // currWingState_ == EWing.DOWN or currWingState_ == EWing.UP
            {
                prevWingState_ = currWingState_;
                currWingState_ = Bird.EWing.NORMAL;
            }
        }
    }


    /**
     * @brief ���� ���¿� �´� �ؽ�ó�� ������Ʈ�մϴ�.
     */
    private void UpdateUITexture()
    {
        string birdTextureSignature = "BirdWingNormal";

        if (currWingState_ != Bird.EWing.NORMAL)
        {
            birdTextureSignature = (currWingState_ == Bird.EWing.UP) ? "BirdWingUp" : "BirdWingDown";
        }

        UITexture = birdTextureSignature;
    }


    /**
     * @brief �� ������Ʈ ������Ʈ�� ������ �� �ִ��� Ȯ���մϴ�.
     */
    private bool bCanMove_ = true;


    /**
     * @brief �� ������Ʈ ������Ʈ�� �����̴� �����Դϴ�.
     * 
     * @note +�� �Ʒ� ����, -�� �� �����Դϴ�.
     */
    private float moveDirection_ = 1.0f;


    /**
     * @brief �� ������Ʈ ������Ʈ�� �����̱� ���� ������ �ð��Դϴ�.
     */
    private float waitTimeForMove_ = 0.0f;


    /**
     * @brief �� ������Ʈ ������Ʈ�� �����̱� ���� ��ٸ� �� �ִ� �ִ� �ð��Դϴ�.
     */
    private float maxWaitTimeForMove_ = 0.0f;


    /**
     * @brief �� ������Ʈ ������Ʈ�� �����̴� �Ÿ��Դϴ�.
     */
    private float moveLength_ = 0.0f;


    /**
     * @brief ������ �� ������Ʈ�� ���� �����Դϴ�.
     */
    private Bird.EWing prevWingState_ = Bird.EWing.NORMAL;


    /**
     * @brief ���� �� ������Ʈ�� ���� �����Դϴ�.
     */
    private Bird.EWing currWingState_ = Bird.EWing.UP;


    /**
     * @brief �� ������Ʈ�� ���� ���°� ���ӵ� �ð��Դϴ�.
     */
    private float wingStateTime_ = 0.0f;


    /**
     * @brief �� ������Ʈ�� ���� ���¸� �����ϴ� �ð��Դϴ�.
     */
    private float changeWingStateTime_ = 0.0f;
}