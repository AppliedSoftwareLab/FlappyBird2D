/**
 * @brief ���� ���� ������Ʈ�� ��Ÿ���� �������̽��Դϴ�.
 */
abstract class GameObject
{
    /**
     * @brief ���� ������Ʈ �Ӽ��� Getter/Setter�Դϴ�.
     */
    public int UpdateOrder
    {
        get => updateOrder_;
        set => updateOrder_ = value;
    }


    /**
     * @brief ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public abstract void Update(float deltaSeconds);


    /**
     * @brief ���� ������Ʈ�� ȭ�鿡 �׸��ϴ�.
     */
    public abstract void Render();


    /**
     * @brief ���� ������Ʈ�� ������Ʈ �����Դϴ�.
     */
    private int updateOrder_ = 0;
}