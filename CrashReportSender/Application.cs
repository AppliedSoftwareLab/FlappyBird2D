using System;
using System.IO;
using System.Net;
using System.Net.Sockets;


/**
 * @brief ũ���� ���� ���ϰ� �α� ������ �����ϴ� ���ø����̼��� �����մϴ�.
 */
class CrashReportSender
{
    /**
     * @brief ũ���� ���� ���ϰ� �α� ������ �����ϴ� ���ø����̼��� �����մϴ�.
     * 
     * @param args ����� �μ��Դϴ�.
     */
    static void Main(string[] args)
    {
        Console.WriteLine("run crash report sender...");
        SetProperties(args);

        client_.Connect(serverAddress_);
        networkStream_ = client_.GetStream();

        RequestSendFileToServer();
        SendFileToServer();

        networkStream_.Close();
        client_.Close();
    }


    /**
     * @brief ��� ������ �����մϴ�.
     * 
     * @param args ����� �μ��Դϴ�.
     */
    private static void SetProperties(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("invalid command line arguments...");
            return;
        }

        serverIP_ = args[0];
        serverPort_ = int.Parse(args[1]);
        sendFilePath_ = args[2];

        clientAddress_ = new IPEndPoint(0, 0);
        serverAddress_ = new IPEndPoint(IPAddress.Parse(serverIP_), serverPort_);

        client_ = new TcpClient(clientAddress_);

        crashPacketID_ = 0;

        Console.WriteLine("IP = {0}", serverIP_);
        Console.WriteLine("PORT = {0}", serverPort_);
        Console.WriteLine("FILE = {0}", sendFilePath_);
    }


    /**
     * @brief ���� ���� ��û�� ������ �����ϴ�.
     */
    private static void RequestSendFileToServer()
    {
        CrashPacket requestPacket = new CrashPacket();
        requestPacket.Body = new CrashPacketRequestBody()
        {
            FileSize = new FileInfo(sendFilePath_).Length,
            FileName = System.Text.Encoding.Default.GetBytes(Path.GetFileName(sendFilePath_))
        };
        requestPacket.Header = new CrashPacketHeader()
        {
            PacketID = crashPacketID_++,
            PacketType = (uint)(CrashPacket.EType.REQ_FILE_SEND),
            BodySize = (uint)(requestPacket.Body.GetByteBufferSize()),
            Fragmented = (byte)(CrashPacket.EFragment.NO),
            LastPacket = (byte)(CrashPacket.ELast.YES),
            Seq = 0
        };
        CrashPacket.Send(networkStream_, requestPacket);

        CrashPacket responePacket = CrashPacket.Receive(networkStream_);
        CrashPacketHeader packetHeader = (CrashPacketHeader)(responePacket.Header);

        if (packetHeader.PacketType != (uint)(CrashPacket.EType.REP_FILE_SEND))
        {
            Console.WriteLine("invalid response from crash collector...");
            networkStream_.Close();
            client_.Close();
            return;
        }
    }


    /**
     * @brief ������ ������ �����ϴ�.
     */
    private static void SendFileToServer()
    {
        Stream fileStream = new FileStream(sendFilePath_, FileMode.Open);
        byte[] readbytes = new byte[FILE_CHUNK_SIZE];
        int totalRead = 0;
        ushort packetSeq = 0;
        byte fragmented = (byte)((fileStream.Length < FILE_CHUNK_SIZE) ? CrashPacket.EFragment.NO : CrashPacket.EFragment.YES);

        while (totalRead < fileStream.Length)
        {
            int read = fileStream.Read(readbytes, 0, FILE_CHUNK_SIZE);
            totalRead += read;
            CrashPacket filePacket = new CrashPacket();

            byte[] sendBytes = new byte[read];
            Array.Copy(readbytes, 0, sendBytes, 0, read);

            filePacket.Body = new CrashPacketDataBody(sendBytes);
            filePacket.Header = new CrashPacketHeader()
            {
                PacketID = crashPacketID_,
                PacketType = (uint)(CrashPacket.EType.FILE_SEND_DATA),
                BodySize = (uint)(filePacket.Body.GetByteBufferSize()),
                Fragmented = fragmented,
                LastPacket = (byte)((totalRead < fileStream.Length) ? CrashPacket.ELast.NO : CrashPacket.ELast.YES),
                Seq = packetSeq++
            };

            CrashPacket.Send(networkStream_, filePacket);
        }

        CrashPacket resultPacket = CrashPacket.Receive(networkStream_);
        CrashPacketResultBody resultPacketBody = (CrashPacketResultBody)(resultPacket.Body);

        if (resultPacketBody.Result == (byte)(CrashPacket.ESuccess.SUCCESS))
        {
            Console.WriteLine("successed sending crash report file...");
        }
        else
        {
            Console.WriteLine("failed sending crash report file...");
        }
    }


    /**
     * @brief ���� ûũ ũ���Դϴ�.
     */
    private static readonly int FILE_CHUNK_SIZE = 4096;


    /**
     * @brief ũ���� ���� ������ IP�Դϴ�.
     */
    private static string serverIP_;


    /**
     * @brief ũ���� ���� ������ PORT�Դϴ�.
     */
    private static int serverPort_;


    /**
     * @brief ������ ������ ����Դϴ�.
     */
    private static string sendFilePath_;


    /**
     * @brief ũ���� ��Ŷ�� ID�Դϴ�.
     */
    private static uint crashPacketID_;


    /**
     * @brief Ŭ���̾�Ʈ �ּ��Դϴ�.
     */
    private static IPEndPoint clientAddress_;


    /**
     * @brief ���� �ּ��Դϴ�.
     */
    private static IPEndPoint serverAddress_;


    /**
     * @brief Ŭ���̾�Ʈ ��Ʈ��ũ ��Ʈ���Դϴ�.
     */
    private static NetworkStream networkStream_;


    /**
     * @brief TCP Ŭ���̾�Ʈ ��ü�Դϴ�.
     */
    private static TcpClient client_;
}