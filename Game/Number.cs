using System.Collections.Generic;


/**
 * @brief 게임의 숫자 오브젝트입니다.
 */
class Number : GameObject
{
    /**
     * @brief 게임 숫자 오브젝트의 속성에 대한 Getter/Setter입니다.
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
     * @brief 게임의 숫자 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
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
     * @brief 정수의 각각 자리수에 있는 자연수를 리스트로 만듭니다.
     * 
     * @param number 자리수를 계산할 정수값입니다.
     * 
     * @return 정수의 자리수 리스트를 반환합니다.
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
     * @brief 화면에 표시할 숫자입니다.
     */
    private int number_ = 0;


    /**
     * @brief 게임 숫자의 중심 좌표입니다.
     */
    private Vector2<float> center_;


    /**
     * @brief 숫자 하나의 가로 크기입니다.
     */
    private float numberWidth_ = 0.0f;


    /**
     * @brief 숫자 하나의 세로 크기입니다.
     */
    private float numberHeight_ = 0.0f;


    /**
     * @brief 표시될 숫자들의 간격입니다.
     */
    private float numberGapLength_ = 0.0f;


    /**
     * @brief 숫자들의 텍스처 시그니처입니다.
     */
    private List<string> numberTextureSignatures_ = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
}