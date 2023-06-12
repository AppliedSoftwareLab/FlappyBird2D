using System;
using System.IO;
using System.Runtime.InteropServices;


/**
 * @brief ���ø����̼� �浹 ó���� �����ϴ� �ڵ鷯�Դϴ�.
 * 
 * @note �� Ŭ������ ���� Ŭ������ �ν��Ͻ� ���� ���� �ٷ� ����� �� �ֽ��ϴ�.
 */
class CrashHandler
{
    /*******************************************************
     * @brief Begin Windows API
     *******************************************************/
    [DllImport("Dbghelp.dll")]
    static extern bool MiniDumpWriteDump(IntPtr hProcess, uint ProcessId, IntPtr hFile, int DumpType, ref MINIDUMP_EXCEPTION_INFORMATION ExceptionParam, IntPtr UserStreamParam, IntPtr CallbackParam);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetCurrentProcess();

    [DllImport("kernel32.dll")]
    static extern uint GetCurrentProcessId();

    [DllImport("kernel32.dll")]
    static extern uint GetCurrentThreadId();

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct MINIDUMP_EXCEPTION_INFORMATION
    {
        public uint ThreadId;
        public IntPtr ExceptionPointers;
        public int ClientPointers;
    }

    const int MiniDumpNormal = 0x00000000;
    const int MiniDumpWithFullMemory = 0x00000002;
    /*******************************************************
     * @brief End Windows API
     *******************************************************/


    /**
     * @brief ���ø����̼� ũ���� �߻� �� ������ �޼���
     * 
     * @see https://learn.microsoft.com/en-us/dotnet/api/system.unhandledexceptioneventhandler?view=net-7.0
     * 
     * @param Sender ó������ ���� ���� �̺�Ʈ
     * @param ExceptionEvent ó������ ���� ���� ������
     */
    public static void DetectApplicationCrash(object Sender, UnhandledExceptionEventArgs ExceptionEvent)
    {
        string crashDirectory = CommandLine.GetValue("Crash") + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        DirectoryInfo directoryInfo = new DirectoryInfo(crashDirectory);

        if(!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        string crashDumpFileName = crashDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".dmp";
        string logFileName = crashDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";

        MINIDUMP_EXCEPTION_INFORMATION exceptionInfo = new MINIDUMP_EXCEPTION_INFORMATION();
        exceptionInfo.ClientPointers = 1;
        exceptionInfo.ExceptionPointers = Marshal.GetExceptionPointers();
        exceptionInfo.ThreadId = GetCurrentThreadId();

        FileStream crashDumpFile = new FileStream(crashDumpFileName, FileMode.Create);

        bool bIsSuccess = MiniDumpWriteDump(
            GetCurrentProcess(), 
            GetCurrentProcessId(),
            crashDumpFile.SafeFileHandle.DangerousGetHandle(), 
            MiniDumpWithFullMemory, 
            ref exceptionInfo, 
            IntPtr.Zero, 
            IntPtr.Zero
        );

        crashDumpFile.Close();
        Logger.Export(logFileName);

        if (!bIsSuccess)
        {
            System.Console.WriteLine("failed to create crash dump file : %s...", crashDumpFileName);
        }
    }
}