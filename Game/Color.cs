using System;


/**
 * @brief RGBA ������ ��Ÿ���� Ŭ�����Դϴ�.
 */
struct Color
{
    /**
     * @brief ���ڰ� ��� float ������ RGBA ������ ��Ÿ���� Ŭ������ �������Դϴ�.
     * 
     * @note 0.0 ~ 1.0 ������ ���� ó���� �� �ֽ��ϴ�.
     * 
     * @param rr RGBA ���� �� float Ÿ���� R���Դϴ�.
     * @param gg RGBA ���� �� float Ÿ���� G���Դϴ�.
     * @param bb RGBA ���� �� float Ÿ���� B���Դϴ�.
     * @param aa RGBA ���� �� float Ÿ���� A���Դϴ�.
     */
    public Color(float rr, float gg, float bb, float aa)
    {
        r = Math.Min(Math.Max(0.0f, rr), 1.0f);
        g = Math.Min(Math.Max(0.0f, gg), 1.0f);
        b = Math.Min(Math.Max(0.0f, bb), 1.0f);
        a = Math.Min(Math.Max(0.0f, aa), 1.0f);
    }


    /**
     * @brief ���ڰ� ��� byte ������ RGBA ������ ��Ÿ���� Ŭ������ �������Դϴ�.
     * 
     * @param rr RGBA ���� �� byte Ÿ���� R���Դϴ�.
     * @param gg RGBA ���� �� byte Ÿ���� G���Դϴ�.
     * @param bb RGBA ���� �� byte Ÿ���� B���Դϴ�.
     * @param aa RGBA ���� �� byte Ÿ���� A���Դϴ�.
     */
    public Color(byte rr, byte gg, byte bb, byte aa)
    {
        r = (float)(rr) / 255.0f;
        g = (float)(gg) / 255.0f;
        b = (float)(bb) / 255.0f;
        a = (float)(aa) / 255.0f;
    }


    /**
     * @brief R, G, B, A���� ����Ʈ �������� ��ȯ�մϴ�.
     * 
     * @param red[out] ����Ʈ �������� ��ȯ�� R ���Դϴ�.
     * @param green[out] ����Ʈ �������� ��ȯ�� G ���Դϴ�.
     * @param blue[out] ����Ʈ �������� ��ȯ�� B ���Դϴ�.
     * @param alpha[out] ����Ʈ �������� ��ȯ�� A ���Դϴ�.
     */
    public void ConvertToByte(out byte red, out byte green, out byte blue, out byte alpha)
    {
        red   = (byte)(Math.Min(Math.Max(0.0f, r), 1.0f) * 255.0f);
        green = (byte)(Math.Min(Math.Max(0.0f, g), 1.0f) * 255.0f);
        blue  = (byte)(Math.Min(Math.Max(0.0f, b), 1.0f) * 255.0f);
        alpha = (byte)(Math.Min(Math.Max(0.0f, a), 1.0f) * 255.0f);
    }


    /**
     * @brief �⺻������ ���ǵ� �����Դϴ�.
     */
    public static Color BLACK   = new Color(0.0f, 0.0f, 0.0f, 1.0f); // ������
    public static Color WHITE   = new Color(1.0f, 1.0f, 1.0f, 1.0f); // �Ͼ��
    public static Color RED     = new Color(1.0f, 0.0f, 0.0f, 1.0f); // ������
    public static Color GREEN   = new Color(0.0f, 1.0f, 0.0f, 1.0f); // �ʷϻ�
    public static Color BLUE    = new Color(0.0f, 0.0f, 1.0f, 1.0f); // �Ķ���
    public static Color YELLOW  = new Color(1.0f, 1.0f, 0.0f, 1.0f); // �����
    public static Color MAGENTA = new Color(1.0f, 0.0f, 1.0f, 1.0f); // ��ȫ��
    public static Color CYAN    = new Color(0.0f, 1.0f, 1.0f, 1.0f); // û�ϻ�


    /**
     * @brief RGBA ���� �� R���Դϴ�.
     */
    public float r;


    /**
     * @brief RGBA ���� �� G���Դϴ�.
     */
    public float g;


    /**
     * @brief RGBA ���� �� B���Դϴ�.
     */
    public float b;


    /**
     * @brief RGBA ���� �� A���Դϴ�.
     */
    public float a;
}