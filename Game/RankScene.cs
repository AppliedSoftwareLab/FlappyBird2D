using System.Collections.Generic;


/**
 * @brief �÷��̾��� ��ŷ�� �����ִ� ���Դϴ�.
 */
class RankScene : Scene
{
    /**
     * @brief �÷��̾��� ��ŷ ���� �����մϴ�.
     */
    public override void Entry()
    {
        base.Entry();

        gameObjectSignatures_ = new List<string>();
    }


    /**
     * @brief �÷����� ��ŷ �����κ��� �����ϴ�.
     */
    public override void Leave()
    {
        base.Leave();
    }
}