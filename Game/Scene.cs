using System.Collections.Generic;


/**
 * @brief ���� ���� ���Դϴ�.
 */
class Scene : GameObject
{
    /**
     * @brief ���� �� ���� �Ӽ��� ���� Getter/Setter�Դϴ�.
     */
    public bool DetectSwitch
    {
        get => bIsDetectSwitch_;
        set => bIsDetectSwitch_ = value;
    }

    public Scene NextScene
    {
        get => nextScene_;
        set => nextScene_ = value;
    }


    /**
     * @brief ���� �����մϴ�.
     * 
     * @note �� �޼���� ��ӹ޴� ���� Ŭ�������� �����ؾ� �մϴ�.
     */
    public virtual void Entry() 
    {
        Active = true;
    }


    /**
     * @brief �����κ��� �����ϴ�.
     * 
     * @note �� �޼���� ��ӹ޴� ���� Ŭ�������� �����ؾ� �մϴ�.
     */
    public virtual void Leave() 
    {
        NextScene.Entry();

        DetectSwitch = false;
        Active = false;
    }


    /**
     * @brief �� ���� ���� ������Ʈ�� �����մϴ�.
     */
    public void CleanupGameObjects()
    {
        foreach (string gameObjectSignature in gameObjectSignatures_)
        {
            WorldManager.Get().RemoveGameObject(gameObjectSignature);
        }
    }


    /**
     * @brief �� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @note �� �޼���� ��ӹ޴� ���� Ŭ�������� �����ؾ� �մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds) 
    {
        if(bIsDetectSwitch_)
        {
            Leave();
        }
    }


    /**
     * @brief ���� ��ȯ�� Ȯ���մϴ�.
     */
    protected bool bIsDetectSwitch_ = false;


    /**
     * @brief ���� ����� ���� ���Դϴ�.
     */
    protected Scene nextScene_ = null;


    /**
     * @brief �� ���� ���� ������Ʈ �ñ״�ó�Դϴ�.
     */
    protected List<string> gameObjectSignatures_ = null;
}