using System;
using SDL2;


/**
 * @brief ������ ��׶��� ������Ʈ�Դϴ�.
 */
class Background : GameObject
{
    /**
     * @brief ������ ��׶��� �Ӽ��� ���� Getter/Setter �Դϴ�.
     */
    public RigidBody Body
    {
        get => rigidBody_;
    }


    /**
     * @brief ��׶����� �ٵ� �����մϴ�.
     * 
     * @param center ��׶��� �ٵ��� �߽� ��ǥ�Դϴ�.
     * @param width ��׶��� �ٵ��� ���� ũ���Դϴ�.
     * @param height ��׶��� �ٵ��� ���� ũ���Դϴ�.
     */
    public void CreateBody(Vector2<float> center, float width, float height)
    {
        rigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Update(float deltaSeconds)
    {
    }


    /**
     * @brief ��׶��� ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     */
    public override void Render()
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