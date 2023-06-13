using System;


/**
 * @brief ���� ������ ��û���� ���� ũ���� ��Ŷ �ٵ��Դϴ�.
 */
public class CrashPacketRequestBody : ISerialize
{
    /**
     * @brief ���� ������ ��û���� ���� ũ���� ��Ŷ �ٵ� �� �������Դϴ�.
     */
    public CrashPacketRequestBody() { }


    /**
     * @brief ���� ������ ��û���� ���� ũ���� ��Ŷ �ٵ� �������Դϴ�.
     * 
     * @param bodyBytes ũ���� ��Ŷ �ٵ��� ����Ʈ �����Դϴ�.
     */
    public CrashPacketRequestBody(byte[] bodyBytes)
    {
        fileSize_ = BitConverter.ToInt64(bodyBytes, 0);
        fileName_ = new byte[bodyBytes.Length - sizeof(long)];
        Array.Copy(bodyBytes, sizeof(long), fileName_, 0, fileName_.Length);
    }


    /**
     * @brief ���� ������ ��û���� ���� ũ���� ��Ŷ �ٵ��� Getter/Setter�Դϴ�.
     */
    public long FileSize
    {
        get => fileSize_;
        set => fileSize_ = value;
    }

    public byte[] FileName
    {
        get => fileName_;
        set => fileName_ = value;
    }
    

    /**
     * @brief ���� ������ ��û���� ���� ũ���� ��Ŷ �ٵ� ����Ʈ ���۸� ����ϴ�.
     * 
     * @return ���� ������ ��û���� ���� ũ���� ��Ŷ �ٵ� ����Ʈ ���۸� ��ȯ�մϴ�.
     */
    public byte[] GetByteBuffer()
    {
        byte[] bytes = new byte[GetByteBufferSize()];
        byte[] buffer = BitConverter.GetBytes(fileSize_);
        Array.Copy(buffer, 0, bytes, 0, buffer.Length);
        Array.Copy(fileName_, 0, bytes, buffer.Length, fileName_.Length);

        return bytes;
    }


    /**
     * @brief ���� ������ ��û���� ���� ũ���� ��Ŷ �ٵ� ����Ʈ ������ ũ�⸦ ����ϴ�.
     * 
     * @return ���� ������ ��û���� ���� ũ���� ��Ŷ �ٵ� ����Ʈ ������ ũ�⸦ ��ȯ�մϴ�.
     */
    public int GetByteBufferSize()
    {
        return sizeof(long) + fileName_.Length;
    }


    /**
     * @brief ������ ũ���Դϴ�.
     */
    private long fileSize_;


    /**
     * @brief ������ �̸��Դϴ�.
     */
    private byte[] fileName_;
}


/**
 * @brief �������� ���� ũ���� ��Ŷ �ٵ��Դϴ�.
 */
public class CrashPacketResponseBody : ISerialize
{
    /**
     * @brief �������� ���� ũ���� ��Ŷ �ٵ��� �� �������Դϴ�.
     */
    public CrashPacketResponseBody() { }


    /**
     * @brief �������� ���� ũ���� ��Ŷ �ٵ��� �������Դϴ�.
     * 
     * @param bodyBytes ũ���� ��Ŷ �ٵ��� ����Ʈ �����Դϴ�.
     */
    public CrashPacketResponseBody(byte[] bodyBytes)
    {
        packetID_ = BitConverter.ToUInt32(bodyBytes, 0);
        response_ = bodyBytes[4];
    }


    /**
     * @brief �������� ���� ũ���� ��Ŷ �ٵ��� Getter/Setter�Դϴ�.
     */
    public uint PacketID
    {
        get => packetID_;
        set => packetID_ = value;
    }

    public byte Response
    {
        get => response_;
        set => response_ = value;
    }


    /**
     * @brief ���� ���� ���� ũ���� ��Ŷ �ٵ� ����Ʈ ���۸� ����ϴ�.
     * 
     * @return ���� �� ��û���� ���� ũ���� ��Ŷ �ٵ� ����Ʈ ���۸� ��ȯ�մϴ�.
     */
    public byte[] GetByteBuffer()
    {
        byte[] bytes = new byte[GetByteBufferSize()];
        byte[] buffer = BitConverter.GetBytes(packetID_);
        Array.Copy(buffer, 0, bytes, 0, buffer.Length);
        bytes[buffer.Length] = response_;

        return bytes;
    }


    /**
     * @brief ���� ���� ���� ũ���� ��Ŷ �ٵ� ����Ʈ ������ ũ�⸦ ����ϴ�.
     * 
     * @return ���� ���� ���� ũ���� ��Ŷ �ٵ� ����Ʈ ������ ũ�⸦ ��ȯ�մϴ�.
     */
    public int GetByteBufferSize()
    {
        return sizeof(uint) + sizeof(byte);
    }


    /**
     * @brief ��Ŷ ���̵��Դϴ�.
     */
    public uint packetID_;


    /**
     * @brief ��Ŷ ���� �����Դϴ�.
     */
    public byte response_;
}


/**
 * @brief �����Ͱ� ���Ե� ũ���� ��Ŷ �ٵ��Դϴ�.
 */
public class CrashPacketDataBody : ISerialize
{
    /**
     * @brief �����Ͱ� ���Ե� ũ���� ��Ŷ �ٵ��� �� �������Դϴ�.
     */
    public CrashPacketDataBody() { }


    /**
     * @brief �����Ͱ� ���Ե� ũ���� ��Ŷ �ٵ��� �������Դϴ�.
     * 
     * @param bodyBytes ũ���� ��Ŷ �ٵ��� ����Ʈ �����Դϴ�.
     */
    public CrashPacketDataBody(byte[] bodyBytes)
    {
        data_ = new byte[bodyBytes.Length];
        bodyBytes.CopyTo(data_, 0);
    }


    /**
     * @brief �����Ͱ� ���Ե� ũ���� ��Ŷ �ٵ��� Getter/Setter�Դϴ�.
     */
    public byte[] Data
    {
        get => data_;
        set => data_ = value;
    }


    /**
     * @brief �����Ͱ� ���Ե� ũ���� ��Ŷ �ٵ� ����Ʈ ���۸� ����ϴ�.
     * 
     * @return �����Ͱ� ���Ե�  ũ���� ��Ŷ �ٵ� ����Ʈ ���۸� ��ȯ�մϴ�.
     */
    public byte[] GetByteBuffer()
    {
        return data_;
    }


    /**
     * @brief �����Ͱ� ���Ե� ũ���� ��Ŷ �ٵ� ����Ʈ ������ ũ�⸦ ����ϴ�.
     * 
     * @return �����Ͱ� ���Ե� ũ���� ��Ŷ �ٵ� ����Ʈ ������ ũ�⸦ ��ȯ�մϴ�.
     */
    public int GetByteBufferSize()
    {
        return data_.Length;
    }
    

    /**
     * @brief ��Ŷ �ٵ� �κ��� �������Դϴ�.
     */
    public byte[] data_;
}


/**
 * @brief ��Ŷ ���� ����� ������ ũ���� ��Ŷ �ٵ��Դϴ�.
 */
public class CrashPacketResultBody : ISerialize
{
    /**
     * @brief ��Ŷ ���� ����� ������ ũ���� ��Ŷ �ٵ��� �� �������Դϴ�.
     */
    public CrashPacketResultBody() { }


    /**
     * @brief ��Ŷ ���� ����� ������ ũ���� ��Ŷ �ٵ��� �������Դϴ�.
     * 
     * @param bodyBytes ũ���� ��Ŷ �ٵ��� ����Ʈ �����Դϴ�.
     */
    public CrashPacketResultBody(byte[] bodyBytes)
    {
        packetID_ = BitConverter.ToUInt32(bodyBytes, 0);
        result_ = bodyBytes[4];
    }


    /**
     * @brief ��Ŷ ���� ����� ������ ũ���� ��Ŷ �ٵ��� Getter/Setter �Դϴ�.
     */
    public uint PacketID
    {
        get => packetID_;
        set => packetID_ = value;
    }

    public byte Result
    {
        get => result_;
        set => result_ = value;
    }


    /**
     * @brief ��Ŷ ���� ����� ������ ũ���� ��Ŷ �ٵ� ����Ʈ ���۸� ����ϴ�.
     * 
     * @return ��Ŷ ���� ����� ������ ũ���� ��Ŷ �ٵ� ����Ʈ ���۸� ��ȯ�մϴ�.
     */
    public byte[] GetByteBuffer()
    {
        byte[] bytes = new byte[GetByteBufferSize()];
        byte[] buffer = BitConverter.GetBytes(packetID_);
        Array.Copy(buffer, 0, bytes, 0, buffer.Length);
        bytes[buffer.Length] = result_;

        return bytes;
    }


    /**
     * @brief ��Ŷ ���� ����� ������ ũ���� ��Ŷ �ٵ� ����Ʈ ������ ũ�⸦ ����ϴ�.
     * 
     * @return ��Ŷ ���� ����� ������ ũ���� ��Ŷ �ٵ� ����Ʈ ������ ũ�⸦ ��ȯ�մϴ�.
     */
    public int GetByteBufferSize()
    {
        return sizeof(uint) + sizeof(byte);
    }


    /**
     * @brief ��Ŷ�� ���̵��Դϴ�.
     */
    private uint packetID_;


    /**
     * @brief ��Ŷ�� ���� ����Դϴ�.
     */
    private byte result_;
}