using System;


/**
 * @brief ���带 �ε��ϰ� �����ϴ� Ŭ�����Դϴ�.
 */
class Sound : IContent
{
    /**
     * @brief ���带 �ε��ϰ� �����ϴ� Ŭ������ �������Դϴ�.
     * 
     * @param path ���� ���ҽ��� ����Դϴ�.
     * 
     * @thorws ���� ���ҽ� �ε��� �����ϸ� ���ܸ� �����ϴ�.
     */
    public Sound(string path)
    {
        soundID_ = AudioModule.CreateSound(path);
        if(soundID_ == -1)
        {
            throw new Exception("failed to create sound resource...");
        }
    }


    /**
     * @brief ���� ���ҽ��� �����͸� ��������� �����մϴ�.
     */
    public void Release()
    {
    }


    /**
     * @brief ������ ũ�⸦ �����մϴ�.
     *
     * @param volume ������ ũ���Դϴ�. ������ 0.0 ~ 1.0 �Դϴ�.
     */
    public void SetVolume(float volume)
    {
        AudioModule.SetSoundVolume(soundID_, volume);
    }


    /**
     * @brief ���� ������ ũ�⸦ ����ϴ�.
     *
     * @return ���� ������ ũ�⸦ ����ϴ�.
     */
    public float GetVolume()
    {
        return AudioModule.GetSoundVolume(soundID_);
    }


    /**
     * @brief ������ �ݺ� ���θ� �����մϴ�.
     *
     * @param bIsLoop ���� �ݺ� �����Դϴ�.
     */
    public void SetLooping(bool bIsLoop)
    {
        AudioModule.SetSoundLooping(soundID_, bIsLoop);
    }


    /**
     * @brief ������ �ݺ� ���θ� ����ϴ�.
     *
     * @return ���尡 �ݺ��Ѵٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    public bool GetLooping()
    {
        return AudioModule.GetSoundLooping(soundID_);
    }


    /**
     * @brief ���带 �÷����մϴ�.
     *
     * @note ������ ������ ���� �ִٸ� ������ �������� �÷��̵˴ϴ�.
     */
    public void Play()
    {
        AudioModule.PlaySound(soundID_);
    }


    /**
     * @brief ���尡 �÷��������� Ȯ���մϴ�.
     *
     * @return ���尡 �÷��� ���̶�� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    public bool IsPlaying()
    {
        return AudioModule.IsPlayingSound(soundID_);
    }


    /**
     * @brief ���� �÷��̰� �������� Ȯ���մϴ�.
     * 
     * @return ���� �÷��̰� �����ٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    public bool IsDone()
    {
        return AudioModule.IsDoneSound(soundID_);
    }


    /**
     * @brief ���� �÷��̸� �����մϴ�.
     */
    public void Stop()
    {
        AudioModule.StopSound(soundID_);
    }


    /**
     * @brief ���� �÷��̰� ���� �Ǿ����� Ȯ���մϴ�.
     * 
     * @return ���� �÷��̰� ���� �Ǿ��ٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    public bool IsStopping()
    {
        return AudioModule.IsStoppingSound(soundID_);
    }


    /**
     * @brief ���带 �ʱ�ȭ�մϴ�.
     *
     * @param �ʱ�ȭ�� ������ ���̵��Դϴ�.
     */
    public void Reset()
    {
        AudioModule.ResetSound(soundID_);
    }


    /**
     * @brief ���� ���ҽ��� ���̵��Դϴ�.
     */
    private int soundID_ = 0;
}