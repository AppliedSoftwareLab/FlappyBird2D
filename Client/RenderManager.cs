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