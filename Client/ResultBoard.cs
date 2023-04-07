/**
 * @brief ������ ����� ����ϴ� ���� ������Ʈ�Դϴ�.
 */
class ResultBoard : GameObject
{
    /**
     * @brief ���� ���ھ� ���� ������Ʈ�� �Ӽ��� ���� Getter/Setter�Դϴ�.
     */
    public int PlayerID
    {
        get => playerID_;
        set => playerID_ = value;
    }

    public RigidBody BoardBody
    {
        get => rigidBody_;
    }


    /**
     * @brief ������ ����� ����ϴ� ���� ������Ʈ�� �ٵ� �����մϴ�.
     * 
     * @param center ������ ����� ����ϴ� ���� ������Ʈ�� ȭ�� �� �߽� ��ǥ�Դϴ�.
     * @param width ������ ����� ����ϴ� ���� ������Ʈ�� ���� ũ���Դϴ�.
     * @param height ������ ����� ����ϴ� ���� ������Ʈ�� ���� ũ���Դϴ�.
     */
    public void CreateBody(Vector2<float> center, float width, float height)
    {
        rigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief ������ ����� ����ϴ� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        Texture slateTexture = ContentManager.Get().GetTexture("Slate");
        RenderManager.Get().DrawTexture(ref slateTexture, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height);

        Vector2<float> center;
        float width = 200.0f, height = 50.0f;

        center.x = 400.0f;
        center.y = 350.0f;
        Texture userIDTexture = ContentManager.Get().GetTexture("UserID");
        RenderManager.Get().DrawTexture(ref userIDTexture, center, width, height);

        center.x = 400.0f;
        center.y = 450.0f;
        Texture scoreTexture = ContentManager.Get().GetTexture("Score");
        RenderManager.Get().DrawTexture(ref scoreTexture, center, width, height);
    }


    /**
     * @brief �÷��̾��� ���̵��Դϴ�.
     */
    int playerID_ = 0;


    /**
     * @brief �÷��̾� ���̵� ǥ�õ� ��ġ�Դϴ�.
     */
    Vector2<float> playerIDPosition_;


    /**
     * @brief �÷��̾� ���ھ ǥ�õ� ��ġ�Դϴ�.
     */
    Vector2<float> playerScorePosition_;


    /**
     * @brief ����� ����ϴ� ������ �ٵ��Դϴ�.
     */
    RigidBody rigidBody_;
}