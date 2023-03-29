using System;
using SDL2;


/**
 * @brief �������� �����ϴ� �̱��� Ŭ�����Դϴ�.
 */
class RenderManager
{
    /**
     * @brief �������� �����ϴ� ���� �Ŵ����� �ν��Ͻ��� ����ϴ�.
     * 
     * @return ���� �Ŵ��� �ν��Ͻ��� �����ڸ� ��ȯ�մϴ�.
     */
    public static RenderManager Get()
    {
        if(renderManager_ == null)
        {
            renderManager_ = new RenderManager();
        }

        return renderManager_;
    }


    /**
     * @brief ���� �Ŵ����� �ʱ�ȭ�մϴ�.
     * 
     * @param window �������� ������ �� ������ SDL ������ �������Դϴ�.
     * @param bIsAccelerated �������� �ϵ���� ���� �����Դϴ�. �⺻ ���� true�Դϴ�.
     * @param bIsSync �������� ���� ����ȭ �����Դϴ�. �⺻ ���� true�Դϴ�.
     * 
     * @throw �ʱ�ȭ�� �����ϸ� ���ܸ� �����ϴ�.
     */
    public void Setup(IntPtr window, bool bIsAccelerated = true, bool bIsSync = true)
    {
        int Flags = 0;
        Flags |= (bIsAccelerated ? (int)(SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED) : 0);
        Flags |= (bIsSync ? (int)(SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC) : 0);

        renderer_ = SDL.SDL_CreateRenderer(window, -1, (SDL.SDL_RendererFlags)(Flags));

        if (renderer_ == IntPtr.Zero)
        {
            throw new Exception("failed to create renderer...");
        }
    }


    /**
     * @brief ���� �Ŵ��� ���� ���ҽ��� ��������� �����մϴ�.
     */
    public void Cleanup()
    {
        SDL.SDL_DestroyRenderer(renderer_);
    }


    /**
     * @brief SDL �������� �����͸� ����ϴ�.
     * 
     * @return SDL �������� ������ ���� ��ȯ�մϴ�.
     */
    public IntPtr GetRendererPtr()
    {
        return renderer_;
    }


    /**
     * @brief ����۸� �ʱ�ȭ�մϴ�.
     * 
     * @param red �ʱ�ȭ �� ������ R���Դϴ�. ������ 0.0 ~ 1.0�Դϴ�.
     * @param green �ʱ�ȭ �� ������ G���Դϴ�. ������ 0.0 ~ 1.0�Դϴ�.
     * @param blue �ʱ�ȭ �� ������ B���Դϴ�. ������ 0.0 ~ 1.0�Դϴ�.
     * @param alpha �ʱ�ȭ �� ������ A���Դϴ�. ������ 0.0 ~ 1.0�Դϴ�.
     */
    public void Clear(float red, float green, float blue, float alpha)
    {
        SetDrawColor(red, green, blue, alpha);

        if(SDL.SDL_RenderClear(renderer_) != 0)
        {
            throw new Exception("failed to clear back buffer...");
        }
    }


    /**
     * @brief ����ۿ� ����Ʈ ���۸� �����մϴ�.
     */
    public void Present()
    {
        SDL.SDL_RenderPresent(renderer_);
    }


    /**
     * @brief ����ۿ� �ؽ�ó�� �׸��ϴ�.
     * 
     * @param texture ����ۿ� �׸� �ؽ�ó ���ҽ��Դϴ�.
     * @param center �ؽ�ó�� ȭ�� �� �߽� ��ǥ�Դϴ�.
     * @param width �ؽ�ó�� ���� ũ���Դϴ�.
     * @param height �ؽ�ó�� ���� ũ���Դϴ�.
     * @param rotate �ؽ�ó�� ȸ�� ���� ���Դϴ�. ������ ���ʺй��̰� �⺻ ���� 0.0�Դϴ�.
     * 
     * @throws �ؽ�ó�� ����ۿ� �׸��� �� �����ϸ� ���ܸ� �����ϴ�.
     */
    public void DrawTexture(ref Texture texture, Vector2<float> center, float width, float height, float rotate = 0.0f)
    {
        SDL.SDL_Rect Rect;
        Rect.x = (int)(center.x - width / 2.0f);
        Rect.y = (int)(center.y - height / 2.0f);
        Rect.w = (int)(width);
        Rect.h = (int)(height);

        SDL.SDL_Point Point;
        Point.x = (int)(center.x);
        Point.y = (int)(center.y);

        if (SDL.SDL_RenderCopyEx(
            renderer_, 
            texture.Resource, 
            IntPtr.Zero, 
            ref Rect, 
            rotate, 
            ref Point, 
            SDL.SDL_RendererFlip.SDL_FLIP_NONE) != 0)
        {
            throw new Exception("failed to draw texture in back buffer...");
        }
    }


    /**
     * @brief ���η� �����̴� �ؽ�ó�� ����ۿ� �׸��ϴ�.
     * 
     * @note �ؽ�ó ���� ������ ������ �����ϴ�.
     * 
     * ������������������������������������������������������������������
     * ��            ��                  ��
     * ��            ��                  ��
     * ��            ��                  ��
     * ��            ��                  ��
     * �� 1.0f - rate��       rate       ��
     * ��            ��                  ��
     * ��            ��                  ��
     * ��            ��                  ��
     * ������������������������������������������������������������������
     * 
     * @param texture ����ۿ� �׸� �ؽ�ó ���ҽ��Դϴ�.
     * @param center �ؽ�ó�� ȭ�� �� �߽� ��ǥ�Դϴ�.
     * @param width �ؽ�ó�� ���� ũ���Դϴ�.
     * @param height �ؽ�ó�� ���� ũ���Դϴ�.
     * @param rate �ؽ�ó ���� �����Դϴ�.
     */
    public void DrawHorizonScrollingTexture(ref Texture texture, Vector2<float> center, float width, float height, float rate)
    {
        float texWidth = texture.Width;
        float texHeight = texture.Height;

        SDL.SDL_Rect leftSrcRect;
        leftSrcRect.x = (int)(texWidth * rate);
        leftSrcRect.y = 0;
        leftSrcRect.w = (int)(texWidth * (1.0f - rate));
        leftSrcRect.h = (int)texHeight;

        SDL.SDL_Rect leftDstRect;
        leftDstRect.x = (int)(center.x - width / 2.0f);
        leftDstRect.y = (int)(center.y - height / 2.0f);
        leftDstRect.w = (int)(width * (1.0f - rate));
        leftDstRect.h = (int)(height);

        if(SDL.SDL_RenderCopy(renderer_, texture.Resource, ref leftSrcRect, ref leftDstRect) != 0)
        {
            throw new Exception("failed to draw horizon scrolling left texture...");
        }

        SDL.SDL_Rect rightSrcRect;
        rightSrcRect.x = 0;
        rightSrcRect.y = 0;
        rightSrcRect.w = (int)(texWidth * rate);
        rightSrcRect.h = (int)texHeight;

        SDL.SDL_Rect rightDstRect;
        rightDstRect.x = (int)(center.x - width / 2.0f + width * (1.0f - rate));
        rightDstRect.y = (int)(center.y - height / 2.0f);
        rightDstRect.w = (int)(width * rate);
        rightDstRect.h = (int)(height);

        if(SDL.SDL_RenderCopy(renderer_, texture.Resource, ref rightSrcRect, ref rightDstRect) != 0)
        {
            throw new Exception("failed to draw horizon scrolling right texture...");
        }
    }


    /**
     * @brief ���η� �����̴� �ؽ�ó�� ����ۿ� �׸��ϴ�.
     * 
     * @note �ؽ�ó ���� ������ ������ �����ϴ�.
     * 
     * ��������������������������������������������������������������
     * ��                             ��
     * ��                             ��
     * ��         1.0f - rate         ��
     * ��                             ��
     * ��������������������������������������������������������������
     * ��                             ��
     * ��            rate             ��
     * ��                             ��
     * ��������������������������������������������������������������
     * 
     * @param texture ����ۿ� �׸� �ؽ�ó ���ҽ��Դϴ�.
     * @param center �ؽ�ó�� ȭ�� �� �߽� ��ǥ�Դϴ�.
     * @param width �ؽ�ó�� ���� ũ���Դϴ�.
     * @param height �ؽ�ó�� ���� ũ���Դϴ�.
     * @param rate �ؽ�ó ���� �����Դϴ�.
     */
    public void DrawVerticalScrollingTexture(ref Texture texture, Vector2<float> center, float width, float height, float rate)
    {
        float texWidth = texture.Width;
        float texHeight = texture.Height;

        SDL.SDL_Rect topSrcRect;
        topSrcRect.x = 0;
        topSrcRect.y = (int)(texHeight * rate);
        topSrcRect.w = (int)texWidth;
        topSrcRect.h = (int)(texHeight * (1.0f - rate));

        SDL.SDL_Rect topDstRect;
        topDstRect.x = (int)(center.x - width / 2.0f);
        topDstRect.y = (int)(center.y - height / 2.0f);
        topDstRect.w = (int)width;
        topDstRect.h = (int)(height * (1.0f - rate));

        if (SDL.SDL_RenderCopy(renderer_, texture.Resource, ref topSrcRect, ref topDstRect) != 0)
        {
            throw new Exception("failed to draw horizon scrolling top texture...");
        }

        SDL.SDL_Rect bottomSrcRect;
        bottomSrcRect.x = 0;
        bottomSrcRect.y = 0;
        bottomSrcRect.w = (int)texWidth;
        bottomSrcRect.h = (int)(texHeight * rate);

        SDL.SDL_Rect bottomDstRect;
        bottomDstRect.x = (int)(center.x - width / 2.0f);
        bottomDstRect.y = (int)(center.y - height / 2.0f + height * (1.0f - rate));
        bottomDstRect.w = (int)width;
        bottomDstRect.h = (int)(height * rate);

        if (SDL.SDL_RenderCopy(renderer_, texture.Resource, ref bottomSrcRect, ref bottomDstRect) != 0)
        {
            throw new Exception("failed to draw horizon scrolling bottom texture...");
        }
    }


    /**
     * @brief �����ڴ� �ܺο��� ȣ���� �� ������ ����ϴ�.
     */
    private RenderManager() { }


    /**
     * @brief SDL ������ ���ؽ�Ʈ�� ������ �����մϴ�.
     * 
     * @param Red ������ ������ R���Դϴ�. ������ 0.0 ~ 1.0�Դϴ�.
     * @param Green ������ ������ G���Դϴ�. ������ 0.0 ~ 1.0�Դϴ�.
     * @param Blue ������ ������ B���Դϴ�. ������ 0.0 ~ 1.0�Դϴ�.
     * @param Alpha ������ ������ A���Դϴ�. ������ 0.0 ~ 1.0�Դϴ�.
     * 
     * @throws ���� ������ �����ϸ� ���ܸ� �����ϴ�.
     */
    private void SetDrawColor(float Red, float Green, float Blue, float Alpha)
    {
        byte r = (byte)(Red   * 255.0f);
        byte g = (byte)(Green * 255.0f);
        byte b = (byte)(Blue  * 255.0f);
        byte a = (byte)(Alpha * 255.0f);

        if(SDL.SDL_SetRenderDrawColor(renderer_, r, g, b, a) != 0)
        {
            throw new Exception("failed to set draw color...");
        }
    }


    /**
     * @brief SDL �������� �������Դϴ�.
     */
    private IntPtr renderer_;


    /**
     * @brief �������� �����ϴ� ���� �Ŵ����� �ν��Ͻ��Դϴ�.
     */
    private static RenderManager renderManager_;
}