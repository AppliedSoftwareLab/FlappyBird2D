using System;


/**
 * @brief ���� ���� ������Ʈ�� ��Ÿ���� �������̽��Դϴ�.
 */
public interface IGameObject
{
    /**
     * @brief ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float deltaSeconds);


    /**
     * @brief ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     */
    public void Render();


    /**
     * @brief ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup();
}