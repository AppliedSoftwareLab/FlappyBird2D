using System;
using System.Collections.Generic;


/**
 * @brief Ŀ�ǵ���� ���ڸ� �Ľ��ϰ� �����ϴ� Ŭ�����Դϴ�.
 * 
 * @note �� Ŭ������ ���� Ŭ������, �ν��Ͻ� ���� ���� �ٷ� ����� �� �ֽ��ϴ�.
 */
class CommandLine
{
    /**
     * @brief ����� ���ڸ� �Ľ��մϴ�.
     * 
     * @param args �Ľ��� ����� �����Դϴ�.
     */
    public static void Parse(string[] args)
    {
        Logger.Info("parse command line argument");

        foreach (string arg in args)
        {
            string[] tokens = arg.Split('=');

            if (tokens.Length == 2)
            {
                arguments_.Add(tokens[0], tokens[1]);
            }
        }
    }


    /**
     * @brief Ű ���� �����ϴ� ���� �����ϴ��� Ȯ���մϴ�.
     * 
     * @param key ���� �����ϴ��� Ȯ���� Ű ���Դϴ�.
     * 
     * @return Ű ���� �����ϴ� ���� �����ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    public static bool IsValid(string key)
    {
        return arguments_.ContainsKey(key);
    }


    /**
     * @brief Ű ���� �����ϴ� ���� ����ϴ�.
     * 
     * @param key ���� ���� Ű ���Դϴ�.
     * 
     * @return Ű ���� �����ϴ� ���� ��ȯ�մϴ�.
     * 
     * @throws Ű ���� �������� ������ ���ܸ� �����ϴ�.
     */
    public static string GetValue(string key)
    {
        if(!IsValid(key))
        {
            throw new Exception("invalid key in command line...");
        }

        return arguments_[key];
    }


    /**
     * @brief ����� ������ Ű-�� ���Դϴ�.
     */
    private static Dictionary<string, string> arguments_ = new Dictionary<string, string>();
}