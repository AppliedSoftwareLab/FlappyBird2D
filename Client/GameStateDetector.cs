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
        get => gameState_;
    }


    /**
     * @brief ���� ���� ���¸� �����ϴ� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {

    }


    /**
     * @brief ���� ���� ���Դϴ�.
     */
    private EGameScene gameScene_ = EGameScene.START;


    /**
     * @brief ���� ���� �����Դϴ�.
     */
    private EGameState gameState_ = EGameState.WAIT;
}