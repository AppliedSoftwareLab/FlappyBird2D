using System;
using System.Collections.Generic;


/**
 * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�Դϴ�.
 */
class Bird : GameObject
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

    public bool Movable
    {
        get => bCanMove_;
        set => bCanMove_ = value;
    }

    public int PassPipe
    {
        get => countPassPipe_;
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
    public override void Tick(float deltaSeconds)
    {
        if (bCanMove_)
        {
            switch (currentState_)
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

                case EState.DONE:
                    UpdateDoneState(deltaSeconds);
                    break;
            }
        }

        string birdTextureSignature = "BirdWingNormal";

        if (currWingState_ != EWing.NORMAL)
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
            PlaySound("Hit");
        }

        if(currentState_ == EState.DONE)
        {
            StopRelatedGameObject();
        }
    }


    /**
     * @brief �� ������Ʈ�� �������� �ε������� Ȯ���մϴ�.
     * 
     * @note �� ������Ʈ�� �������� �ε����� ���¸� DONE���� �����մϴ�.
     */
    private void CheckCollisionPipe()
    {
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
        List<Pipe> pipes = pipeDetector.DetectPipes;

        foreach(Pipe pipe in pipes)
        {
            if(pipe.TopRigidBody.IsCollision(ref rigidBody_) || pipe.BottomRigidBody.IsCollision(ref rigidBody_))
            {
                currentState_ = EState.DONE;
                PlaySound("Hit");
            }
        }

        if (currentState_ == EState.DONE)
        {
            StopRelatedGameObject();
        }
    }


    /**
     * @brief �� ������Ʈ�� ����� �������� Ȯ���մϴ�.
     */
    private void CheckPassPipe()
    {
        float birdCenterX = rigidBody_.Center.x;

        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
        List<Pipe> pipes = pipeDetector.DetectPipes;
        float pipeWidth = 0.0f;
        float pipeCenterX = 0.0f;

        foreach (Pipe pipe in pipes)
        {
            pipeWidth = pipe.TopRigidBody.Width;
            pipeCenterX = pipe.TopRigidBody.Center.x;

            if(Math.Abs(birdCenterX - pipeCenterX) <= 1.0f && !pipe.PassBird)
            {
                pipe.PassBird = true;
                countPassPipe_++;

                Number birdScore = WorldManager.Get().GetGameObject("BirdScore") as Number;
                birdScore.NumberContext = countPassPipe_;

                PlaySound("Coin");
            }
        }
    }


    /**
     * @brief �� ������Ʈ�� ���� �ִ� ���� ������Ʈ�� �������� �ߴ��մϴ�.
     */
    private void StopRelatedGameObject()
    {
        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
        List<Pipe> pipes = pipeDetector.DetectPipes;

        floor.Movable = false;
        pipeDetector.CanGeneratePipe = false;

        foreach (Pipe pipe in pipes)
        {
            pipe.Movable = false;
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
        UpdateWingState(deltaSeconds);

        if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
        {
            currentState_ = EState.JUMP;
            rotate_ = MinRotate;
            bIsJump_ = true;

            Scene scene = WorldManager.Get().GetGameObject("Scene") as Scene;
            SceneNode currentSceneNode = scene.CurrSceneNode;
            currentSceneNode.DetectSwitch = true;

            PlaySound("Jump");
        }

        CheckCollisionPipe();
        CheckCollisionFloor();
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

            PlaySound("Jump");
        }

        CheckPassPipe();
        CheckCollisionPipe();
        CheckCollisionFloor();
    }


    /**
     * @brief �� ������Ʈ�� ���°� FALL ������ �� ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateFallState(float deltaSeconds)
    {
        if (currentState_ != EState.FALL) return; // ���� ���°� FALL�� �ƴϸ� �������� �ʽ��ϴ�.

        MovePosition(deltaSeconds);

        if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
        {
            currentState_ = EState.JUMP;
            rotate_ = MinRotate;
            bIsJump_ = true;

            PlaySound("Jump");
        }

        CheckPassPipe();
        CheckCollisionPipe();
        CheckCollisionFloor();
    }


    /**
     * @brief �� ������Ʈ�� DONE ������ �� ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateDoneState(float deltaSeconds)
    {
        if (currentState_ != EState.DONE) return;

        MovePosition(deltaSeconds);

        Background background = WorldManager.Get().GetGameObject("Background") as Background;
        bIsLeaveInBackground_ = !background.Body.IsCollision(ref rigidBody_);

        if(bIsLeaveInBackground_)
        {
            Scene scene = WorldManager.Get().GetGameObject("Scene") as Scene;
            SceneNode currentSceneNode = scene.CurrSceneNode;

            if (currentSceneNode is PlaySceneNode)
            {
                currentSceneNode.DetectSwitch = true;
            }
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
                rotate_ += (deltaSeconds * rotateSpeed_);
                rotate_ = Math.Min(rotate_, MaxRotate);
                center.y += (deltaSeconds * moveSpeed_);
                rigidBody_.Center = center;
                break;

            case EState.DONE:
                if(!bIsLeaveInBackground_)
                {
                    center.y += (deltaSeconds * moveSpeed_);
                    rigidBody_.Center = center;
                    rotate_ += (deltaSeconds * rotateSpeed_);
                }
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
     * @brief ���带 �÷����մϴ�.
     */
    private void PlaySound(string signature)
    {
        Sound sound = ContentManager.Get().GetSound(signature) as Sound;
        sound.Reset();
        sound.Play();
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
     * @brief �� ������Ʈ�� ������ �� �ִ��� Ȯ���մϴ�.
     */
    private bool bCanMove_ = true;


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
     * @brief �� ������Ʈ�� ��׶��� ������ �������� Ȯ���մϴ�.
     */
    private bool bIsLeaveInBackground_ = false;


    /**
     * @brief �� ������Ʈ�� ����� �������� ���Դϴ�.
     */
    private int countPassPipe_ = 0;


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