/**
 * @brief ���� ���� �� ������Ʈ UI ������Ʈ�Դϴ�.
 */
class BirdSlate : SlideSlate
{
    /**
     * @brief ���� ���� �� ������Ʈ UI ������Ʈ �Ӽ��� ���� Getter/Setter�Դϴ�.
     */
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
            UpdateWingState(deltaSeconds);
        }

        UpdateUITexture();

        base.Tick(deltaSeconds);
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