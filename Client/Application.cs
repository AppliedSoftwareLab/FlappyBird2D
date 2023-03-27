using System;
using System.Collections.Generic;
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
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CrashHandler.DetectApplicationCrash);

        SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
        SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG | SDL_image.IMG_InitFlags.IMG_INIT_JPG);

        Window_ = SDL.SDL_CreateWindow(
            "FlappyBird2D",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            1000, 800,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
        );

        Renderer_ = SDL.SDL_CreateRenderer(
            Window_,
            -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
        );

        GameTimer_ = new Timer();

        string ContentPath = CommandLine.GetValue("Content");

        Background BGObject = new Background();
        BGObject.Center = new Vector2<float>(500.0f, 400.0f);
        BGObject.Width = 1000.0f;
        BGObject.Height = 800.0f;
        BGObject.SetTexture(new Texture(Renderer_, ContentPath + "Background.png"));

        Floor FloorObject = new Floor();
        FloorObject.Center = new Vector2<float>(500.0f, 700.0f);
        FloorObject.Width = 1000.0f;
        FloorObject.Height = 200.0f;
        FloorObject.Speed = 3.0f;
        FloorObject.Movable = true;
        FloorObject.SetTexture(new Texture(Renderer_, ContentPath + "Base.png"));

        GameObjects_.Add(BGObject);
        GameObjects_.Add(FloorObject);
    }


    /**
     * @brief FlappyBird2D ������ �����մϴ�.
     */
    public void Run()
    {
        GameTimer_.Reset();

        SDL.SDL_Event Event;
        while (!bIsDone_)
        {
            GameTimer_.Tick();

            while (SDL.SDL_PollEvent(out Event) != 0)
            {
                if (Event.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    bIsDone_ = true;
                }
            }

            SDL.SDL_SetRenderDrawColor(Renderer_, 0, 0, 0, 255);
            SDL.SDL_RenderClear(Renderer_);

            foreach (IGameObject GameObject in GameObjects_)
            {
                GameObject.Update(GameTimer_.GetDeltaSeconds());
                GameObject.Render(Renderer_);
            }

            SDL.SDL_RenderPresent(Renderer_);
        }
    }


    /**
     * @brief FlappyBird2D ���� ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        foreach (IGameObject GameObject in GameObjects_)
        {
            GameObject.Cleanup();
        }

        SDL.SDL_DestroyRenderer(Renderer_);
        SDL.SDL_DestroyWindow(Window_);

        SDL_image.IMG_Quit();
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


    /**
     * @brief SDL �������� ������ ���Դϴ�.
     * 
     * @note �ݵ�� �Ҵ� ���� ���־�� �մϴ�.
     */
    private IntPtr Renderer_;


    /**
     * @brief ���� Ÿ�̸��Դϴ�.
     */
    private Timer GameTimer_;


    /**
     * @brief ���� ���� ������Ʈ���Դϴ�.
     */
    private List<IGameObject> GameObjects_ = new List<IGameObject>();
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
        CommandLine.Parse(Args);
        FlappyBird2D Game = new FlappyBird2D();

        Game.Setup();
        Game.Run();
        Game.Cleanup();
    }
}