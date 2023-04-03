using System.Collections.Generic;


/**
 * @brief ���� ���� ���¸� �����ϴ� ���� ������Ʈ�Դϴ�.
 */
class GameStateDetector : GameObject
{
    /**
     * @brief ���� ���� ���¸� ��Ÿ���� �������Դϴ�.
     */
    public enum EGameState
    {
        READY = 0x01,
        PLAY = 0x02,
        DONE = 0x03,
    }


    /**
     * @brief ���� ���� ���¸� �����ϴ� ���� ������Ʈ�� Setter/Getter�Դϴ�.
     */
    public EGameState GameState
    {
        get => gameState_;
    }


    /**
     * @brief ���� ���� ���¸� �����ϴ� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Update(float deltaSeconds)
    {
        switch(gameState_)
        {
            case EGameState.READY:
                UpdateReadyState(deltaSeconds);
                break;

            case EGameState.PLAY:
                UpdatePlayState(deltaSeconds);
                break;
        }
    }


    /**
     * @brief ���� ���� ���¸� �����ϴ� ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     * 
     * @note ���� ���¸� �����ϴ� ���� ������Ʈ�� ȭ�鿡 �������� �������� �ʽ��ϴ�.
     */
    public override void Render() {}


    /**
     * @brief ��� ���� ������ ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateReadyState(float deltaSeconds)
    {
        if (gameState_ != EGameState.READY) return;

        bool bIsDetectSpace = InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED;

        if (bIsDetectSpace)
        {
            gameState_ = EGameState.PLAY;

            PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
            pipeDetector.CanGeneratePipe = true;
        }
    }


    /**
     * @brief �÷��� ���� ������ ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdatePlayState(float deltaSeconds)
    {
        if (gameState_ != EGameState.PLAY) return;

        Bird bird = WorldManager.Get().GetGameObject("Bird") as Bird;
        if (bird.State == Bird.EState.DONE)
        {
            Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
            floor.Movable = false;

            PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
            pipeDetector.CanGeneratePipe = false;

            List<Pipe> pipes = pipeDetector.DetectPipes;
            foreach (Pipe pipe in pipes)
            {
                pipe.Movable = false;
            }

            gameState_ = EGameState.DONE;
        }
    }


    /**
     * @brief ���� ���� �����Դϴ�.
     */
    EGameState gameState_ = EGameState.READY;
}