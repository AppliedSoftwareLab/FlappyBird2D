using System;
using System.IO;


/**
 * @brief ����ȭ�� ũ���� ��Ŷ�Դϴ�.
 */
class CrashPacket : Packet
{
    /**
     * @brief ��Ŷ�� �����Դϴ�.
     */
    public enum EType : uint
    {
        REQ_FILE_SEND = 0x01,
        REP_FILE_SEND = 0x02,
        FILE_SEND_DATA = 0x03,
        FILE_SEND_RES = 0x04,
    }


    /**
     * @brief ��Ŷ�� ����ȭ �����Դϴ�.
     */
    public enum EFragment : byte
    {
        NO = 0x00,
        YES = 0x01,
    }


    /**
     * @brief ��Ŷ�� ������ ��Ŷ���� �����Դϴ�.
     */
    public enum ELast : byte
    {
        NO = 0x00,
        YES = 0x01,
    }


    /**
     * @brief ���� ��û �����Դϴ�.
     */
    public enum ERespone : byte
    {
        ACCEPTED = 0x00,
        DENIED = 0x01,
    }


    /**
     * @brief ��Ŷ�� ���� �����Դϴ�.
     */
    public enum ESuccess : byte
    {
        FAIL = 0x00,
        SUCCESS = 0x01
    }


    /**
     * @brief ũ���� ��Ŷ�� �����մϴ�.
     * 
     * @param writer ��Ŷ�� ������ ��Ʈ���Դϴ�.
     * @param packet ������ ũ���� ��Ŷ�Դϴ�.
     */
    public static void Send(Stream writer, CrashPacket packet)
    {
        writer.Write(packet.GetByteBuffer(), 0, packet.GetByteBufferSize());
    }


    /**
     * @brief ũ���� ��Ŷ�� �޽��ϴ�.
     * 
     * @param reader ��Ŷ�� ���� ��Ʈ���Դϴ�.
     * 
     * @return ���� ũ���� ��Ŷ ��ü�� ��ȯ�մϴ�.
     */
    public static CrashPacket Receive(Stream reader)
    {
        int totalRecv = 0;
        int readSize = CrashPacketHeader.PACKET_HEADER_SIZE;
        byte[] headerBuffer = new byte[readSize];

        while (readSize > 0)
        {
            byte[] buffer = new byte[readSize];
            int recv = reader.Read(buffer, 0, readSize);

            if (recv == 0)
            {
                return null;
            }

            buffer.CopyTo(headerBuffer, totalRecv);
            totalRecv += recv;
            readSize -= recv;
        }

        CrashPacketHeader packetHeader = new CrashPacketHeader(headerBuffer);

        totalRecv = 0;
        byte[] bodyBuffer = new byte[packetHeader.BodySize];
        readSize = (int)(packetHeader.BodySize);

        while (readSize > 0)
        {
            byte[] buffer = new byte[readSize];
            int recv = reader.Read(buffer, 0, readSize);

            if (recv == 0)
            {
                return null;
            }

            buffer.CopyTo(bodyBuffer, totalRecv);
            totalRecv += recv;
            readSize -= recv;
        }

        ISerialize packetBody = null;
        CrashPacket.EType packetType = (CrashPacket.EType)(packetHeader.PacketType);

        switch (packetType)
        {
            case CrashPacket.EType.REQ_FILE_SEND:
                packetBody = new CrashPacketRequestBody(bodyBuffer);
                break;
            case CrashPacket.EType.REP_FILE_SEND:
                packetBody = new CrashPacketResponseBody(bodyBuffer);
                break;
            case CrashPacket.EType.FILE_SEND_DATA:
                packetBody = new CrashPacketDataBody(bodyBuffer);
                break;
            case CrashPacket.EType.FILE_SEND_RES:
                packetBody = new CrashPacketResultBody(bodyBuffer);
                break;
        }

        return new CrashPacket() { Header = packetHeader, Body = packetBody };
    }
}

