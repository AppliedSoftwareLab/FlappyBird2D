using System;
using System.IO;
using System.Collections.Generic;


/**
 * @brief ���� ���� �α׸� ����մϴ�.
 * 
 * @note �� Ŭ������ ���� Ŭ������ �ν��Ͻ� ���� ���� �ٷ� ����� �� �ֽ��ϴ�.
 */
class Logger
{
    /**
     * @brief �Ϲ����� �α׸� ����մϴ�.
     * 
     * @note ����׳� ������ ��忡���� �ֿܼ� ����մϴ�.
     * 
     * @param message �α� �޽����Դϴ�.
     */
    public static void Info(string message)
    {
        Record("INFO", message, System.ConsoleColor.White);
    }


    /**
     * @brief ��� ������ �α׸� ����մϴ�.
     * 
     * @note ����׳� ������ ��忡���� �ֿܼ� ����մϴ�.
     * 
     * @param message �α� �޽����Դϴ�.
     */
    public static void Warn(string message)
    {
        Record("WARN", message, System.ConsoleColor.Yellow);
    }


    /**
     * @brief ���� ������ �α׸� ����մϴ�.
     * 
     * @note ����׳� ������ ��忡���� �ֿܼ� ����մϴ�.
     * 
     * @param message �α� �޽����Դϴ�.
     */
    public static void Error(string message)
    {
        Record("ERROR", message, System.ConsoleColor.Red);
    }


    /**
     * @brief �α� ����� �ؽ�Ʈ ���Ϸ� ����մϴ�.
     * 
     * @param path �α� ������ ����� ��ο� ���� �̸��Դϴ�.
     */
    public static void Export(string path)
    {
        File.WriteAllLines(path, logMessages_);
    }


    /**
     * @brief �α׸� ����մϴ�.
     * 
     * @param type �α��� Ÿ���Դϴ�.
     * @param message �α� �޽����Դϴ�.
     * @param color �α� �޽����� �����Դϴ�.
     */
    private static void Record(string type, string message, System.ConsoleColor color)
    {
        string messageFormat = string.Format("[{0}|{1}] {2}", type, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), message);
        logMessages_.Add(messageFormat);

#if DEBUG || RELEASE
        System.Console.ForegroundColor = color;
        System.Console.WriteLine(messageFormat);
        System.Console.ResetColor();
#endif
    }


    /**
     * @brief ������ �α� �޽����Դϴ�.
     */
    private static List<string> logMessages_ = new List<string>();
}