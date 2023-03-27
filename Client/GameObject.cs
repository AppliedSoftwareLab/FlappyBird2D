using System;


/**
 * @brief ���� ���� ������Ʈ�� ��Ÿ���� �������̽��Դϴ�.
 */
public interface IGameObject
{
    /**
     * @brief ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param DeltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public void Update(float DeltaSeconds);


    /**
     * @brief ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     * 
     * @param Renderer ������Ʈ�� ȭ�鿡 �׸��� ���� �������Դϴ�.
     */
    public void Render(IntPtr Renderer);


    /**
     * @brief ���� ������Ʈ ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup();
}