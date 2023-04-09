/**
 * @brief ���� �� ���� ����� �߻� Ŭ�����Դϴ�.
 */
abstract class SceneNode
{
    /**
     * @brief ���� �� ���� ����� �߻� Ŭ���� �Ӽ��� ���� Getter/Setter�Դϴ�.
     */
    public bool DetectSwitch
    {
        get => bIsDetectSwitch_;
        set => bIsDetectSwitch_ = value;
    }

    public SceneNode NextSceneNode
    {
        get => nextSceneNode_;
        set => nextSceneNode_ = value;
    }


    /**
     * @brief �� ��忡 �����մϴ�.
     */
    public abstract void Entry();


    /**
     * @brief �� ���� ���� �����ϴ�.
     */
    public abstract void Leave();


    /**
     * @brief �� ����� ��ȯ�� Ȯ���մϴ�.
     */
    private bool bIsDetectSwitch_ = false;


    /**
     * @brief �� ��忡 ����� ���� ����Դϴ�.
     */
    private SceneNode nextSceneNode_ = null;
}