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
     * 
     * @param renderer ������Ʈ�� ȭ�鿡 �׸��� ���� �������Դϴ�.
     */
    public void Render(IntPtr renderer);


    /**
     * @brief ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup();
}