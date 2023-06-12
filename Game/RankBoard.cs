using System.Collections.Generic;


/**
 * @brief �÷��̾��� ��ŷ�� �����ִ� ������Ʈ�Դϴ�.
 */
class RankBoard : GameObject
{
    /**
     * @brief �÷��̾��� ��ŷ�� �����ִ� ������Ʈ�� ���� Getter/Setter�Դϴ�.
     */
    public Vector2<float> BasePosition
    {
        get => basePosition_;
        set => basePosition_ = value;
    }

    public float Stride
    {
        get => stride_;
        set => stride_ = value;
    }

    public List<KeyValuePair<string, int>> TopPlayRank
    {
        get => topPlayRank_;
        set => topPlayRank_ = value;
    }

    public string NewGamePlayTime
    {
        get => newGamePlayTime_;
        set => newGamePlayTime_ = value;
    }

    public int NewCountPassPipe
    {
        get => newCountPassPipe_;
        set => newCountPassPipe_ = value;
    }


    /**
     * @brief �÷��̾��� ��ŷ�� �����ִ� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        TTFont font = ContentManager.Get().GetTTFont("SeoulNamsanEB");

        Vector2<float> position;
        position.x = basePosition_.x;
        position.y = basePosition_.y;

        RenderManager.Get().DrawText(ref font, "TOP 3", position, Color.BLUE);

        foreach(KeyValuePair<string, int> topPlayRank in topPlayRank_)
        {
            position.y += stride_;
            RenderManager.Get().DrawText(ref font, string.Format("{0} | {1}", topPlayRank.Key, topPlayRank.Value), position, Color.WHITE);
        }

        position.y += stride_;
        RenderManager.Get().DrawText(ref font, "YOUR", position, Color.RED);

        position.y += stride_;
        RenderManager.Get().DrawText(ref font, string.Format("{0} | {1}", newGamePlayTime_, newCountPassPipe_), position, Color.WHITE);
    }


    /**
     * @brief UI�� ���� ��ġ�Դϴ�.
     */
    private Vector2<float> basePosition_;


    /**
     * @brief ǥ�õ� ���ڿ� ���� ��ġ �����Դϴ�.
     */
    private float stride_;


    /**
     * @brief �÷��̾��� TOP 3 ��ŷ ����Դϴ�.
     */
    private List<KeyValuePair<string, int>> topPlayRank_ = null;


    /**
     * @brief ���� �ֱ��� ���� �÷��̰� ���� �ð��Դϴ�.
     */
    private string newGamePlayTime_;


    /**
     * @brief ���� �ֱٿ� �÷��̾ ����� �������� ���Դϴ�.
     */
    private int newCountPassPipe_ = 0;
}