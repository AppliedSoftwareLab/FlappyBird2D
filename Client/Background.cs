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


    /**
     * @brief ��׶��� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float deltaSeconds)
    {
        
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     * 
     * @param Renderer ������Ʈ�� ȭ�鿡 �׸��� ���� �������Դϴ�.
     */
    public void Render(IntPtr renderer)
    {
        SDL.SDL_Rect backgroundRect;
        backgroundRect.x = (int)(rigidBody_.Center.x - rigidBody_.Width / 2.0f);
        backgroundRect.y = (int)(rigidBody_.Center.y - rigidBody_.Height / 2.0f);
        backgroundRect.w = (int)(rigidBody_.Width);
        backgroundRect.h = (int)(rigidBody_.Height);

        SDL.SDL_RenderCopy(
            renderer,
            texture_.Resource,
            IntPtr.Zero,
            ref backgroundRect
        );
    }


    /**
     * @brief ��׶��� ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        texture_.Release();
    }


    /**
     * @brief ���� ��׶��� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody rigidBody_;


    /**
     * @brief ������ ��׶��� ������Ʈ �ؽ�ó�Դϴ�.
     */
    private Texture texture_;
}