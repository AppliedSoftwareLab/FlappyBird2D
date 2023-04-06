using System.Collections.Generic;


/**
 * @brief ���� ���� ���Դϴ�.
 */
class Scene : GameObject
{
    /**
     * @brief ���� �� ���� �Ӽ��� ���� Getter/Setter�Դϴ�.
     */
    public SceneNode CurrSceneNode
    {
        get => currentSceneNode_;
        set => currentSceneNode_ = value;
    }


    /**
     * @brief �� ��带 �߰��մϴ�.
     * 
     * @param sceneNode �߰��� �� ����Դϴ�.
     */
    public void AddSceneNode(SceneNode sceneNode)
    {
        sceneNodes_.AddLast(sceneNode);
    }


    /**
     * @brief �� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        if(currentSceneNode_.DetectSwitch)
        {
            currentSceneNode_.Leave();
            currentSceneNode_ = currentSceneNode_.NextSceneNode;
            currentSceneNode_.Entry();
        }
    }

    
    /**
     *@brief ���� ���� ����Ű�� �� ����Դϴ�.
     */
    private SceneNode currentSceneNode_ = null;


    /**
     * @brief �� ���� ��� ����Դϴ�.
     */
    private LinkedList<SceneNode> sceneNodes_ = new LinkedList<SceneNode>();
}