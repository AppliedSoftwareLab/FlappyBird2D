using System;
using SDL2;


/**
 * @brief FlappyBird2D ������ �ʱ�ȭ �� �����մϴ�.
 */
class FlappyBird2D
{
    /**
     * @brief FlappyBird2D ������ �ʱ�ȭ�մϴ�.
     */
    public void Setup()
    {
        SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

        Window_ = SDL.SDL_CreateWindow(
            "FlappyBird2D",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            1000, 800,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
        );
    }


    /**
     * @brief FlappyBird2D ������ �����մϴ�.
     */
    public void Run()
    {
        SDL.SDL_Event e;
        while (!bIsDone_)
        {
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    bIsDone_ = true;
                }
            }
        }
    }


    /**
     * @brief FlappyBird2D ���� ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        SDL.SDL_DestroyWindow(Window_);
        SDL.SDL_Quit();
    }


    /**
     * @brief ������ ������ �� Ȯ���մϴ�.
     */
    private bool bIsDone_ = false;


    /**
     * @brief SDL �������� ������ ���Դϴ�.
     * 
     * @note �ݵ�� �Ҵ� ���� ���־�� �մϴ�.
     */
    private IntPtr Window_;
}


/**
 * @brief Ŭ���̾�Ʈ ���ø����̼��� �����մϴ�.
 */
class ClientApplication
{
    /**
     * @brief Ŭ���̾�Ʈ�� �����մϴ�.
     * 
     * @param Args ����� �μ��Դϴ�.
     */
    static void Main(string[] Args)
    {
        FlappyBird2D Game = new FlappyBird2D();

        Game.Setup();
        Game.Run();
        Game.Cleanup();
    }
}