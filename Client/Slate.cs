/**
 * @brief ���� ���� ������Ʈ UI ������Ʈ�Դϴ�.
 */
class Slate : GameObject
{
    /**
     * @brief ������Ʈ UI �Ӽ��� ���� Getter/Setter �Դϴ�.
     */
    public RigidBody UIBody
    {
        get => uiRigidBody_;
    }

    public string UITexture
    {
        get => uiTextureSignature_;
        set => uiTextureSignature_ = value;
    }


    /**
     * @brief ������Ʈ UI�� �ٵ� �����մϴ�.
     * 
     * @param center ȭ�� ���� UI �߽� ��ǥ�Դϴ�.
     * @param width UI �ٵ��� ���� ũ���Դϴ�.
     * @param height UI �ٵ��� ���� ũ���Դϴ�.
     */
    public void CreateUIBody(Vector2<float> center, float width, float height)
    {
        uiRigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief ���� ���� ������Ʈ UI ������Ʈ�� ������Ʈ�մϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        Texture uiSlateTexture = ContentManager.Get().GetTexture(uiTextureSignature_);
        RenderManager.Get().DrawTexture(ref uiSlateTexture, uiRigidBody_.Center, uiRigidBody_.Width, uiRigidBody_.Height);
    }


    /**
     * @brief UI ������Ʈ �ؽ�ó �ñ״�ó �Դϴ�.
     */
    private string uiTextureSignature_;


    /**
     * @brief UI ������Ʈ�� �ٵ��Դϴ�.
     */
    private RigidBody uiRigidBody_;
}