using System;
using System.Collections.Generic;
using System.Linq;


/**
 * @brief ���� ������ ���� ������Ʈ�� �����ϴ� �̱��� Ŭ�����Դϴ�.
 */
class WorldManager
{
    /**
     * @brief ���� ������Ʈ�� �����ϴ� ���� �Ŵ����� �ν��Ͻ��� ����ϴ�.
     * 
     * @return ���� ������Ʈ�� �����ϴ� ���� �Ŵ����� �ν��Ͻ��� ��ȯ�մϴ�.
     */
    public static WorldManager Get()
    {
        if(worldManager_ == null)
        {
            worldManager_ = new WorldManager();
        }

        return worldManager_;
    }


    /**
     * @brief ���� �Ŵ����� �ʱ�ȭ�մϴ�.
     */
    public void Setup()
    {
        if (bIsSetup_) return;

        gameObjects_ = new Dictionary<string, GameObject>();

        bIsSetup_ = true;
    }


    /**
     * @brief ���� �Ŵ����� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        if (!bIsSetup_) return;

        bIsSetup_ = false;
    }


    /**
     * @brief �ñ״�ó ���� �����ϴ� ���� ������Ʈ�� �ִ��� Ȯ���մϴ�.
     * 
     * @param signature ���� ������Ʈ�� �ִ��� Ȯ���� �ñ״�ó ���Դϴ�.
     * 
     * @return �ñ״�ó ���� �����ϴ� ���� ������Ʈ�� �ִٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    public bool IsValid(string signature)
    {
        return gameObjects_.ContainsKey(signature);
    }


    /**
     * @brief ���� �Ŵ����� �����ϴ� ������Ʈ�� ������Ʈ�ϰ� �������մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Tick(float deltaSeconds)
    {
        IEnumerable<GameObject> gameObjects = from gameObject in gameObjects_
                          orderby gameObject.Value.UpdateOrder
                          select gameObject.Value;

        foreach (GameObject gameObject in gameObjects)
        {
            if(gameObject.Active)
            {
                gameObject.Tick(deltaSeconds);
            }
        }
    }


    /**
     * @brief ���忡 ���� ������Ʈ�� �߰��մϴ�.
     * 
     * @param signature ���� ������Ʈ�� �ñ״�ó�Դϴ�.
     * @param gameObject �߰��� ���� ������Ʈ�Դϴ�.
     * 
     * @throws �ñ״�ó ���� �̹� �����ϸ� ���ܸ� �����ϴ�.
     */
    public void AddGameObject(string signature, GameObject gameObject)
    {
        if (IsValid(signature))
        {
            throw new Exception("collision game object signature...");
        }

        gameObjects_.Add(signature, gameObject);
    }


    /**
     * @brief ���� �Ŵ����� �����ϴ� ���� ������Ʈ�� ����ϴ�.
     * 
     * @param signature ���� ������Ʈ�� �ñ״�ó ���Դϴ�.
     * 
     * @throws �ñ״�ó ���� �����ϴ� ���� ������Ʈ�� ���ٸ� ���ܸ� �����ϴ�.
     * 
     * @return �ñ״�ó ���� �����ϴ� ���� ������Ʈ�� ��ȯ�մϴ�.
     */
    public GameObject GetGameObject(string signature)
    {
        if(!IsValid(signature))
        {
            throw new Exception("can't find game object in world manager...");
        }

        return gameObjects_[signature];
    }


    /**
     * @brief ���忡 ���� ������Ʈ�� �����մϴ�.
     * 
     * @note �ñ״�ó ���� �����ϴ� ���� ������Ʈ�� ������ �ƹ� ���۵� �������� �ʽ��ϴ�.
     * 
     * @param signature ���� ������Ʈ�� �ñ״�ó�Դϴ�.
     */
    public void RemoveGameObject(string signature)
    {
        if (!IsValid(signature)) return;

        gameObjects_.Remove(signature);
    }


    /**
     * @brief �����ڴ� �ܺο��� ȣ���� �� ������ ����ϴ�.
     */
    private WorldManager() { }


    /**
     * @brief ���� �Ŵ����� �ʱ�ȭ�� ���� �ִ��� Ȯ���մϴ�.
     */
    private bool bIsSetup_ = false;


    /**
     * @brief ���� �Ŵ����� �����ϴ� ���� ������Ʈ�Դϴ�.
     */
    private Dictionary<string, GameObject> gameObjects_;


    /**
     * @brief ���� ������Ʈ�� �����ϴ� ���� �Ŵ����� �ν��Ͻ��Դϴ�.
     */
    private static WorldManager worldManager_;
}