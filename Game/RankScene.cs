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
     * @brief ���� �÷��̰� ���� �ð��Դϴ�.
     */
    private string gamePlayTime_;


    /**
     * @brief �÷��̾ ����� �������� ���Դϴ�.
     */
    private int countPassPipe_ = 0;
}