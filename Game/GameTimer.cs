using SDL2;


/**
 * @brief ���� ���� �ð� ������ ���� Ÿ�̸� Ŭ�����Դϴ�.
 */
class GameTimer
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
            return (float)(currTime_ - prevTime_);
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
            return (float)(stopTime_ - pausedTime_ - baseTime_);
        }
        else
        {
            return (float)(currTime_ - pausedTime_ - baseTime_);
        }
    }


	/**
	 * Ÿ�̸��� ��� ���¸� �����մϴ�.
	 */
	public void Reset()
    {
		ulong tick = SDL.SDL_GetTicks64();

        bIsStop_ = false;
        baseTime_ = tick;
        pausedTime_ = 0UL;
        stopTime_ = 0UL;
        prevTime_ = tick;
        currTime_ = tick;
    }


	/**
	 * Ÿ�̸Ӹ� �����մϴ�.
	 * �̶�, Ÿ�̸Ӱ� �����Ǿ� �ִٸ� �߽õ� �������� ���۵˴ϴ�.
	 */
	public void Start()
    {
        if (bIsStop_)
        {
			ulong tick = SDL.SDL_GetTicks64();

            pausedTime_ += (tick - stopTime_);
            prevTime_ = tick;
            stopTime_ = 0UL;

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
			ulong tick = SDL.SDL_GetTicks64();

			stopTime_ = tick;
			
            bIsStop_ = true;
        }
    }


	/**
	 * Ÿ�̸Ӹ� ������Ʈ�մϴ�.
	 */
	public void Tick()
    {
        prevTime_ = currTime_;
        currTime_ = SDL.SDL_GetTicks64();
    }


	/**
	 * Ÿ�̸��� ���� ���θ� Ȯ���մϴ�.
	 */
	private bool bIsStop_ = false;


	/**
	 * Ÿ�̸Ӱ� ���۵� �ð��Դϴ�.
	 */
	private ulong baseTime_ = 0UL;


	/**
	 * Ÿ�̸Ӱ� ������ ������ ���� �ð��� �Դϴ�.
	 */
	private ulong pausedTime_ = 0UL;


	/**
	 * Ÿ�̸Ӱ� ������ �ð��Դϴ�.
	 */
	private ulong stopTime_ = 0UL;


	/**
	 * ������ Tick ȣ�� �ð��Դϴ�.
	 */
	private ulong prevTime_ = 0UL;


	/**
	 * Tick ȣ�� �ð��Դϴ�.
	 */
	private ulong currTime_ = 0UL;
}