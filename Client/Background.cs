using System;
using SDL2;


/**
 * @brief ������ ��׶��� ������Ʈ�Դϴ�.
 */
class Background : IGameObject
{
    /**
     * @brief ������ ��׶��� ������Ʈ �Ӽ��� ���� Getter/Setter �Դϴ�.
     */
    public Vector2<float> Center
    {
        get => Center_;
        set => Center_ = value;
    }

    public float Width
    {
        get => Width_;
        set => Width_ = value;
    }

    public float Height
    {
        get => Height_;
        set => Height_ = value;
    }


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
        BGSpriteRect.x = (int)(Center_.X - Width_ / 2.0f);
        BGSpriteRect.y = (int)(Center_.Y - Height_ / 2.0f);
        BGSpriteRect.w = (int)(Width_);
        BGSpriteRect.h = (int)(Height_);

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
     * @brief ������ ��׶��� ������Ʈ �߽� ��ǥ�Դϴ�.
     */
    private Vector2<float> Center_;


    /**
     * @brief ������ ��׶��� ������Ʈ ���� ũ���Դϴ�.
     */
    private float Width_ = 0.0f;


    /**
     * @brief ������ ��׶��� ������Ʈ ���� ũ���Դϴ�.
     */
    private float Height_ = 0.0f;


    /**
     * @brief ������ ��׶��� ������Ʈ �ؽ�ó�Դϴ�.
     */
    private Texture BGTexture_;
}