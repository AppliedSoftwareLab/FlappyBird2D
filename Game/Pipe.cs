using System;


/**
 * @brief ������ ������ ������Ʈ�Դϴ�.
 */
class Pipe : GameObject
{
    /**
     * @brief ������ ������Ʈ�� ���¸� �����մϴ�.
     */
    public enum EState
    {
        WAIT = 0x00, // ��׶��� �ۿ��� ��� ���� �����Դϴ�.
        ENTRY = 0x01, // ��׶��� ������ ���� �����Դϴ�.
        LEAVE = 0x02, // ��׶��� ������ ���� �����Դϴ�.
    }


    /**
     * @brief ������ ������ ������Ʈ �Ӽ��� ���� Getter/Setter �Դϴ�.
     */
    public bool Movable
    {
        get => bIsMove_;
        set => bIsMove_ = value;
    }

    public bool PassBird
    {
        get => bIsPassBird_;
        set => bIsPassBird_ = value;
    }

    public float Speed
    {
        get => speed_;
        set => speed_ = value;
    }

    public int SignatureNumber
    {
        get => signatureNumber_;
        set => signatureNumber_ = value;
    }

    public EState State
    {
        get => currentState_;
        set => currentState_ = value;
    }

    public RigidBody TopRigidBody
    {
        get => topRigidBody_;
        set => topRigidBody_ = value;
    }

    public RigidBody BottomRigidBody
    {
        get => bottomRigidBody_;
        set => bottomRigidBody_ = value;
    }


    /**
     * @brief ������ ������ ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        if (currentState_ == EState.LEAVE) return;

        if(bIsMove_)
        {
            Vector2<float> topCenter = topRigidBody_.Center;
            topCenter.x -= (deltaSeconds * speed_);
            topRigidBody_.Center = topCenter;

            Vector2<float> bottomCenter = bottomRigidBody_.Center;
            bottomCenter.x -= (deltaSeconds * speed_);
            bottomRigidBody_.Center = bottomCenter;
        }

        Background background = WorldManager.Get().GetGameObject("Background") as Background;
        switch(currentState_)
        {
            case EState.WAIT:
                CheckEntryFromBackground(background);
                break;

            case EState.ENTRY:
                CheckLeaveFromBackground(background);
                break;

            case EState.LEAVE:
                break;
        }

        float textureWidth = topRigidBody_.Width;
        float textureHeight = topRigidBody_.Height > bottomRigidBody_.Height ? topRigidBody_.Height : bottomRigidBody_.Height;

        Vector2<float> leftTop;
        Vector2<float> rightBottom;

        Texture topPipeTexture = ContentManager.Get().GetTexture("PipeTop");
        leftTop.x = (topRigidBody_.Center.x - textureWidth / 2.0f);
        leftTop.y = (topRigidBody_.Center.y - topRigidBody_.Height / 2.0f - (textureHeight - topRigidBody_.Height));

        rightBottom.x = (topRigidBody_.Center.x + textureWidth / 2.0f);
        rightBottom.y = (topRigidBody_.Center.y + topRigidBody_.Height / 2.0f);

        RenderManager.Get().DrawTexture(ref topPipeTexture, leftTop, rightBottom);

        Texture bottomPipeTexture = ContentManager.Get().GetTexture("PipeBottom");
        leftTop.x = (bottomRigidBody_.Center.x - textureWidth / 2.0f);
        leftTop.y = (bottomRigidBody_.Center.y - bottomRigidBody_.Height / 2.0f);

        rightBottom.x = (bottomRigidBody_.Center.x + textureWidth / 2.0f);
        rightBottom.y = (bottomRigidBody_.Center.y + bottomRigidBody_.Height / 2.0f + (textureHeight - bottomRigidBody_.Height));

        RenderManager.Get().DrawTexture(ref bottomPipeTexture, leftTop, rightBottom);
    }


    /**
     * @brief �������� ��׶��� ������ �������� Ȯ���մϴ�.
     * 
     * @param background ��׶��� ������Ʈ�Դϴ�.
     */
    private void CheckLeaveFromBackground(Background background)
    {
        if (currentState_ != EState.ENTRY) return;

        if (!background.Body.IsCollision(ref topRigidBody_) || !background.Body.IsCollision(ref bottomRigidBody_))
        {
            currentState_ = EState.LEAVE;
        }
    }


    /**
     * @brief �������� ��׶��� ���η� ���Դ��� Ȯ���մϴ�.
     * 
     * @param background ��׶��� ������Ʈ�Դϴ�.
     */
    private void CheckEntryFromBackground(Background background)
    {
        if (currentState_ != EState.WAIT) return;

        if (background.Body.IsCollision(ref topRigidBody_) && background.Body.IsCollision(ref bottomRigidBody_))
        {
            currentState_ = EState.ENTRY;
        }
    }

    
    /**
     * @brief �������� �̵��� �� �ִ��� Ȯ���մϴ�.
     */
    private bool bIsMove_ = false;


    /**
     * @brief �� ������Ʈ�� �� �������� ����ߴ��� Ȯ���մϴ�.
     */
    private bool bIsPassBird_ = false;


    /**
     * @brief �������� �̵� �ӵ��Դϴ�.
     */
    private float speed_ = 0.0f;


    /**
     * @brief �������� ���� �ѹ��Դϴ�.
     */
    private int signatureNumber_ = -1;


    /**
     * @brief �������� �����Դϴ�.
     */
    private EState currentState_ = EState.WAIT;


    /**
     * @brief �������� ��� ��ü�Դϴ�.
     */
    private RigidBody topRigidBody_;


    /**
     * @brief �������� �ϴ� ��ü�Դϴ�.
     */
    private RigidBody bottomRigidBody_;
}