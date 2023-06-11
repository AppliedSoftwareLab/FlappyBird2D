using System.Collections.Generic;


/**
 * @brief ������ ���� �� ����Դϴ�.
 */
class DoneScene : Scene
{
    /**
     * @brief ������ ���� �� ��忡 �����մϴ�.
     */
    public override void Entry()
    {
        Active = true; // �� Ȱ��ȭ

        WorldManager.Get().RemoveGameObject("PressButton");

        gameObjectSignatures_ = new List<string>();

        gameObjectSignatures_.Add("GameOverSlate");
        gameObjectSignatures_.Add("OkButton");
        gameObjectSignatures_.Add("ResultBoard");
        gameObjectSignatures_.Add("Floor");
        gameObjectSignatures_.Add("Bird");
        gameObjectSignatures_.Add("PipeDetector");
        gameObjectSignatures_.Add("BirdScore");

        Number birdScore = WorldManager.Get().GetGameObject("BirdScore") as Number;
        birdScore.UpdateOrder = 7;
        birdScore.Active = true;

        Vector2<float> center = birdScore.Center;
        center.x = 650.0f;
        center.y = 400.0f;
        birdScore.Center = center;

        SlideSlate gameOverSlate = new SlideSlate();
        gameOverSlate.UpdateOrder = 6;
        gameOverSlate.Active = true;
        gameOverSlate.UITexture = "GameOver";
        gameOverSlate.Movable = true;
        gameOverSlate.MaxWaitTimeForMove = 1.0f;
        gameOverSlate.MoveLength = 20.0f;
        gameOverSlate.CreateUIBody(new Vector2<float>(500.0f, 200.0f), 400.0f, 100.0f);

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
        okButton.CreateUIBody(new Vector2<float>(500.0f, 550.0f), 160.0f, 60.0f);

        ResultBoard resultBoard = new ResultBoard();
        resultBoard.UpdateOrder = 6;
        resultBoard.Active = true;
        resultBoard.UIWidth = 200.0f;
        resultBoard.UIHeight = 50.0f;
        resultBoard.PlayerScorePosition = new Vector2<float>(400.0f, 400.0f);
        resultBoard.CreateBody(new Vector2<float>(500.0f, 400.0f), 500.0f, 200.0f);

        WorldManager.Get().AddGameObject("GameOverSlate", gameOverSlate);
        WorldManager.Get().AddGameObject("OkButton", okButton);
        WorldManager.Get().AddGameObject("ResultBoard", resultBoard);
    }


    /**
     * @brief ������ ���� �� ��忡�� �����ϴ�.
     */
    public override void Leave()
    {
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
        List<Pipe> pipes = pipeDetector.DetectPipes;
        foreach (Pipe pipe in pipes)
        {
            string pipeSignature = string.Format("Pipe{0}", pipe.SignatureNumber);
            WorldManager.Get().RemoveGameObject(pipeSignature);
        }

        foreach (string gameObjectSignature in gameObjectSignatures_)
        {
            WorldManager.Get().RemoveGameObject(gameObjectSignature);
        }

        NextScene.Entry();

        DetectSwitch = false;
        Active = false; // �� ��Ȱ��ȭ
    }


    /**
     * @brief ������ ���� ���� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        if (DetectSwitch)
        {
            Leave();
        }
    }
}