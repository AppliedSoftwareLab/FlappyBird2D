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

    public float UIWidth
    {
        get => uiWidth_;
        set => uiWidth_ = value;
    }

    public float UIHeight
    {
        get => uiHeight_;
        set => uiHeight_ = value;
    }

    public Vector2<float> PlayerIDPosition
    {
        get => playerIDPosition_;
        set => playerIDPosition_ = value;
    }

    public Vector2<float> PlayerScorePosition
    {
        get => playerScorePosition_;
        set => playerScorePosition_ = value;
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

        Texture userIDTexture = ContentManager.Get().GetTexture("UserID");
        RenderManager.Get().DrawTexture(ref userIDTexture, playerIDPosition_, uiWidth_, uiHeight_);

        Texture scoreTexture = ContentManager.Get().GetTexture("Score");
        RenderManager.Get().DrawTexture(ref scoreTexture, playerScorePosition_, uiWidth_, uiHeight_);
    }


    /**
     * @brief �÷��̾��� ���̵��Դϴ�.
     */
    int playerID_ = 0;


    /**
     * @brief ���� ���� UI ���� ũ���Դϴ�.
     */
    float uiWidth_ = 0.0f;


    /**
     * @brief ���� ���� UI ���� ũ���Դϴ�.
     */
    float uiHeight_ = 0.0f;


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