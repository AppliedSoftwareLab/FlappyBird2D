using System;
using System.Runtime.InteropServices;


/**
 * @brief �ܼ� â ��� �����ϴ� �ڵ鷯�Դϴ�.
 * 
 * @note �� Ŭ������ ���� Ŭ������ �ν��Ͻ� ���� ���� �ٷ� ����� �� �ֽ��ϴ�.
 */
class ConsoleHandler
{
    /*******************************************************
     * @brief Begin Windows API
     *******************************************************/
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 0;
    const int SW_SHOW = 5;
    /*******************************************************
     * @brief End Windows API
     *******************************************************/


    /**
     * @brief �ܼ� â�� �þ� ������ �����մϴ�.
     * 
     * @param bIsVisible true ���̸� �ܼ� â�� ȭ�鿡 ����, false ���̸� �ܼ� â�� ����ϴ�.
     * 
     * @throws �ܼ� â ��� �����ϸ� ���ܸ� �����ϴ�.
     */
    public static void SetVisible(bool bIsVisible)
    {
        int CmdShow = bIsVisible ? SW_SHOW : SW_HIDE;
        IntPtr consoleWindowHandle = GetConsoleWindow();

        if(!ShowWindow(consoleWindowHandle, CmdShow))
        {
            throw new Exception("failed to set console window visible...");
        }
    }
}