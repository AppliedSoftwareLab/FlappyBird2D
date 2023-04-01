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
        if(SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0)
        {
            throw new Exception("failed to initialize SDL...");
        }

        int Flags = (int)(SDL_image.IMG_InitFlags.IMG_INIT_PNG | SDL_image.IMG_InitFlags.IMG_INIT_JPG);
        int InitFlag = SDL_image.IMG_Init((SDL_image.IMG_InitFlags)(Flags));

        if((InitFlag & Flags) != Flags)
        {
            throw new Exception("failed to initialize SDL_image...");
        }

        if (SDL_ttf.TTF_Init() != 0)
        {
            throw new Exception("failed to initialize SDL_ttf...");
        }

        window_ = SDL.SDL_CreateWindow(
            "FlappyBird2D",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            1000, 800,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
        );

        if(window_ == IntPtr.Zero)
        {
            throw new Exception("failed to create window...");
        }

        InputManager.Get().Setup();
        InputManager.Get().BindWindowEventAction(EWindowEvent.CLOSE, () => { bIsDone_ = true; });

        RenderManager.Get().Setup(window_);
        ContentManager.Get().Setup(CommandLine.GetValue("Content"));
        WorldManager.Get().Setup();

        gameTimer_ = new Timer();

        string contentPath = CommandLine.GetValue("Content");

        Background background = new Background();
        background.Plable = false;
        background.Texture = ContentManager.Get().CreateTexture("Background", "Background.png");
        background.RigidBody = new RigidBody(new Vector2<float>(500.0f, 400.0f), 1000.0f, 800.0f);

        Floor floor = new Floor();
        floor.Speed = 3.0f;
        floor.Movable = true;
        floor.Texture = ContentManager.Get().CreateTexture("Base", "Base.png");
        floor.RigidBody = new RigidBody(new Vector2<float>(500.0f, 700.0f), 1000.0f, 200.0f);

        Bird bird = new Bird();
        bird.Texture = ContentManager.Get().CreateTexture("Bird", "Bird.png");
        bird.RigidBody = new RigidBody(new Vector2<float>(400.0f, 300.0f), 45.0f, 30.0f);

        WorldManager.Get().AddGameObject("Background", background);
        WorldManager.Get().AddGameObject("Floor", floor);
        WorldManager.Get().AddGameObject("Bird", bird);

        gameObjects_.Add(background);
        gameObjects_.Add(bird);
        gameObjects_.Add(floor);
    }


    /**
     * @brief FlappyBird2D ������ �����մϴ�.
     */
    public void Run()
    {
        gameTimer_.Reset();

        while (!bIsDone_)
        {
            gameTimer_.Tick();
            InputManager.Get().Tick();

            RenderManager.Get().Clear(Color.BLACK);

            foreach (IGameObject gameObject in gameObjects_)
            {
                gameObject.Update(gameTimer_.GetDeltaSeconds());
                gameObject.Render();
            }

            RenderManager.Get().Present();
        }
    }


    /**
     * @brief FlappyBird2D ���� ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        SDL.SDL_DestroyWindow(window_);

        WorldManager.Get().Cleanup();
        ContentManager.Get().Cleanup();
        RenderManager.Get().Cleanup();

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
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CrashHandler.DetectApplicationCrash);

        CommandLine.Parse(args);
        FlappyBird2D Game = new FlappyBird2D();

        Game.Setup();
        Game.Run();
        Game.Cleanup();
    }
}