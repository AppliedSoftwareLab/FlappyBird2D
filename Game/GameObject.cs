/**
 * @brief ���� ���� ������Ʈ�� ��Ÿ���� �������̽��Դϴ�.
 */
abstract class GameObject
{
    /**
     * @brief ���� ������Ʈ �Ӽ��� Getter/Setter�Դϴ�.
     */
    public bool Active
    {
        get => bIsActive_;
        set => bIsActive_ = value;
    }

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
    public abstract void Tick(float deltaSeconds);


    /**
     * @brief ���� ������Ʈ�� Ȱ��ȭ �����Դϴ�.
     * 
     * @note ��Ȱ��ȭ �Ǿ� �ִٸ�, ������Ʈ�� �������� �������� �ʽ��ϴ�.
     */
    private bool bIsActive_ = false;


    /**
     * @brief ���� ������Ʈ�� ������Ʈ �����Դϴ�.
     */
    private int updateOrder_ = 0;
}