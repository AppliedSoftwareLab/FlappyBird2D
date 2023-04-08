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
     * @param Path ���� ���ҽ��� ����Դϴ�.
     *
     * @return ������ ���� ���ҽ��� ���̵� ��ȯ�մϴ�. ������ �����ϸ� -1�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "CreateSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern int CreateSound([MarshalAs(UnmanagedType.LPStr)] string Path);


    /**
     * @brief ������ ũ�⸦ �����մϴ�.
     *
     * @param SoundID ũ�⸦ ������ ������ ���̵��Դϴ�.
     * @param Volume ������ ũ���Դϴ�. ������ 0.0 ~ 1.0 �Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "SetSoundVolume", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetSoundVolume(int SoundID, float Volume);


    /**
     * @brief ���� ������ ũ�⸦ ����ϴ�.
     *
     * @param SoundID �Ҹ� ũ�⸦ ���� ������ ���̵��Դϴ�.
     *
     * @return ���� ������ ũ�⸦ ����ϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "GetSoundVolume", CallingConvention = CallingConvention.Cdecl)]
    public static extern float GetSoundVolume(int SoundID);


    /**
     * @brief ������ �ݺ� ���θ� �����մϴ�.
     *
     * @param SoundID �ݺ� ���θ� ������ ������ ���̵��Դϴ�.
     * @param bIsLoop ���� �ݺ� �����Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "SetSoundLooping", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetSoundLooping(int SoundID, bool bIsLoop);


    /**
     * @brief ������ �ݺ� ���θ� ����ϴ�.
     *
     * @param SoundID �ݺ� ���θ� Ȯ���� ������ ���̵��Դϴ�.
     *
     * @return ���尡 �ݺ��Ѵٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "GetSoundLooping", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool GetSoundLooping(int SoundID);


    /**
     * @brief ���带 �÷����մϴ�.
     *
     * @note ������ ������ ���� �ִٸ� ������ �������� �÷��̵˴ϴ�.
     *
     * @param SoundID �÷����� ������ ���̵��Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "PlaySound", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PlaySound(int SoundID);


    /**
     * @brief ���尡 �÷��������� Ȯ���մϴ�.
     *
     * @param SoundID �÷��� ������ Ȯ���� ���� ���̵��Դϴ�.
     *
     * @return ���尡 �÷��� ���̶�� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "IsPlayingSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsPlayingSound(int SoundID);


    /**
     * @brief ���� �÷��̰� �������� Ȯ���մϴ�.
     *
     * @param �÷��̰� �������� Ȯ���� ���� ���̵��Դϴ�.
     *
     * @return ���� �÷��̰� �����ٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "IsDoneSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsDoneSound(int SoundID);


    /**
     * @brief ���� �÷��̸� �����մϴ�.
     *
     * @param SoundID �÷��̸� ������ ���� ���̵��Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "StopSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern void StopSound(int SoundID);


    /**
     * @brief ���� �÷��̰� ���� �Ǿ����� Ȯ���մϴ�.
     *
     * @param SoundID �÷��̰� ���� �Ǿ����� Ȯ���� ���� ���̵��Դϴ�.
     *
     * @return ���� �÷��̰� ���� �Ǿ��ٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "IsStoppingSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsStoppingSound(int SoundID);


    /**
     * @brief ���带 �ʱ�ȭ�մϴ�.
     *
     * @param �ʱ�ȭ�� ������ ���̵��Դϴ�.
     */
    [DllImport("AudioModule.dll", EntryPoint = "ResetSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ResetSound(int SoundID);
}