using SDL2;


/**
 * @brief ���� ���� �ð� ������ ���� Ÿ�̸� Ŭ�����Դϴ�.
 */
class Timer
{

	/**
	 * Ÿ�̸��� �� ���� ��Ÿ �ð����� ����ϴ�.
	 * 
	 * @return �� ������ ��Ÿ �ð����� ��ȯ�մϴ�.
	 */
	public float GetDeltaSeconds()
    {
		return GetDeltaMilliseconds() / 1000.0f;
	}


	/**
	 * Ÿ�̸��� �и��� ������ ��Ÿ �ð����� ����ϴ�.
	 * 
	 * @return �и��� ������ ��Ÿ �ð����� ��ȯ�մϴ�.
	 */
	public float GetDeltaMilliseconds()
    {
        if (bIsStop_)
        {
            return 0.0f;
        }
        else
        {
            return (float)(CurrTime_ - PrevTime_);
        }
    }


	/**
	 * Ÿ�̸Ӱ� ������ �ð��� ������ ��ü �ð����� ��ȯ�մϴ�.
	 * 
	 * @return Ÿ�̸Ӱ� ���۵� ������ ������ �ð��� ������ ��ü �ð����� ��ȯ�մϴ�.
	 */
	public float GetTotalSeconds()
    {
		return GetTotalMilliseconds() / 1000.0f;
	}


	/**
	 * Ÿ�̸Ӱ� ������ �ð��� ������ ��ü �ð����� ��ȯ�մϴ�.
	 *
	 * @return Ÿ�̸Ӱ� ���۵� ������ ������ �ð��� ������ ��ü �ð����� ��ȯ�մϴ�.
	 */
	public float GetTotalMilliseconds()
    {
        if (bIsStop_)
        {
            return (float)(StopTime_ - PausedTime_ - BaseTime_);
        }
        else
        {
            return (float)(CurrTime_ - PausedTime_ - BaseTime_);
        }
    }


	/**
	 * Ÿ�̸��� ��� ���¸� �����մϴ�.
	 */
	public void Reset()
    {
		ulong TickTime = SDL.SDL_GetTicks64();

        bIsStop_ = false;
        BaseTime_ = TickTime;
        PausedTime_ = 0UL;
        StopTime_ = 0UL;
        PrevTime_ = TickTime;
        CurrTime_ = TickTime;
    }


	/**
	 * Ÿ�̸Ӹ� �����մϴ�.
	 * �̶�, Ÿ�̸Ӱ� �����Ǿ� �ִٸ� �߽õ� �������� ���۵˴ϴ�.
	 */
	public void Start()
    {
        if (bIsStop_)
        {
			ulong TickTime = SDL.SDL_GetTicks64();

            PausedTime_ += (TickTime - StopTime_);
            PrevTime_ = TickTime;
            StopTime_ = 0UL;

            bIsStop_ = false;
        }
    }


	/**
	 * Ÿ�̸Ӹ� ������ŵ�ϴ�.
	 */
	public void Stop()
    {
        if (!bIsStop_)
        {
			ulong TickTime = SDL.SDL_GetTicks64();

			StopTime_ = TickTime;

            bIsStop_ = true;
        }
    }


	/**
	 * Ÿ�̸Ӹ� ������Ʈ�մϴ�.
	 */
	public void Tick()
    {
        PrevTime_ = CurrTime_;
        CurrTime_ = SDL.SDL_GetTicks64();
    }


	/**
	 * Ÿ�̸��� ���� ���θ� Ȯ���մϴ�.
	 */
	private bool bIsStop_ = false;


	/**
	 * Ÿ�̸Ӱ� ���۵� �ð��Դϴ�.
	 */
	private ulong BaseTime_ = 0UL;


	/**
	 * Ÿ�̸Ӱ� ������ ������ ���� �ð��� �Դϴ�.
	 */
	private ulong PausedTime_ = 0UL;


	/**
	 * Ÿ�̸Ӱ� ������ �ð��Դϴ�.
	 */
	private ulong StopTime_ = 0UL;


	/**
	 * ������ Tick ȣ�� �ð��Դϴ�.
	 */
	private ulong PrevTime_ = 0UL;


	/**
	 * Tick ȣ�� �ð��Դϴ�.
	 */
	private ulong CurrTime_ = 0UL;
}