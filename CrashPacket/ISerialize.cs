/**
 * @brief ����ȭ�� ��ü�� �������̽��Դϴ�.
 */
public interface ISerialize
{
    /**
     * @brief ����ȭ�� ��ü�� ����Ʈ ���۸� ����ϴ�.
     */
    public byte[] GetByteBuffer();


    /**
     * @brief ����ȭ�� ��ü�� ����Ʈ ���� ũ�⸦ ����ϴ�.
     */
    public int GetByteBufferSize();
}