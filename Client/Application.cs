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

        window_ = SDL.SDL_CreateWindow(
            "FlappyBird2D",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            1000, 800,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
        );

        renderer_ = SDL.SDL_CreateRenderer(
            window_,
            -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
        );

        gameTimer_ = new Timer();

        string contentPath = CommandLine.GetValue("Content");

        Background background = new Background();
        background.SetTexture(new Texture(renderer_, contentPath + "Background.png"));
        background.SetRigidBody(new RigidBody(new Vector2<float>(500.0f, 400.0f), 1000.0f, 800.0f));

        Floor floor = new Floor();
        floor.Speed = 3.0f;
        floor.Movable = true;
        floor.SetTexture(new Texture(renderer_, contentPath + "Base.png"));
        floor.SetRigidBody(new RigidBody(new Vector2<float>(500.0f, 700.0f), 1000.0f, 200.0f));

        gameObjects_.Add(background);
        gameObjects_.Add(floor);
    }


    /**
     * @brief FlappyBird2D ������ �����մϴ�.
     */
    public void Run()
    {
        gameTimer_.Reset();

        SDL.SDL_Event e;
        while (!bIsDone_)
        {
            gameTimer_.Tick();

            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    bIsDone_ = true;
                }
            }

            SDL.SDL_SetRenderDrawColor(renderer_, 0, 0, 0, 255);
            SDL.SDL_RenderClear(renderer_);

            foreach (IGameObject gameObject in gameObjects_)
            {
                gameObject.Update(gameTimer_.GetDeltaSeconds());
                gameObject.Render(renderer_);
            }

            SDL.SDL_RenderPresent(renderer_);
        }
    }


    /**
     * @brief FlappyBird2D ���� ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        foreach (IGameObject gameObject in gameObjects_)
        {
            gameObject.Cleanup();
        }

        SDL.SDL_DestroyRenderer(renderer_);
        SDL.SDL_DestroyWindow(window_);

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
    private IntPtr window_;


    /**
     * @brief SDL �������� ������ ���Դϴ�.
     * 
     * @note �ݵ�� �Ҵ� ���� ���־�� �մϴ�.
     */
    private IntPtr renderer_;


    /**
     * @brief ���� Ÿ�̸��Դϴ�.
     */
    private Timer gameTimer_;


    /**
     * @brief ���� ���� ������Ʈ���Դϴ�.
     */
    private List<IGameObject> gameObjects_ = new List<IGameObject>();
}


/**
 * @brief Ŭ���̾�Ʈ ���ø����̼��� �����մϴ�.
 */
class ClientApplication
{
    /**
     * @brief Ŭ���̾�Ʈ�� �����մϴ�.
     * 
     * @param args ����� �μ��Դϴ�.
     */
    static void Main(string[] args)
    {
        CommandLine.Parse(args);
        FlappyBird2D Game = new FlappyBird2D();

        Game.Setup();
        Game.Run();
        Game.Cleanup();
    }
}