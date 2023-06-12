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
        string messageFormat = string.Format("[INFO|{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), message);
        logMessages_.Add(messageFormat);

#if DEBUG || RELEASE
        System.Console.ForegroundColor = System.ConsoleColor.White;
        System.Console.WriteLine(messageFormat);
        System.Console.ResetColor();
#endif
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
        string messageFormat = string.Format("[WARN|{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), message);
        logMessages_.Add(messageFormat);

#if DEBUG || RELEASE
        System.Console.ForegroundColor = System.ConsoleColor.Yellow;
        System.Console.WriteLine(messageFormat);
        System.Console.ResetColor();
#endif
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
        string messageFormat = string.Format("[ERROR|{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), message);
        logMessages_.Add(messageFormat);

#if DEBUG || RELEASE
        System.Console.ForegroundColor = System.ConsoleColor.Red;
        System.Console.WriteLine(messageFormat);
        System.Console.ResetColor();
#endif
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
     * @brief ������ �α� �޽����Դϴ�.
     */
    private static List<string> logMessages_ = new List<string>();
}