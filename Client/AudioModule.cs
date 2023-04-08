using System;
using System.Runtime.InteropServices;


/**
 * @brief ����� ��� ���̺귯���� C# ���ε��Դϴ�.
 */
class AudioModule
{
    /**
     * @brief ����� ����� �ʱ�ȭ�մϴ�.
     * 
     * @return ����� ��� �ʱ�ȭ�� �����ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll")]
    public static extern bool Setup();


    /**
     * @brief ����� ����� �ʱ�ȭ�� �����մϴ�.
     */
    [DllImport("AudioModule.dll")]
    public static extern void Cleanup();
}