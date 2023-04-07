using System.Collections.Generic;


/**
 * @brief ������ ���ھ� ���� ������Ʈ�Դϴ�.
 */
class ScoreBoard : GameObject
{
    /**
     * @brief ���� ���ھ� ���� ������Ʈ�� �Ӽ��� ���� Getter/Setter�Դϴ�.
     */
    public Vector2<float> Center
    {
        get => center_;
        set => center_ = value;
    }

    public float BoardNumberWidth
    {
        get => boardNumberWidth_;
        set => boardNumberWidth_ = value;
    }

    public float BoardNumberHeight
    {
        get => boardNumberHeight_;
        set => boardNumberHeight_ = value;
    }

    public float NumberGapLength
    {
        get => numberGapLength_;
        set => numberGapLength_ = value;
    }


    /**
     * @brief ������ ���ھ� ���� ������Ʈ�� ������Ʈ�մϴ�.
     * 
     * @param deltaSeconds �ʴ��� ��Ÿ �ð����Դϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        Bird bird = WorldManager.Get().GetGameObject("Bird") as Bird;
        score_ = bird.PassPipe;

        List<int> digits = CreateDigitArray(score_);
        float totalWidth = (boardNumberWidth_ + numberGapLength_) * (float)(digits.Count - 1);

        Vector2<float> digitCenter;
        digitCenter.x = center_.x - totalWidth / 2.0f;
        digitCenter.y = center_.y;

        foreach(int digit in digits)
        {
            Texture numberTexture = ContentManager.Get().GetTexture(numberTextureSignatures_[digit]);
            RenderManager.Get().DrawTexture(ref numberTexture, digitCenter, boardNumberWidth_, boardNumberHeight_);

            digitCenter.x += (boardNumberWidth_ + numberGapLength_);
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
     * @brief ���� ���� ���ھ��Դϴ�.
     * 
     * @note �� ���ھ�� �� ������Ʈ�� ����� ������ ���Դϴ�.
     */
    private int score_ = 0;


    /**
     * @brief ���� ���ھ� ������ �߽� ��ǥ�Դϴ�.
     */
    private Vector2<float> center_;


    /**
     * @brief ���� �� ���� �ϳ��� ���� ũ���Դϴ�.
     */
    private float boardNumberWidth_ = 0.0f;


    /**
     * @brief ���� �� ���� �ϳ��� ���� ũ���Դϴ�.
     */
    private float boardNumberHeight_ = 0.0f;


    /**
     * @brief ���� �� ǥ�õ� ���ڵ��� �����Դϴ�.
     */
    private float numberGapLength_ = 0.0f;


    /**
     * @brief ���ڵ��� �ؽ�ó �ñ״�ó�Դϴ�.
     */
    private List<string> numberTextureSignatures_ = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
}