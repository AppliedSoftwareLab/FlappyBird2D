using System;


/**
 * @brief ũ���� ��Ŷ�� ����Դϴ�.
 */
public class CrashPacketHeader : ISerialize
{
    /**
     * @brief ũ���� ��Ŷ �ش��� ���� Getter/Setter�Դϴ�.
     */
    public uint PacketID
    {
        get => packetID_;
        set => packetID_ = value;
    }

    public uint PacketType
    {
        get => packetType_;
        set => packetType_ = value;
    }

    public uint BodySize
    {
        get => bodySize_;
        set => bodySize_ = value;
    }

    public byte Fragmented
    {
        get => fragmented_;
        set => fragmented_ = value;
    }

    public byte LastPacket
    {
        get => lastPacket_;
        set => lastPacket_ = value;
    }

    public ushort Seq
    {
        get => seq_;
        set => seq_ = value;
    }


    /**
     * @brief ũ���� ��Ŷ ����� �� �������Դϴ�.
     */
    public CrashPacketHeader() { }


    /**
     * @brief ũ���� ��Ŷ ����� �������Դϴ�.
     * 
     * @param headerBytes ũ���� ��Ŷ ����� ����Ʈ �����Դϴ�.
     */
    public CrashPacketHeader(byte[] headerBytes)
    {
        packetID_ = BitConverter.ToUInt32(headerBytes, 0);
        packetType_ = BitConverter.ToUInt32(headerBytes, 4);
        bodySize_ = BitConverter.ToUInt32(headerBytes, 8);
        fragmented_ = headerBytes[12];
        lastPacket_ = headerBytes[13];
        seq_ = BitConverter.ToUInt16(headerBytes, 14);
    }


    /**
     * @brief ũ���� ��Ŷ ����� ����Ʈ ���۸� ����ϴ�.
     * 
     * @return ũ���� ��Ŷ ����� ����Ʈ ���۸� ��ȯ�մϴ�.
     */
    public byte[] GetByteBuffer()
    {
        byte[] byteBuffer = new byte[PACKET_SIZE];

        byte[] buffer = BitConverter.GetBytes(packetID_);
        Array.Copy(buffer, 0, byteBuffer, 0, buffer.Length);

        buffer = BitConverter.GetBytes(packetType_);
        Array.Copy(buffer, 0, byteBuffer, 4, buffer.Length);

        buffer = BitConverter.GetBytes(bodySize_);
        Array.Copy(buffer, 0, byteBuffer, 8, buffer.Length);

        byteBuffer[12] = fragmented_;
        byteBuffer[13] = lastPacket_;

        buffer = BitConverter.GetBytes(seq_);
        Array.Copy(buffer, 0, byteBuffer, 14, buffer.Length);

        return byteBuffer;
    }


    /**
     * @brief ũ���� ��Ŷ ����� ����Ʈ ���� ũ�⸦ ����ϴ�.
     * 
     * @return ũ���� ��Ŷ �ش��� ����Ʈ ���� ũ�⸦ ��ȯ�մϴ�.
     */
    public int GetByteBufferSize()
    {
        return PACKET_HEADER_SIZE;
    }


    /**
     * @brief ũ���� ��Ŷ�� ũ���Դϴ�.
     */
    public static readonly int PACKET_HEADER_SIZE = 16;


    /**
     * @brief ��Ŷ�� �ĺ��� ID�Դϴ�.
     */
    private uint packetID_;


    /**
     * @brief ��Ŷ�� �����Դϴ�.
     */
    private uint packetType_;


    /**
     * @brief ��Ŷ �ٵ��� ũ���Դϴ�.
     */
    private uint bodySize_;


    /**
     * @brief ��Ŷ ���� �����Դϴ�.
     */
    private byte fragmented_;


    /**
     * @brief ���ҵ� ��Ŷ�� ������ ��Ŷ���� Ȯ���մϴ�.
     */
    private byte lastPacket_;


    /**
     * @brief ��Ŷ�� ���� ��ȣ�Դϴ�.
     */
    private ushort seq_;
}