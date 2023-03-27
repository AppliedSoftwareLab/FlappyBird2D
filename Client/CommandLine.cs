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
     * @param Args �Ľ��� ����� �����Դϴ�.
     */
    public static void Parse(string[] Args)
    {
        foreach (string Arg in Args)
        {
            string[] Tokens = Arg.Split('=');

            if (Tokens.Length == 2)
            {
                Arguments_.Add(Tokens[0], Tokens[1]);
            }
        }
    }


    /**
     * @brief Ű ���� �����ϴ� ���� �����ϴ��� Ȯ���մϴ�.
     * 
     * @param Key ���� �����ϴ��� Ȯ���� Ű ���Դϴ�.
     * 
     * @return Ű ���� �����ϴ� ���� �����ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    public static bool IsValid(string Key)
    {
        return Arguments_.ContainsKey(Key);
    }


    /**
     * @brief Ű ���� �����ϴ� ���� ����ϴ�.
     * 
     * @param Key ���� ���� Ű ���Դϴ�.
     * 
     * @return Ű ���� �����ϴ� ���� ��ȯ�մϴ�.
     * 
     * @throws Ű ���� �������� ������ ���ܸ� �����ϴ�.
     */
    public static string GetValue(string Key)
    {
        if(!IsValid(Key))
        {
            throw new Exception("invalid key in command line...");
        }

        return Arguments_[Key];
    }


    /**
     * @brief ����� ������ Ű-�� ���Դϴ�.
     */
    private static Dictionary<string, string> Arguments_ = new Dictionary<string, string>();
}