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
        string CrashDumpFileName = CommandLine.GetValue("Crash") + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".dmp";

        MINIDUMP_EXCEPTION_INFORMATION ExceptionInfo = new MINIDUMP_EXCEPTION_INFORMATION();
        ExceptionInfo.ClientPointers = 1;
        ExceptionInfo.ExceptionPointers = Marshal.GetExceptionPointers();
        ExceptionInfo.ThreadId = GetCurrentThreadId();

        FileStream CrashDumpFile = new FileStream(CrashDumpFileName, FileMode.Create);

        bool bIsSuccess = MiniDumpWriteDump(
            GetCurrentProcess(), 
            GetCurrentProcessId(),
            CrashDumpFile.SafeFileHandle.DangerousGetHandle(), 
            MiniDumpWithFullMemory, 
            ref ExceptionInfo, 
            IntPtr.Zero, 
            IntPtr.Zero
        );

        CrashDumpFile.Close();

        if(!bIsSuccess)
        {
            System.Console.WriteLine("failed to create crash dump file : %s...", CrashDumpFileName);
        }
    }
}