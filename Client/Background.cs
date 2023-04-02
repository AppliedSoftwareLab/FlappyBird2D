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
    public RigidBody BackgroundBody
    {
        get => rigidBody_;
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
     * @brief ��׶����� �ٵ� �����մϴ�.
     * 
     * @param center ��׶��� �ٵ��� �߽� ��ǥ�Դϴ�.
     * @param width ��׶��� �ٵ��� ���� ũ���Դϴ�.
     * @param height ��׶��� �ٵ��� ���� ũ���Դϴ�.
     */
    public void CreateBackgroundBody(Vector2<float> center, float width, float height)
    {
        rigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     */
    public void Render()
    {
        Texture backgroundTexture = ContentManager.Get().GetTexture("Background");

        RenderManager.Get().DrawTexture(
            ref backgroundTexture, 
            rigidBody_.Center, 
            rigidBody_.Width, 
            rigidBody_.Height
        );
    }


    /**
     * @brief ���� ��׶��� ������Ʈ�� ��ü�Դϴ�.
     */
    private RigidBody rigidBody_;
}