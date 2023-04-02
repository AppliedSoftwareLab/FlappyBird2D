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
        SetupCoreProperties();
        LoadTextureResource();

        Background background = new Background();
        background.UpdateOrder = 0;
        background.CreateBody(new Vector2<float>(500.0f, 400.0f), 1000.0f, 800.0f);

        Floor floor = new Floor();
        floor.UpdateOrder = 1;
        floor.Speed = 300.0f;
        floor.Movable = true;
        floor.CreateBody(new Vector2<float>(500.0f, 700.0f), 1000.0f, 200.0f);

        Bird bird = new Bird();
        bird.UpdateOrder = 3;
        bird.CreateBody(new Vector2<float>(400.0f, 300.0f), 70.0f, 50.0f);

        WorldManager.Get().AddGameObject("Background", background);
        WorldManager.Get().AddGameObject("Floor", floor);
        WorldManager.Get().AddGameObject("Bird", bird);

        CreatePipeObject();
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

            Pipe pipe = WorldManager.Get().GetGameObject("Pipe") as Pipe;
            if(pipe.State == Pipe.EState.LEAVE)
            {
                WorldManager.Get().RemoveGameObject("Pipe");
                CreatePipeObject();
            }

            Bird bird = WorldManager.Get().GetGameObject("Bird") as Bird;
            if(bird.State == Bird.EState.DONE)
            {
                pipe.Movable = false;

                Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
                floor.Movable = false;
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

        int Flags = (int)(SDL_image.IMG_InitFlags.IMG_INIT_PNG | SDL_image.IMG_InitFlags.IMG_INIT_JPG);
        int InitFlag = SDL_image.IMG_Init((SDL_image.IMG_InitFlags)(Flags));

        if ((InitFlag & Flags) != Flags)
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
     * @brief �ؽ�ó ���ҽ��� �ε��մϴ�.
     * 
     * @throws �ؽ�ó ���ҽ� ������ �����ϸ� ���ܸ� �����ϴ�.
     */
    private void LoadTextureResource()
    {
        Dictionary<string, string> textures = new Dictionary<string, string>()
        {
            {     "Background",     "Background.png" },
            {        "PipeTop",        "PipeTop.png" },
            {     "PipeBottom",     "PipeBottom.png" },
            { "BirdWingNormal", "BirdWingNormal.png" },
            {   "BirdWingDown",   "BirdWingDown.png" },
            {     "BirdWingUp",     "BirdWingUp.png" },
            {          "Floor",          "Floor.png" },

        };

        foreach(KeyValuePair<string, string> texture in textures)
        {
            ContentManager.Get().CreateTexture(texture.Key, texture.Value);
        }
    }


    /**
     * @brief ������ ������Ʈ�� �߰��մϴ�.
     */
    private void CreatePipeObject()
    {
        Pipe pipe = new Pipe();
        pipe.UpdateOrder = 2;
        pipe.Movable = true;
        pipe.Speed = 300.0f;
        pipe.TopRigidBody = new RigidBody(new Vector2<float>(1100.0f, 100.0f), 100.0f, 200.0f);
        pipe.BottomRigidBody = new RigidBody(new Vector2<float>(1100.0f, 500.0f), 100.0f, 200.0f);

        WorldManager.Get().AddGameObject("Pipe", pipe);
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

        CommandLine.Parse(args);
        FlappyBird2D Game = new FlappyBird2D();

        Game.Setup();
        Game.Run();
        Game.Cleanup();
    }
}