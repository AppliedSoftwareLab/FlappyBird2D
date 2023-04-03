using System;
using System.Collections.Generic; 


/**
 * @brief ������ ������Ʈ�� �����ϰ� �����ϴ� ������Ʈ�Դϴ�.
 */
class PipeDetector : GameObject
{
    /**
     * @brief ������ ������Ʈ�� �����ϰ� �����ϴ� ������Ʈ�� Setter/Getter�Դϴ�. 
     */
    public bool CanGeneratePipe
    {
        get => bCanGeneratePipe_;
        set => bCanGeneratePipe_ = value;
    }

    public int MaxPipeCount
    {
        get => maxPipeCount_;
        set => maxPipeCount_ = value;
    }

    public Vector2<float> RespawnPosition
    {
        get => respawnPosition_;
        set => respawnPosition_ = value;
    }

    public float PipeTopAndBottomGapLength
    {
        get => pipeTopAndBottomGapLength_;
        set => pipeTopAndBottomGapLength_ = value;
    }

    public float PipeToPipeGapLength
    {
        get => pipeToPipeGapLength_;
        set => pipeToPipeGapLength_ = value;
    }

    public float TotalPipeWidth
    {
        get => totalPipeWidth_;
        set => totalPipeWidth_ = value;
    }

    public float TotalPipeHeight
    {
        get => totalPipeHeight_;
        set => totalPipeHeight_ = value;
    }

    public float MinPipeHeight
    {
        get => minPipeHeight_;
        set => minPipeHeight_ = value;
    }

    public float PipeSpeed
    {
        get => pipeSpeed_;
        set => pipeSpeed_ = value;
    }

    public List<Pipe> DetectPipes
    {
        get => detectPipes_;
    }


    /**
     * @brief �������� �����ϰ� �����ϴ� ������Ʈ�� ������Ʈ�մϴ�.
     */
    public override void Update(float deltaSeconds)
    {
        RemoveLeavePipeFromBackground();

        if (CanGenerateDetectPipe())
        {
            GenerateDetectPipe();
        }
    }


    /**
     * @brief �������� �����ϰ� �����ϴ� ������Ʈ�� �������մϴ�.
     * 
     * @note ������ ���� ��ü�� �������� �������� �ʽ��ϴ�.
     */
    public override void Render() {}


    /**
     * @brief ���� ������ ������ ��ü�� ������ �� �ִ��� Ȯ���մϴ�.
     * 
     * @return ���� ������ ������ ��ü�� ������ �� �ִٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    private bool CanGenerateDetectPipe()
    {
        if (!bCanGeneratePipe_) return false;

        if (detectPipes_.Count >= maxPipeCount_) return false;

        if (detectPipes_.Count == 0) return true;

        Pipe pipe = detectPipes_[detectPipes_.Count - 1];
        float pipeXPosition = pipe.TopRigidBody.Center.x;
        float respawnXPosition = respawnPosition_.x;

        return (respawnXPosition - pipeXPosition) > pipeToPipeGapLength_;
    }


    /**
     * @brief ���� ������ ������ ��ü�� �����մϴ�.
     */
    private void GenerateDetectPipe()
    {
        Random random = new Random();

        bool bIsFindRespawnPosition = false;
        while(!bIsFindRespawnPosition)
        {
            float randomY = (float)(random.NextDouble());
            randomY *= totalPipeHeight_;

            float minRandomY = randomY - pipeTopAndBottomGapLength_ / 2.0f;
            float maxRandomY = randomY + pipeTopAndBottomGapLength_ / 2.0f;

            if(minRandomY > minPipeHeight_ && maxRandomY < (totalPipeHeight_ - minPipeHeight_))
            {
                respawnPosition_.y = randomY;
                bIsFindRespawnPosition = true;
            }
        }

        float pipeWidth = totalPipeWidth_;
        float pipeTopHeight = respawnPosition_.y - pipeTopAndBottomGapLength_ / 2.0f;
        float pipeBottomHeight = totalPipeHeight_ - (pipeTopHeight + pipeTopAndBottomGapLength_);

        Vector2<float> topCenter = new Vector2<float>(
            respawnPosition_.x,
            pipeTopHeight / 2.0f
        );

        Vector2<float> bottomCenter = new Vector2<float>(
            respawnPosition_.x,
            totalPipeHeight_ - pipeBottomHeight / 2.0f
        );

        Pipe newPipe = new Pipe();
        newPipe.UpdateOrder = 1;
        newPipe.Movable = true;
        newPipe.Speed = 300.0f;
        newPipe.SignatureNumber = countGeneratePipe_;
        newPipe.TopRigidBody = new RigidBody(topCenter, pipeWidth, pipeTopHeight);
        newPipe.BottomRigidBody = new RigidBody(bottomCenter, pipeWidth, pipeBottomHeight);

        string pipeSignature = string.Format("Pipe{0}", countGeneratePipe_++);

        detectPipes_.Add(newPipe);
        WorldManager.Get().AddGameObject(pipeSignature, newPipe);
    }


    /**
     * @brief ���� ������ ������ ��ü �� ��׶��� ������ ���� ������ ��ü�� �����մϴ�.
     */
    void RemoveLeavePipeFromBackground()
    {
        List<Pipe> removePipes = new List<Pipe>();

        foreach(Pipe detectPipe in detectPipes_)
        {
            if(detectPipe.State == Pipe.EState.LEAVE)
            {
                removePipes.Add(detectPipe);
            }
        }

        foreach(Pipe removePipe in removePipes)
        {
            string pipeSignature = string.Format("Pipe{0}", removePipe.SignatureNumber);

            detectPipes_.Remove(removePipe);
            WorldManager.Get().RemoveGameObject(pipeSignature);
        }
    }


    /**
     * @brief �������� ������ �� �ִ��� Ȯ���մϴ�.
     */
    private bool bCanGeneratePipe_ = false;


    /**
     * @brief �ִ� ������ ���Դϴ�.
     */
    private int maxPipeCount_ = 0;


    /**
     * @brief ������ �������� ���Դϴ�.
     */
    private int countGeneratePipe_ = 0;


    /**
     * @brief �������� ���� ��ġ�Դϴ�.
     */
    private Vector2<float> respawnPosition_;


    /**
     * @brief ������ ������ �����Դϴ�.
     */
    private float pipeToPipeGapLength_ = 0.0f;


    /**
     * @brief ��� �������� �ϴ� ������ ������ �����Դϴ�.
     */
    private float pipeTopAndBottomGapLength_ = 0.0f;


    /**
     * @brief �������� ���� ũ���Դϴ�.
     */
    private float totalPipeWidth_ = 0.0f;


    /**
     * @brief ������ ������ �������� ���� ũ���Դϴ�.
     */
    private float totalPipeHeight_ = 0.0f;


    /**
     * @brief �������� �ּ� �����Դϴ�.
     */
    private float minPipeHeight_ = 0.0f;


    /**
     * @brief �������� �̵� �ӵ��Դϴ�.
     */
    private float pipeSpeed_ = 0.0f;


    /**
     * @brief ���� ���� ���� �������Դϴ�.
     */
    private List<Pipe> detectPipes_ = new List<Pipe>();
}