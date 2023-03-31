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
     * @brief Ʈ�� Ÿ�� ��Ʈ ���ҽ��� �����մϴ�.
     * 
     * @note �̶�, Ʈ�� Ÿ�� ��Ʈ ���ҽ��� ��δ� Content ���� �����Դϴ�.
     * 
     * @param path Ʈ�� Ÿ�� ��Ʈ ���ҽ��� ����Դϴ�.
     * @param beginCodePoint �ؽ�ó ��Ʋ�󽺿� ǥ���� ���� �����Դϴ�.
     * @param endCodePoint �ؽ�ó ��Ʋ�󽺿� ǥ���� �� �����Դϴ�.
     * @param size ��Ʈ�� ũ���Դϴ�.
     * 
     * @throws
     * - �ñ״�ó ���� �̹� �����ϸ� ���ܸ� �����ϴ�.
     * - Ʈ�� Ÿ�� ��Ʈ ���ҽ� ������ �����ϸ� ���ܸ� �����ϴ�.
     */
    public TTFont CreateTTFont(string signature, string path, ushort beginCodePoint, ushort endCodePoint, int size)
    {
        if (IsValid(signature))
        {
            throw new Exception("collision true type font resource signature...");
        }

        beginCodePoint = Math.Max(beginCodePoint, (ushort)0);
        endCodePoint = Math.Max(endCodePoint, (ushort)0);

        beginCodePoint = Math.Min(beginCodePoint, endCodePoint);
        endCodePoint = Math.Max(beginCodePoint, endCodePoint);

        TTFont font = new TTFont(contentPath_ + path, beginCodePoint, endCodePoint, size);
        contents_.Add(signature, font);

        return font;
    }


    /**
     * @brief Ʈ�� Ÿ�� ��Ʈ ���ҽ��� ����ϴ�.
     * 
     * @param signature Ʈ�� Ÿ�� ��Ʈ ���ҽ��� �����ϴ� �ñ״�ó ���Դϴ�.
     * 
     * @throws 
     * �ñ״�ó ���� �����ϴ� Ʈ�� Ÿ�� ��Ʈ ���ҽ��� �������� ������ ���ܸ� �����ϴ�.
     * �ñ״�ó ���� �����ϴ� �������� Ʈ�� Ÿ�� ��Ʈ�� �ƴϸ� ���ܸ� �����ϴ�.
     * 
     * @return �ñ״�ó ���� �����ϴ� �ؽ�ó ���ҽ��Դϴ�.
     */
    public TTFont GetTTFont(string signature)
    {
        if (!IsValid(signature))
        {
            throw new Exception("can't find true type font resource from signature...");
        }

        IContent content = contents_[signature];
        if (!(content is TTFont))
        {
            throw new Exception("signature isn't true type font resource...");
        }

        return (content as TTFont);
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
    private Dictionary<string, IContent> contents_ = new Dictionary<string, IContent>();


    /**
     * @brief �������� �����ϴ� ������ �Ŵ����� �ν��Ͻ��Դϴ�.
     */
    private static ContentManager contentManager_;
}