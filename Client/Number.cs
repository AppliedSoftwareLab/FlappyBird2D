using System.Collections.Generic;


/**
 * @brief ������ ���� ������Ʈ�Դϴ�.
 */
class Number : GameObject
{
    /**
     * @brief ���� ���� ������Ʈ�� �Ӽ��� ���� Getter/Setter�Դϴ�.
     */
    public Vector2<float> Center
    {
        get => center_;
        set => center_ = value;
    }

    public int NumberContext
    {
        get => number_;
        set => number_ = value;
    }

    public float NumberWidth
    {
        get => numberWidth_;
        set => numberWidth_ = value;
    }

    public float NumberHeight
    {
        get => numberHeight_;
        set => numberHeight_ = value;
    }

    public float NumberGapLength
    {
        get => numberGapLength_;
        set => numberGapLength_ = value;
    }


    /**
     * @brief ������ ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        List<int> digits = CreateDigitArray(number_);
        float totalWidth = (numberWidth_ + numberGapLength_) * (float)(digits.Count - 1);

        Vector2<float> digitCenter;
        digitCenter.x = center_.x - totalWidth / 2.0f;
        digitCenter.y = center_.y;

        foreach(int digit in digits)
        {
            Texture numberTexture = ContentManager.Get().GetTexture(numberTextureSignatures_[digit]);
            RenderManager.Get().DrawTexture(ref numberTexture, digitCenter, numberWidth_, numberHeight_);

            digitCenter.x += (numberWidth_ + numberGapLength_);
        }
    }


    /**
     * @brief ������ ���� �ڸ����� �ִ� �ڿ����� ����Ʈ�� ����ϴ�.
     * 
     * @param number �ڸ����� ����� �������Դϴ�.
     * 
     * @return ������ �ڸ��� ����Ʈ�� ��ȯ�մϴ�.
     */
    private List<int> CreateDigitArray(int number)
    {
        List<int> digits = new List<int>();

        if(number == 0)
        {
            digits.Add(0);
            return digits;
        }

        while(number > 0)
        {
            digits.Add(number % 10);
            number /= 10;
        }

        digits.Reverse();
        return digits;
    }


    /**
     * @brief ȭ�鿡 ǥ���� �����Դϴ�.
     */
    private int number_ = 0;


    /**
     * @brief ���� ������ �߽� ��ǥ�Դϴ�.
     */
    private Vector2<float> center_;


    /**
     * @brief ���� �ϳ��� ���� ũ���Դϴ�.
     */
    private float numberWidth_ = 0.0f;


    /**
     * @brief ���� �ϳ��� ���� ũ���Դϴ�.
     */
    private float numberHeight_ = 0.0f;


    /**
     * @brief ǥ�õ� ���ڵ��� �����Դϴ�.
     */
    private float numberGapLength_ = 0.0f;


    /**
     * @brief ���ڵ��� �ؽ�ó �ñ״�ó�Դϴ�.
     */
    private List<string> numberTextureSignatures_ = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
}