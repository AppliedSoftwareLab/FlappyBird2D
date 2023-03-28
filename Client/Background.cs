using System;
using SDL2;


/**
 * @brief ������ ��׶��� ������Ʈ�Դϴ�.
 */
class Background : IGameObject
{
    /**
     * @brief ��׶��� �ؽ�ó�� �����մϴ�.
     * 
     * @note �̹� �ؽ�ó�� �ε��Ǿ� �ִٸ�, ������ �ؽ�ó ���ҽ��� �����մϴ�.
     * 
     * @param BGTexture ������ ��׶��� �ؽ�ó�Դϴ�.
     * 
     * @throws �ؽ�ó ���ҽ� ������ �����ϸ� ǥ�� ���ܸ� �����ϴ�.
     */
    public void SetTexture(Texture BGTexture)
    {
        BGTexture_ = BGTexture;
    }


    /**
     * @brief ��׶��� ������Ʈ�� ��ü�� �����մϴ�.
     * 
     * @note �̹� �����Ǿ� �ִٸ�, ������ ��ü�� ���� ���ϴ�.
     * 
     * @param BackgroundRigidBody ������ ��ü�Դϴ�.
     */
    public void SetRigidBody(RigidBody BackgroundRigidBody)
    {
        RigidBody_ = BackgroundRigidBody;
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param DeltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float DeltaSeconds)
    {
        
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     * 
     * @param Renderer ������Ʈ�� ȭ�鿡 �׸��� ���� �������Դϴ�.
     */
    public void Render(IntPtr Renderer)
    {
        SDL.SDL_Rect BGSpriteRect;
        BGSpriteRect.x = (int)(RigidBody_.Center.X - RigidBody_.Width / 2.0f);
        BGSpriteRect.y = (int)(RigidBody_.Center.Y - RigidBody_.Height / 2.0f);
        BGSpriteRect.w = (int)(RigidBody_.Width);
        BGSpriteRect.h = (int)(RigidBody_.Height);

        SDL.SDL_RenderCopy(
            Renderer,
            BGTexture_.Resource,
            IntPtr.Zero,
            ref BGSpriteRect
        );
    }


    /**
     * @brief ��׶��� ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        BGTexture_.Release();
    }


    /**
     * @brief ���� ��׶��� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody RigidBody_;


    /**
     * @brief ������ ��׶��� ������Ʈ �ؽ�ó�Դϴ�.
     */
    private Texture BGTexture_;
}