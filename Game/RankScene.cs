using System;
using System.Data;
using System.Collections.Generic;


/**
 * @brief �÷��̾��� ��ŷ�� �����ִ� ���Դϴ�.
 */
class RankScene : Scene
{
    /**
     * @brief ��ŷ ���� ���� Getter/Setter �Դϴ�.
     */
    public string GamePlayTime
    {
        get => gamePlayTime_;
        set => gamePlayTime_ = value;
    }

    public int CountPassPipe
    {
        get => countPassPipe_;
        set => countPassPipe_ = value;
    }


    /**
     * @brief �÷��̾��� ��ŷ ���� �����մϴ�.
     */
    public override void Entry()
    {
        base.Entry();

        gameObjectSignatures_ = new List<string>();
        gameObjectSignatures_.Add("OkButton");

        Button okButton = new Button();
        okButton.UpdateOrder = 6;
        okButton.Active = true;
        okButton.UITexture = "OkButton";
        okButton.EventAction = () =>
        {
            DetectSwitch = true;

            Sound doneSound = ContentManager.Get().GetSound("Done") as Sound;
            doneSound.Reset();
            doneSound.Play();
        };
        okButton.ReduceRatio = 0.95f;
        okButton.CreateUIBody(new Vector2<float>(500.0f, 600.0f), 160.0f, 60.0f);

        List<KeyValuePair<string, int>> topPlayRank = GetTop3PlayRank();

        WorldManager.Get().AddGameObject("OkButton", okButton);
    }


    /**
     * @brief �÷����� ��ŷ �����κ��� �����ϴ�.
     */
    public override void Leave()
    {
        CleanupGameObjects();
        base.Leave();
    }


    /**
     * @brief �����ͺ��̽��κ��� ���� 3���� ����� �����ɴϴ�.
     * 
     * @return ���� 3���� ����� ��ȯ�մϴ�.
     */
    private List<KeyValuePair<string, int>> GetTop3PlayRank()
    {
        List<KeyValuePair<string, int>> topPlayRank = new List<KeyValuePair<string, int>>();

        Database playRank = ContentManager.Get().GetDatabase("PlayRank");
        DataSet playRankDataSet = playRank.Select("SELECT * FROM PlayRank ORDER BY Pipe DESC LIMIT 3;");
        DataTable playRankDataTable = playRankDataSet.Tables[0];

        foreach (DataRow dataRows in playRankDataTable.Rows)
        {
            string playTime = (string)(dataRows.ItemArray[0]);
            Int64 pipe = (Int64)(dataRows.ItemArray[1]);

            topPlayRank.Add(new KeyValuePair<string, int>(playTime, (int)pipe));
        }

        return topPlayRank;
    }


    /**
     * @brief ���� �÷��̰� ���� �ð��Դϴ�.
     */
    private string gamePlayTime_;


    /**
     * @brief �÷��̾ ����� �������� ���Դϴ�.
     */
    private int countPassPipe_ = 0;
}