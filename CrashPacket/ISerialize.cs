/**
 * @brief ����ȭ�� ��ü�� �������̽��Դϴ�.
 */
interface ISerialize
{
    /**
     * @brief ����ȭ�� ��ü�� ����Ʈ ���۸� ����ϴ�.
     */
    byte[] GetByteBuffer();


    /**
     * @brief ����ȭ�� ��ü�� ����Ʈ ���� ũ�⸦ ����ϴ�.
     */
    int GetByteBufferSize();
}