/**
 * @brief ����ȭ�� ��Ŷ�Դϴ�.
 */
public class Packet : ISerialize
{
    /**
     * @brief ����ȭ�� ��Ŷ�� Getter/Setter�Դϴ�.
     */
    public ISerialize Header
    {
        get => header_;
        set => header_ = value;
    }

    public ISerialize Body
    {
        get => body_;
        set => body_ = value;
    }


    /**
     * @brief ����ȭ�� ��Ŷ ��ü�� ����Ʈ ���۸� ����ϴ�.
     */
    public byte[] GetByteBuffer()
    {
        byte[] byteBuffer = new byte[GetByteBufferSize()];

        header_.GetByteBuffer().CopyTo(byteBuffer, 0);
        body_.GetByteBuffer().CopyTo(byteBuffer, header_.GetByteBufferSize());

        return byteBuffer;
    }


    /**
     * @brief ����ȭ�� ��Ŷ ��ü�� ����Ʈ ���� ũ�⸦ ����ϴ�.
     */
    public int GetByteBufferSize()
    {
        return header_.GetByteBufferSize() + body_.GetByteBufferSize();
    }


    /**
     * @brief ��Ŷ�� �ش��Դϴ�.
     */
    protected ISerialize header_;


    /**
     * @brief ��Ŷ�� �ٵ��Դϴ�.
     */
    protected ISerialize body_;
}