using System.Collections.Generic;


/**
 * @brief ���� ���� ���¸� �����ϴ� ���� ������Ʈ�Դϴ�.
 */
class GameStateDetector : GameObject
{
    /**
     * @brief ���� ���� ���� ���¸� ��Ÿ���� �������Դϴ�.
     */
    public enum EGameScene
    {
        START = 0x00,
        READY = 0x01,
        PLAY = 0x02,
        DONE = 0x03,
    }


    /**
     * @brief ���� ������ ���¸� ��Ÿ���¿������Դϴ�.
     */
    public enum EGameState
    {
        WAIT = 0x00,
        ENTRY = 0x01,
        PROCESSING = 0x02,
        LEAVE = 0x03,
    }


    /**
     * @brief ���� ���� ���¸� �����ϴ� ���� ������Ʈ�� Setter/Getter�Դϴ�.
     */
    public EGameScene GameScene
    {
        get => gameScene_;
    }

    public EGameState GameState
    {
        get => currGameState_;
        set => currGameState_ = value;
    }


    /**
     * @brief ���� ���� ���¸� �����ϴ� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        switch(gameScene_)
        {
            case EGameScene.START:
                UpdateStartScene(deltaSeconds);
                break;

            case EGameScene.READY:
                UpdateReadyScene(deltaSeconds);
                break;

            case EGameScene.PLAY:
                UpdatePlayScene(deltaSeconds);
                break;

            case EGameScene.DONE:
                UpdateDoneScene(deltaSeconds);
                break;
        }
    }


    /**
     * @brief ������ ���°� ����Ǿ����� Ȯ���մϴ�.
     * 
     * @return ������ ���°� ����Ǿ��ٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    private bool IsSwitchGameState()
    {
        return prevGameState_ != currGameState_;
    }


    /**
     * @brief ������ ���۾����� ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateStartScene(float deltaSeconds)
    {
        if (!IsSwitchGameState()) return;
    }


    /**
     * @brief ������ �غ������ ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateReadyScene(float deltaSeconds)
    {
        if (!IsSwitchGameState()) return;
    }


    /**
     * @brief ������ �÷��̾����� ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdatePlayScene(float deltaSeconds)
    {
        if (!IsSwitchGameState()) return;
    }


    /**
     * @brief ������ ��������� ������Ʈ�� �����մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    private void UpdateDoneScene(float deltaSeconds)
    {
        if (!IsSwitchGameState()) return;
    }


    /**
     * @brief ���� ���� ���Դϴ�.
     */
    private EGameScene gameScene_ = EGameScene.START;


    /**
     * @brief ������ ���� �����Դϴ�.
     */
    private EGameState prevGameState_ = EGameState.WAIT;


    /**
     * @brief ���� ���� �����Դϴ�.
     */
    private EGameState currGameState_ = EGameState.WAIT;
}