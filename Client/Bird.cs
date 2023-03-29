using System;
using SDL2;


/**
 * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�Դϴ�.
 */
class Bird : IGameObject
{
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

    public bool Movable
    {
        get => bIsMove_;
        set => bIsMove_ = value;
    }


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float deltaSeconds)
    {
        if (bIsMove_)
        {
        }
    }
    

    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     * 
     * @param renderer ������Ʈ�� ȭ�鿡 �׸��� ���� �������Դϴ�.
     */
    public void Render(IntPtr renderer)
    {
        RenderManager.Get().DrawTexture(ref texture_, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height);
    }


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        texture_.Release();
    }


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ������ �� �ִ��� Ȯ���մϴ�.
     */
    private bool bIsMove_ = false;


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody rigidBody_;


    /**
     * @brief ������ �÷��̾ �����ϴ� �� ������Ʈ�� �ؽ�ó�Դϴ�.
     */
    private Texture texture_;
}