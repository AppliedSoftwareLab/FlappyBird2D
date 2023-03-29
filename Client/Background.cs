using System;
using SDL2;


/**
 * @brief ������ ��׶��� ������Ʈ�Դϴ�.
 */
class Background : IGameObject
{
    /**
     * @brief ������ ��׶��� �Ӽ��� ���� Getter/Setter �Դϴ�.
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

    public bool Plable
    {
        get => bIsPlay_;
        set => bIsPlay_ = value;
    }

    public float PlayTime
    {
        get => playTime_;
        set => playTime_ = value;
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float deltaSeconds)
    {
        if(bIsPlay_)
        {
            playTime_ += deltaSeconds;
        }
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     */
    public void Render()
    {
        RenderManager.Get().DrawTexture(ref texture_, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height);
    }


    /**
     * @brief ��׶��� ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        texture_.Release();
    }


    /**
     * @brief ���� ������ �÷��������� Ȯ���մϴ�.
     */
    private bool bIsPlay_ = false;


    /**
     * @brief �÷����� �ð��Դϴ�.
     */
    private float playTime_ = 0.0f;


    /**
     * @brief ���� ��׶��� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody rigidBody_;


    /**
     * @brief ������ ��׶��� ������Ʈ �ؽ�ó�Դϴ�.
     */
    private Texture texture_;
}