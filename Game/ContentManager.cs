using System;
using System.Collections.Generic;


/**
 * @brief ���� ���� �������� �����ϰ� �����ϴ� �̱��� Ŭ�����Դϴ�.
 */
class ContentManager
{
    /**
     * @brief ���� ���� �������� �����ϴ� �Ŵ����� �ν��Ͻ��� ����ϴ�.
     * 
     * @return ���� ���� �������� �����ϴ� �Ŵ����� �ν��Ͻ��� ��ȯ�մϴ�.
     */
    public static ContentManager Get()
    {
        if(contentManager_ == null)
        {
            contentManager_ = new ContentManager();
        }

        return contentManager_;
    }


    /**
     * @brief ���� ���� �������� �����ϴ� �Ŵ����� �ʱ�ȭ�մϴ�.
     * 
     * @param contentPath ���� ���� �������� �ִ� �ֻ��� ����Դϴ�.
     */
    public void Setup(string contentPath)
    {
        if (bIsSetup_) return;

        contentPath_ = contentPath;
        contents_ = new Dictionary<string, IContent>();

        bIsSetup_ = true;
    }


    /**
     * @brief ���� ���� ������ ���ҽ����� ��������� �����մϴ�.
     */
    public void Cleanup()
    {
        if (!bIsSetup_) return;

        foreach(KeyValuePair<string, IContent> contentKeyValue in contents_)
        {
            IContent content = contentKeyValue.Value;
            content.Release();
        }

        bIsSetup_ = false;
    }


    /**
     * @brief �ñ״�ó ���� �����ϴ� ���ҽ��� �ִ��� Ȯ���մϴ�.
     * 
     * @param signature ���ҽ��� �ִ��� Ȯ���� �ñ״�ó ���Դϴ�.
     * 
     * @return �ñ״�ó ���� �����ϴ� ���ҽ��� �ִٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
     */
    public bool IsValid(string signature)
    {
        return contents_.ContainsKey(signature);
    }


    /**
     * @brief �ؽ�ó ���ҽ��� �����մϴ�.
     * 
     * @note �̶�, �ؽ�ó ���ҽ��� ��δ� Content ���� �����Դϴ�.
     * 
     * @param signature �ٸ� �ؽ�ó ���ҽ��� �����ϱ� ���� �ñ״�ó ���Դϴ�.
     * @param path �ؽ�ó ���ҽ��� ����Դϴ�. 
     * 
     * @throws 
     * - �ñ״�ó ���� �̹� �����ϸ� ���ܸ� �����ϴ�.
     * - �ؽ�ó ���ҽ� ������ �����ϸ� ���ܸ� �����ϴ�.
     */
    public Texture CreateTexture(string signature, string path)
    {
        if(IsValid(signature))
        {
            throw new Exception("collision texture resource signature...");
        }

        Texture texture = new Texture(contentPath_ + path);
        contents_.Add(signature, texture);

        return texture;
    }


    /**
     * @brief �ؽ�ó ���ҽ��� ����ϴ�.
     * 
     * @param signature �ؽ�ó ���ҽ��� �����ϴ� �ñ״�ó ���Դϴ�.
     * 
     * @throws 
     * �ñ״�ó ���� �����ϴ� �ؽ�ó ���ҽ��� �������� ������ ���ܸ� �����ϴ�.
     * �ñ״�ó ���� �����ϴ� �������� �ؽ�ó�� �ƴϸ� ���ܸ� �����ϴ�.
     * 
     * @return �ñ״�ó ���� �����ϴ� �ؽ�ó ���ҽ��Դϴ�.
     */
    public Texture GetTexture(string signature)
    {
        if(!IsValid(signature))
        {
            throw new Exception("can't find texture resource from signature...");
        }

        IContent content = contents_[signature];
        if(!(content is Texture))
        {
            throw new Exception("signature isn't texture resource...");
        }

        return (content as Texture);
    }


    /**
     * @brief ���� ���ҽ��� �����մϴ�.
     * 
     * @note �̶�, ���� ���ҽ��� ��δ� Content ���� �����Դϴ�.
     * 
     * @param signature �ٸ� ���� ���ҽ��� �����ϱ� ���� �ñ״�ó ���Դϴ�.
     * @param path ���� ���ҽ��� ����Դϴ�. 
     * 
     * @throws 
     * - �ñ״�ó ���� �̹� �����ϸ� ���ܸ� �����ϴ�.
     * - ���� ���ҽ� ������ �����ϸ� ���ܸ� �����ϴ�.
     */
    public Sound CreateSound(string signature, string path)
    {
        if (IsValid(signature))
        {
            throw new Exception("collision sound resource signature...");
        }

        Sound sound = new Sound(contentPath_ + path);
        contents_.Add(signature, sound);

        return sound;
    }


    /**
     * @brief ���� ���ҽ��� ����ϴ�.
     * 
     * @param signature ���� ���ҽ��� �����ϴ� �ñ״�ó ���Դϴ�.
     * 
     * @throws 
     * �ñ״�ó ���� �����ϴ� ���� ���ҽ��� �������� ������ ���ܸ� �����ϴ�.
     * �ñ״�ó ���� �����ϴ� �������� ���尡 �ƴϸ� ���ܸ� �����ϴ�.
     * 
     * @return �ñ״�ó ���� �����ϴ� ���� ���ҽ��Դϴ�.
     */
    public Sound GetSound(string signature)
    {
        if (!IsValid(signature))
        {
            throw new Exception("can't find sound resource from signature...");
        }

        IContent content = contents_[signature];
        if (!(content is Sound))
        {
            throw new Exception("signature isn't sound resource...");
        }

        return (content as Sound);
    }


    /**
     * @brief ���� �������� �����մϴ�.
     * 
     * @note �ñ״�ó ���� �����ϴ� ���� ������ ���ҽ��� �������� ������ �ƹ� ���۵� �������� �ʽ��ϴ�.
     * 
     * @param signature ���� ������ ���ҽ��� �����ϴ� �ñ״�ó ���Դϴ�.
     */
    public void RemoveContent(string signature)
    {
        if (!IsValid(signature)) return;

        IContent content = contents_[signature];
        contents_.Remove(signature);
    }


    /**
     * @brief �����ڴ� �ܺο��� ȣ���� �� ������ ����ϴ�.
     */
    private ContentManager() { }


    /**
     * @brief ���� ���������� ����� �ֻ��� ����Դϴ�.
     */
    private string contentPath_;


    /**
     * @brief ������ �Ŵ����� �ʱ�ȭ�� ���� �ִ��� Ȯ���մϴ�.
     */
    private bool bIsSetup_ = false;


    /**
     * @brief �Ŵ����� �����ϴ� ���� ���� �������Դϴ�.
     */
    private Dictionary<string, IContent> contents_;


    /**
     * @brief �������� �����ϴ� ������ �Ŵ����� �ν��Ͻ��Դϴ�.
     */
    private static ContentManager contentManager_;
}