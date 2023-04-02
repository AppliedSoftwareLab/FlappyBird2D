using System;


/**
 * @brief ������ ������ ������Ʈ�Դϴ�.
 */
class Pipe : IGameObject
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

    public float Speed
    {
        get => speed_;
        set => speed_ = value;
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
    public void Update(float deltaSeconds)
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
        if(currentState_ == EState.ENTRY &&
           (!background.Body.IsCollision(ref topRigidBody_) || 
           !background.Body.IsCollision(ref bottomRigidBody_)))
        {
            currentState_ = EState.LEAVE;
        }

        if(currentState_ == EState.WAIT && 
            background.Body.IsCollision(ref topRigidBody_) && 
            background.Body.IsCollision(ref bottomRigidBody_))
        {
            currentState_ = EState.ENTRY;
        }

        Bird bird = WorldManager.Get().GetGameObject("Bird") as Bird;
        if(bird.Body.IsCollision(ref topRigidBody_) || bird.Body.IsCollision(ref bottomRigidBody_))
        {
            bird.State = Bird.EState.DONE;
            bIsMove_ = false;

            Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
            floor.Movable = false;
        }
    }


    /**
     * @brief ������ ������ ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     */
    public void Render()
    {
        Texture topPipeTexture = ContentManager.Get().GetTexture("PipeTop");
        
        RenderManager.Get().DrawTexture(
            ref topPipeTexture,
            topRigidBody_.Center,
            topRigidBody_.Width,
            topRigidBody_.Height
        );

        Texture bottomPipeTexture = ContentManager.Get().GetTexture("PipeBottom");

        RenderManager.Get().DrawTexture(
            ref bottomPipeTexture,
            bottomRigidBody_.Center,
            bottomRigidBody_.Width,
            bottomRigidBody_.Height
        );
    }


    /**
     * @brief �������� �̵��� �� �ִ��� Ȯ���մϴ�.
     */
    private bool bIsMove_ = false;


    /**
     * @brief �������� �̵� �ӵ��Դϴ�.
     */
    private float speed_ = 0.0f;


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