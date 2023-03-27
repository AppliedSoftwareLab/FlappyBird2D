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
     * 
     * @param Args ����� �μ��Դϴ�.
     */
    public void Setup(string[] Args)
    {
        foreach (string Arg in Args)
        {
            string[] Tokens = Arg.Split('=');

            if(Tokens.Length == 2)
            {
                Arguments_.Add(Tokens[0], Tokens[1]);
            }
        }

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

        string BGImagePath = Arguments_["Content"] + "Background.png";
        IntPtr BGSurface = SDL_image.IMG_Load(BGImagePath);
        SDL.SDL_Log(SDL.SDL_GetError());
        BGTexture_ = SDL.SDL_CreateTextureFromSurface(Renderer_, BGSurface);

        SDL.SDL_FreeSurface(BGSurface);
    }


    /**
     * @brief FlappyBird2D ������ �����մϴ�.
     */
    public void Run()
    {
        SDL.SDL_Rect Rect;
        Rect.x = 0;
        Rect.y = 0;
        Rect.w = 1000;
        Rect.h = 800;

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

            SDL.SDL_SetRenderDrawColor(Renderer_, 0, 0, 0, 255);
            SDL.SDL_RenderClear(Renderer_);

            SDL.SDL_RenderCopy(Renderer_, BGTexture_, IntPtr.Zero, ref Rect);

            SDL.SDL_RenderPresent(Renderer_);
        }
    }


    /**
     * @brief FlappyBird2D ���� ���� ���ҽ��� �����մϴ�.
     */
    public void Cleanup()
    {
        SDL_image.IMG_Quit();

        SDL.SDL_DestroyTexture(BGTexture_);

        SDL.SDL_DestroyRenderer(Renderer_);
        SDL.SDL_DestroyWindow(Window_);
        SDL.SDL_Quit();
    }


    /**
     * @brief ����� ������ Ű-�� ���Դϴ�.
     */
    private Dictionary<string, string> Arguments_ = new Dictionary<string, string>();


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
     * @brief ��׶��� �ؽ�ó ���ҽ��Դϴ�.
     */
    private IntPtr BGTexture_;
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

        Game.Setup(Args);
        Game.Run();
        Game.Cleanup();
    }
}