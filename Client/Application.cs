using System;
using System.Collections.Generic;
using System.Linq;
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
        SetupCoreProperties();
        LoadTextureResource();
        GenerateGameObjects();
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
            WorldManager.Get().Tick(gameTimer_.GetDeltaSeconds());

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
     * @brief ���� ���࿡ �ʿ��� �ٽ� ��ҵ��� �ʱ�ȭ�մϴ�.
     * 
     * @throws ���� ���࿡ �ʿ��� �ٽ� ��ҵ��� �ʱ�ȭ�� �����ϸ� ���ܸ� �����ϴ�.
     */
    private void SetupCoreProperties()
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0)
        {
            throw new Exception("failed to initialize SDL...");
        }

        int flags = (int)(SDL_image.IMG_InitFlags.IMG_INIT_PNG | SDL_image.IMG_InitFlags.IMG_INIT_JPG);
        int initFlag = SDL_image.IMG_Init((SDL_image.IMG_InitFlags)(flags));

        if ((initFlag & flags) != flags)
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

        if (window_ == IntPtr.Zero)
        {
            throw new Exception("failed to create window...");
        }

        InputManager.Get().Setup();
        RenderManager.Get().Setup(window_);
        ContentManager.Get().Setup(CommandLine.GetValue("Content"));
        WorldManager.Get().Setup();

        InputManager.Get().BindWindowEventAction(EWindowEvent.CLOSE, () => { bIsDone_ = true; });
    }


    /**
     * @brief ��ü �ؽ�ó ���ҽ��� �ε��մϴ�.
     * 
     * @throws �ؽ�ó ���ҽ� ������ �����ϸ� ���ܸ� �����ϴ�.
     */
    private void LoadTextureResource()
    {
        string[] pngImageFilePaths = System.IO.Directory.GetFiles(CommandLine.GetValue("Content"), "*.png");
        Dictionary<string, string> textures = new Dictionary<string, string>();

        foreach(string pngImageFilePath in pngImageFilePaths)
        {
            string[] tokens = pngImageFilePath.Split('\\');
            string pngImageFile = tokens.Last();

            string[] pngImageFileTokens = pngImageFile.Split('.');
            textures.Add(pngImageFileTokens.First(), pngImageFile);
        }

        foreach(KeyValuePair<string, string> texture in textures)
        {
            ContentManager.Get().CreateTexture(texture.Key, texture.Value);
        }
    }


    /**
     * @brief ���� ������Ʈ���� �����մϴ�.
     */
    private void GenerateGameObjects()
    {
        Background background = new Background();
        background.UpdateOrder = 0;
        background.Active = true;
        background.CreateBody(new Vector2<float>(500.0f, 400.0f), 1000.0f, 800.0f);

        Floor floor = new Floor();
        floor.UpdateOrder = 2;
        floor.Active = false;
        floor.Speed = 300.0f;
        floor.Movable = true;
        floor.CreateBody(new Vector2<float>(500.0f, 750.0f), 1000.0f, 100.0f);

        Bird bird = new Bird();
        bird.UpdateOrder = 3;
        bird.Active = false;
        bird.CreateBody(new Vector2<float>(400.0f, 350.0f), 70.0f, 50.0f);

        PipeDetector pipeDetector = new PipeDetector();
        pipeDetector.UpdateOrder = 4;
        pipeDetector.Active = false;
        pipeDetector.CanGeneratePipe = false;
        pipeDetector.MaxPipeCount = 7;
        pipeDetector.RespawnPosition = new Vector2<float>(1200.0f, 300.0f);
        pipeDetector.PipeToPipeGapLength = 300.0f;
        pipeDetector.PipeTopAndBottomGapLength = 200.0f;
        pipeDetector.TotalPipeWidth = 100.0f;
        pipeDetector.TotalPipeHeight = 700.0f;
        pipeDetector.MinPipeHeight = 100.0f;
        pipeDetector.PipeSpeed = 300.0f;

        GameStateDetector gameStateDetector = new GameStateDetector();
        gameStateDetector.UpdateOrder = 5;
        gameStateDetector.Active = false;

        WorldManager.Get().AddGameObject("Background", background);
        WorldManager.Get().AddGameObject("Floor", floor);
        WorldManager.Get().AddGameObject("Bird", bird);
        WorldManager.Get().AddGameObject("PipeDetector", pipeDetector);
        WorldManager.Get().AddGameObject("GameStateDetector", gameStateDetector);
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
    private Timer gameTimer_ = new Timer();
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

#if SHIPPING
        ConsoleHandler.SetVisible(false);
#endif

        CommandLine.Parse(args);
        FlappyBird2D Game = new FlappyBird2D();

        Game.Setup();
        Game.Run();
        Game.Cleanup();
    }
}