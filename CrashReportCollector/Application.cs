using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;


/**
 * @brief ũ���� ���� ���ϰ� �α� ������ �����ϴ� ���ø����̼��� �����մϴ�.
 */
class CrashReportCollector
{
    /**
     * @brief ũ���� ���� ���ϰ� �α� ������ �����ϴ� ���ø����̼��� �����մϴ�.
     * 
     * @param args ����� �μ��Դϴ�.
     */
    static void Main(string[] args)
    {
        SetProperties(args);

        server_.Start();

        while (true)
        {
            TcpClient client = server_.AcceptTcpClient();
            NetworkStream networkStream = client.GetStream();
            
            CrashPacket packet = CrashPacket.Receive(networkStream);
            CrashPacketHeader packetHeader = (CrashPacketHeader)(packet.Header);
            
            if (packetHeader.PacketType != (uint)(CrashPacket.EType.REQ_FILE_SEND))
            {
                networkStream.Close();
                client.Close();
                continue;
            }

            CrashPacketRequestBody packetBody = (CrashPacketRequestBody)(packet.Body);

            CrashPacket responsePacket = new CrashPacket();
            responsePacket.Body = new CrashPacketResponseBody()
            {
                PacketID = packetHeader.PacketID,
                Response = (byte)(CrashPacket.ERespone.ACCEPTED)
            };
            responsePacket.Header = new CrashPacketHeader()
            {
                PacketID = crashPacketID_++,
                PacketType = (uint)(CrashPacket.EType.REP_FILE_SEND),
                BodySize = (uint)(responsePacket.Body.GetByteBufferSize()),
                Fragmented = (byte)(CrashPacket.EFragment.NO),
                LastPacket = (byte)(CrashPacket.ELast.YES),
                Seq = 0
            };

            CrashPacket.Send(networkStream, responsePacket);

            long fileSize = packetBody.FileSize;
            string fileName = Encoding.Default.GetString(packetBody.FileName);
            FileStream crashReportFile = new FileStream(crashReportDirectory_ + "\\" + fileName, FileMode.Create);

            uint? packetID = null;
            ushort prevSeq = 0;
            while((packet = CrashPacket.Receive(networkStream)) != null)
            {
                CrashPacketHeader header = (CrashPacketHeader)(packet.Header);
                if(header.PacketType != (uint)(CrashPacket.EType.FILE_SEND_DATA))
                {
                    break;
                }

                if (packetID == null)
                {
                    packetID = header.PacketID;
                }
                else
                {
                    if (packetID != header.PacketID)
                    {
                        break;
                    }
                }

                if (prevSeq++ != header.Seq)
                {
                    break;
                }

                crashReportFile.Write(packet.Body.GetByteBuffer(), 0, packet.Body.GetByteBufferSize());

                if (header.LastPacket == (byte)(CrashPacket.ELast.YES))
                    break;
            }

            long recvFileSize = crashReportFile.Length;
            crashReportFile.Close();

            CrashPacket resultPacket = new CrashPacket();
            resultPacket.Body = new CrashPacketResultBody()
            {
                PacketID = ((CrashPacketHeader)(packet.Header)).PacketID,
                Result = (byte)((fileSize == recvFileSize) ? CrashPacket.ESuccess.SUCCESS : CrashPacket.ESuccess.FAIL)
            };
            resultPacket.Header = new CrashPacketHeader()
            {
                PacketID = crashPacketID_++,
                PacketType = (uint)(CrashPacket.EType.FILE_SEND_RES),
                BodySize = (uint)(resultPacket.Body.GetByteBufferSize()),
                Fragmented = (byte)(CrashPacket.EFragment.NO),
                LastPacket = (byte)(CrashPacket.ELast.YES),
                Seq = 0
            };

            CrashPacket.Send(networkStream, resultPacket);

            networkStream.Close();
            client.Close();
        }
    }


    /**
     * @brief ũ���� ���� ���ϰ� �α� ������ �����ϴ� ���ø����̼��� ��� ������ �����մϴ�.
     * 
     * @param args ����� �μ��Դϴ�.
     */
    private static void SetProperties(string[] args)
    {
        if(args.Length != 3)
        {
            Console.WriteLine("invalid command line arguments...");
            return;
        }

        crashPacketID_ = 0;
        serverIP_ = args[0];
        serverPort_ = int.Parse(args[1]);
        crashReportDirectory_ = args[2];
        
        if (!Directory.Exists(crashReportDirectory_))
        {
            Directory.CreateDirectory(crashReportDirectory_);
        }

        Console.WriteLine("IP = {0}", serverIP_);
        Console.WriteLine("PORT = {0}", serverPort_);
        Console.WriteLine("DIRECTORY = {0}", crashReportDirectory_);

        serverAddress_ = new IPEndPoint(IPAddress.Parse(serverIP_), serverPort_);
        server_ = new TcpListener(serverAddress_);
    }


    /**
     * @brief ũ���� ����Ʈ�� ������ ���丮�Դϴ�.
     */
    private static string crashReportDirectory_;


    /**
     * @brief ������ ��Ʈ�Դϴ�.
     */
    private static int serverPort_;


    /**
     * @brief ������ IP �ּ��Դϴ�.
     */
    private static string serverIP_;


    /**
     * @brief ������ IP �ּ��Դϴ�.
     */
    private static IPEndPoint serverAddress_;


    /**
     * @brief TCP ������ ��ü�Դϴ�.
     */
    private static TcpListener server_;


    /**
     * @brief ũ���� ��Ŷ�� ID�Դϴ�.
     */
    private static uint crashPacketID_;
}