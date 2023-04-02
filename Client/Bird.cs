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
     * @brief �� ������Ʈ�� �����Դϴ�.
     */
    public enum EWing
    {
        UP = 0x00,     // ������ ���� �ֽ��ϴ�.
        NORMAL = 0x01, // ������ �������ϴ�.
        DOWN = 0x02,   // ������ �Ʒ��� �ֽ��ϴ�.
    }


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ �Ӽ��� ���� Getter/Setter �Դϴ�.
     */
    public RigidBody Body
    {
        get => rigidBody_;
    }

    public EState State
    {
        get => currentState_;
        set => currentState_ = value;
    }


    /**
     * @brief �� ������Ʈ�� �ٵ� �����մϴ�.
     * 
     * @param center �� ������Ʈ �ٵ��� �߽� ��ǥ�Դϴ�.
     * @param width �� ������Ʈ �ٵ��� ���� ũ���Դϴ�.
     * @param height �� ������Ʈ �ٵ��� ���� ũ���Դϴ�.
     */
    public void CreateBody(Vector2<float> center, float width, float height)
    {
        rigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float deltaSeconds)
    {
        switch(currentState_)
        {
            case EState.WAIT:
                UpdateWaitState(deltaSeconds);
                break;

            case EState.JUMP:
                UpdateJumpState(deltaSeconds);
                break;

            case EState.FALL:
                UpdateFallState(deltaSeconds);
                break;
        }

        CheckCollisionFloor();
    }
    

    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     */
    public void Render()
    {
        string birdTextureSignature = "BirdWingNormal";

        if(currentState_ == EState.JUMP && (currWingState_ != EWing.NORMAL))
        {
            birdTextureSignature = (currWingState_ == EWing.UP) ? "BirdWingUp" : "BirdWingDown";
        }

        Texture wingBird = ContentManager.Get().GetTexture(birdTextureSignature);
        RenderManager.Get().DrawTexture(ref wingBird, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height, rotate_);
    }


    /**
     * @brief �� ������Ʈ�� �ٴڰ� �ε������� Ȯ���մϴ�.
     * 
     * @note �� ������Ʈ�� �ٴڰ� �ε����� ���¸� DONE���� �����մϴ�.
     */
    private void CheckCollisionFloor()
    {
        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;

        if (floor.Body.IsCollision(ref rigidBody_))
        {
            currentState_ = EState.DONE;
        }
    }


    /**
     * @brief �� ������Ʈ�� ���°� WAIT ������ �� ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateWaitState(float deltaSeconds)
    {
        if (currentState_ != EState.WAIT) return; // ���� ���°� WAIT�� �ƴϸ� �������� �ʽ��ϴ�.

        waitTime_ += deltaSeconds;
        if (waitTime_ > maxWaitTime_)
        {
            waitTime_ = 0.0f;
            waitMoveDirection_ *= -1.0f;
        }

        MovePosition(deltaSeconds);

        if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
        {
            currentState_ = EState.JUMP;
            rotate_ = MinRotate;
            bIsJump_ = true;
        }
    }


    /**
     * @brief �� ������Ʈ�� ���°� JUMP ������ �� ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateJumpState(float deltaSeconds)
    {
        if (currentState_ != EState.JUMP) return;

        MovePosition(deltaSeconds);
        UpdateWingState(deltaSeconds);
        UpdateJumpMoveState(deltaSeconds);

        if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
        {
            jumpMoveUpLength_ = 0.0f;
            jumpMoveDownLength_ = 0.0f;
            bIsJump_ = true;
        }
    }


    /**
     * @brief �� ������Ʈ�� ���°� FALL ������ �� ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateFallState(float deltaSeconds)
    {
        if (currentState_ != EState.FALL) return; // ���� ���°� FALL�� �ƴϸ� �������� �ʽ��ϴ�.

        rotate_ += (deltaSeconds * rotateSpeed_);
        rotate_ = Math.Min(rotate_, MaxRotate);

        MovePosition(deltaSeconds);

        if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
        {
            currentState_ = EState.JUMP;
            rotate_ = MinRotate;
            bIsJump_ = true;
        }
    }


    /**
     * @brief �� ������Ʈ�� �������� ���� �̵� ���¸� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateJumpMoveState(float deltaSeconds)
    {
        if (bIsJump_)
        {
            jumpMoveUpLength_ += (deltaSeconds * moveSpeed_);

            if (jumpMoveUpLength_ > jumpUpLength_)
            {
                bIsJump_ = false;
                jumpMoveUpLength_ = 0.0f;
            }
        }
        else
        {
            jumpMoveDownLength_ += (deltaSeconds * moveSpeed_);

            if (jumpMoveDownLength_ > jumpDownLength_)
            {
                currentState_ = EState.FALL;
                currWingState_ = EWing.UP;
                prevWingState_ = EWing.NORMAL;
                wingStateTime_ = 0.0f;
                jumpMoveDownLength_ = 0.0f;
            }
        }
    }


    /**
     * @brief �� ������Ʈ�� �̵���ŵ�ϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void MovePosition(float deltaSeconds)
    {
        Vector2<float> center = rigidBody_.Center;

        switch (currentState_)
        {
            case EState.WAIT:
                center.y += (waitMoveDirection_ * deltaSeconds * waitMoveLength_);
                rigidBody_.Center = center;
                break;

            case EState.JUMP:
                float jumpDirection = bIsJump_ ? -1.0f : 1.0f;
                center.y += (deltaSeconds * jumpDirection * moveSpeed_);
                center.y = Math.Max(center.y, 0);
                rigidBody_.Center = center;
                break;

            case EState.FALL:
                center.y += (deltaSeconds * moveSpeed_);
                rigidBody_.Center = center;
                break;
        }
    }


    /**
     * @brief �� ������Ʈ�� ���� ���¸� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void UpdateWingState(float deltaSeconds)
    {
        wingStateTime_ += deltaSeconds;

        if (wingStateTime_ > changeWingStateTime_)
        {
            wingStateTime_ = 0.0f;

            if (currWingState_ == EWing.NORMAL)
            {
                currWingState_ = GetCountWingState(prevWingState_);
                prevWingState_ = EWing.NORMAL;
            }
            else // currWingState_ == EWing.DOWN or currWingState_ == EWing.UP
            {
                prevWingState_ = currWingState_;
                currWingState_ = EWing.NORMAL;
            }
        }
    }


    /**
     * @brief ���� ������ �ݴ� ���¸� ����ϴ�.
     * 
     * @note ���ô� ������ �����ϴ�.
     * UP => DOWN, DOWN => UP, NORMAL => NORMAL
     */
    public static EWing GetCountWingState(EWing wingState)
    {
        if(wingState == EWing.NORMAL)
        {
            return EWing.NORMAL;
        }

        return (wingState == EWing.UP ? EWing.DOWN : EWing.UP);
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
    private float waitMoveLength_ = 20.0f;


    /**
     * @brief �� ������Ʈ�� ���� �� �����̴� �Ÿ��Դϴ�.
     */
    private float jumpUpLength_ = 70.0f;


    /**
     * @brief �� ������Ʈ�� ���� �� �������� �Ÿ��Դϴ�.
     */
    private float jumpDownLength_ = 40.0f;


    /**
     * @brief �� ������Ʈ�� �̵� �ӵ��Դϴ�.
     */
    private float moveSpeed_ = 350.0f;


    /**
     * @brief �� ������Ʈ�� ���� �����Դϴ�.
     * 
     * @note +�� �Ʒ� ����, -�� �� �����Դϴ�.
     */
    private bool bIsJump_ = false;


    /**
     * @brief �� ������Ʈ�� ���� �� ������ �Ÿ��Դϴ�.
     */
    private float jumpMoveUpLength_ = 0.0f;


    /**
     * @brief �� ������Ʈ�� ���� �� ������ �Ÿ��Դϴ�.
     */
    private float jumpMoveDownLength_ = 0.0f;


    /**
     * @brief ������ �� ������Ʈ�� ���� �����Դϴ�.
     */
    private EWing prevWingState_ = EWing.NORMAL;


    /**
     * @brief ���� �� ������Ʈ�� ���� �����Դϴ�.
     */
    private EWing currWingState_ = EWing.UP;


    /**
     * @brief �� ������Ʈ�� ���� ���°� ���ӵ� �ð��Դϴ�.
     */
    private float wingStateTime_ = 0.0f;


    /**
     * @brief �� ������Ʈ�� ���� ���¸� �����ϴ� �ð��Դϴ�.
     */
    private float changeWingStateTime_ = 0.1f;

    
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
}