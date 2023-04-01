using System;
using SDL2;


/**
 * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�Դϴ�.
 */
class Bird : IGameObject
{
    /**
     * @brief �� ������Ʈ�� ���¸� �����մϴ�.
     */
    public enum EState
    {
        WAIT = 0x00, // ��� �����Դϴ�.
        JUMP = 0x01, // ���� �����Դϴ�.
        FALL = 0x02, // �������� �����Դϴ�.
        DONE = 0x03, // ���� �����Դϴ�.
    }


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ �Ӽ��� ���� Getter/Setter �Դϴ�.
     */
    public Texture Texture
    {
        get => texture_;
        set => texture_ = value;
    }

    public RigidBody RigidBody
    {
        get => rigidBody_;
        set => rigidBody_ = value;
    }


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float deltaSeconds)
    {
        Vector2<float> center;

        switch(currentState_)
        {
            case EState.WAIT:
                waitTime_ += deltaSeconds;

                if (waitTime_ > maxWaitTime_)
                {
                    waitTime_ = 0.0f;
                    waitMoveDirection_ *= -1.0f;
                }

                center = rigidBody_.Center;
                center.y += (waitMoveDirection_ * deltaSeconds * waitMoveLength_);
                rigidBody_.Center = center;

                if(InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
                {
                    currentState_ = EState.JUMP;
                    rotate_ = MinRotate;
                }
                break;

            case EState.JUMP:
                center = rigidBody_.Center;
                center.y -= (deltaSeconds * moveSpeed_);
                rigidBody_.Center = center;

                jumpMoveLength_ += (deltaSeconds * moveSpeed_);

                if(jumpMoveLength_ > jumpLength_)
                {
                    currentState_ = EState.FALL;
                    jumpMoveLength_ = 0.0f;
                }
                break;

            case EState.FALL:
                rotate_ += (deltaSeconds * rotateSpeed_);

                if (rotate_ > MaxRotate) rotate_ = MaxRotate;

                center = rigidBody_.Center;
                center.y += (deltaSeconds * moveSpeed_);
                rigidBody_.Center = center;

                if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
                {
                    currentState_ = EState.JUMP;
                    rotate_ = MinRotate;
                }
                break;
        }

        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
        if(floor.RigidBody.IsCollision(ref rigidBody_))
        {
            currentState_ = EState.DONE;
            floor.Movable = false;
        }
    }
    

    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     */
    public void Render()
    {
        RenderManager.Get().DrawTexture(ref texture_, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height, rotate_);
    }


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        texture_.Release();
    }


    /**
     * @brief �� ������Ʈ�� ���� �����Դϴ�.
     */
    private EState currentState_ = EState.WAIT;


    /**
     * @brief �� ������Ʈ�� ��� �� �����̴� �����Դϴ�.
     * 
     * @note +�� �Ʒ� ����, -�� �� �����Դϴ�.
     */
    private float waitMoveDirection_ = 1.0f;


    /**
     * @brief �� ������Ʈ�� ��� ���¿��� ������ �ð��Դϴ�.
     */
    private float waitTime_ = 0.0f;


    /**
     * @brief �� ������Ʈ�� ��� ���¿��� ��ٸ� �� �ִ� �ִ� �ð��Դϴ�.
     */
    private float maxWaitTime_ = 1.0f;


    /**
     * @brief �� ������Ʈ�� ��� ���¿��� �����̴� �Ÿ��Դϴ�.
     */
    public float waitMoveLength_ = 10.0f;


    /**
     * @brief �� ������Ʈ�� ���� �� �����̴� �Ÿ��Դϴ�.
     */
    public float jumpLength_ = 70.0f;


    /**
     * @brief �� ������Ʈ�� �̵� �ӵ��Դϴ�.
     */
    public float moveSpeed_ = 300.0f;


    /**
     * @brief �� ������Ʈ�� ���� �� ������ �Ÿ��Դϴ�.
     */
    public float jumpMoveLength_ = 0.0f;

    
    /**
     * @brief �� ������Ʈ�� ȸ�� �����Դϴ�.
     */
    private float rotate_ = 0.0f;


    /**
     * @brief �� ������Ʈ�� ȸ�� �ӵ��Դϴ�.
     */
    private float rotateSpeed_ = 200.0f;


    /**
     * @brief �� ������Ʈ�� �ּ� ȸ�� �����Դϴ�.
     */
    private static readonly float MinRotate = -30.0f;


    /**
     * @brief �� ������Ʈ�� �ִ� ȸ�� �����Դϴ�.
     */
    private static readonly float MaxRotate = 90.0f;


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody rigidBody_;


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� �ؽ�ó�Դϴ�.
     */
    private Texture texture_;
}