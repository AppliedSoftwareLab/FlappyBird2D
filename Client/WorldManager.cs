using System;
using System.Collections.Generic;


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

        bIsSetup_ = true;
    }


    /**
     * @brief ���� �Ŵ����� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        if (!bIsSetup_) return;

        foreach (KeyValuePair<string, IGameObject> objectKeyValue in gameObjects_)
        {
            IGameObject gameObject = objectKeyValue.Value;
            gameObject.Cleanup();
        }

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
     * @brief ���忡 ���� ������Ʈ�� �߰��մϴ�.
     * 
     * @param signature ���� ������Ʈ�� �ñ״�ó�Դϴ�.
     * @param gameObject �߰��� ���� ������Ʈ�Դϴ�.
     * 
     * @throws �ñ״�ó ���� �̹� �����ϸ� ���ܸ� �����ϴ�.
     */
    public void AddGameObject(string signature, IGameObject gameObject)
    {
        if (IsValid(signature))
        {
            throw new Exception("collision game object signature...");
        }

        gameObjects_.Add(signature, gameObject);
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
    private Dictionary<string, IGameObject> gameObjects_ = new Dictionary<string, IGameObject>();


    /**
     * @brief ���� ������Ʈ�� �����ϴ� ���� �Ŵ����� �ν��Ͻ��Դϴ�.
     */
    private static WorldManager worldManager_;
}