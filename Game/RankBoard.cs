using System.Collections.Generic;


/**
 * @brief �÷��̾��� ��ŷ�� �����ִ� ������Ʈ�Դϴ�.
 */
class RankBoard : GameObject
{
    /**
     * @brief �÷��̾��� ��ŷ�� �����ִ� ������Ʈ�� ���� Getter/Setter�Դϴ�.
     */
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

    }


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