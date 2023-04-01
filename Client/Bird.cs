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
     * @brief �� ������Ʈ�� ȸ�� �����Դϴ�.
     */
    private float rotate_ = 0.0f;


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody rigidBody_;


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� �ؽ�ó�Դϴ�.
     */
    private Texture texture_;
}