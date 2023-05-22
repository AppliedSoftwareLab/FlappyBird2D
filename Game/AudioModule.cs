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
    [DllImport("AudioModule.dll", EntryPoint = "Setup", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Setup();


    /**
     * @brief ����� ����� �ʱ�ȭ�� �����մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "Cleanup", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Cleanup();


    /**
     * @brief ���� ���ҽ��� �����մϴ�.
     *
     * @param path ���� ���ҽ��� ����Դϴ�.
     *
     * @return ������ ���� ���ҽ��� ���̵� ��ȯ�մϴ�. ������ �����ϸ� -1�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "CreateSound", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int CreateSound([MarshalAs(UnmanagedType.LPStr)] string path);


    /**
     * @brief ������ ũ�⸦ �����մϴ�.
     *
     * @param soundID ũ�⸦ ������ ������ ���̵��Դϴ�.
     * @param volume ������ ũ���Դϴ�. ������ 0.0 ~ 1.0 �Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "SetSoundVolume", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetSoundVolume(int soundID, float volume);


    /**
     * @brief ���� ������ ũ�⸦ ����ϴ�.
     *
     * @param soundID �Ҹ� ũ�⸦ ���� ������ ���̵��Դϴ�.
     *
     * @return ���� ������ ũ�⸦ ����ϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "GetSoundVolume", CallingConvention = CallingConvention.Cdecl)]
    public static extern float GetSoundVolume(int soundID);


    /**
     * @brief ������ �ݺ� ���θ� �����մϴ�.
     *
     * @param soundID �ݺ� ���θ� ������ ������ ���̵��Դϴ�.
     * @param bIsLoop ���� �ݺ� �����Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "SetSoundLooping", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetSoundLooping(int soundID, bool bIsLoop);


    /**
     * @brief ������ �ݺ� ���θ� ����ϴ�.
     *
     * @param soundID �ݺ� ���θ� Ȯ���� ������ ���̵��Դϴ�.
     *
     * @return ���尡 �ݺ��Ѵٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "GetSoundLooping", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool GetSoundLooping(int soundID);


    /**
     * @brief ���带 �÷����մϴ�.
     *
     * @note ������ ������ ���� �ִٸ� ������ �������� �÷��̵˴ϴ�.
     *
     * @param soundID �÷����� ������ ���̵��Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "PlaySound", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PlaySound(int soundID);


    /**
     * @brief ���尡 �÷��������� Ȯ���մϴ�.
     *
     * @param soundID �÷��� ������ Ȯ���� ���� ���̵��Դϴ�.
     *
     * @return ���尡 �÷��� ���̶�� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "IsPlayingSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsPlayingSound(int soundID);


    /**
     * @brief ���� �÷��̰� �������� Ȯ���մϴ�.
     *
     * @param �÷��̰� �������� Ȯ���� ���� ���̵��Դϴ�.
     *
     * @return ���� �÷��̰� �����ٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "IsDoneSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsDoneSound(int soundID);


    /**
     * @brief ���� �÷��̸� �����մϴ�.
     *
     * @param soundID �÷��̸� ������ ���� ���̵��Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "StopSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern void StopSound(int soundID);


    /**
     * @brief ���� �÷��̰� ���� �Ǿ����� Ȯ���մϴ�.
     *
     * @param soundID �÷��̰� ���� �Ǿ����� Ȯ���� ���� ���̵��Դϴ�.
     *
     * @return ���� �÷��̰� ���� �Ǿ��ٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "IsStoppingSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsStoppingSound(int soundID);


    /**
     * @brief ���带 �ʱ�ȭ�մϴ�.
     *
     * @param �ʱ�ȭ�� ������ ���̵��Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "ResetSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ResetSound(int soundID);
}