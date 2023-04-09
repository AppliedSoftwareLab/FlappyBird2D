using System;


/**
 * @brief ���� ���� ��ư UI ������Ʈ�Դϴ�.
 */
class Button : GameObject
{
    /**
     * @brief ��ư UI �Ӽ��� ���� Getter/Setter �Դϴ�.
     */
    public RigidBody UIBody
    {
        get => uiRigidBody_;
    }

    public string UITexture
    {
        get => uiTextureSignature_;
        set => uiTextureSignature_ = value;
    }

    public Action EventAction
    {
        get => eventAction_;
        set => eventAction_ = value;
    }

    public float ReduceRatio
    {
        get => reduceRatio_;
        set => reduceRatio_ = value;
    }


    /**
     * @brief ��ư UI�� �ٵ� �����մϴ�.
     * 
     * @param center ȭ�� ���� UI �߽� ��ǥ�Դϴ�.
     * @param width UI �ٵ��� ���� ũ���Դϴ�.
     * @param height UI �ٵ��� ���� ũ���Դϴ�.
     */
    public void CreateUIBody(Vector2<float> center, float width, float height)
    {
        uiRigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief ���� ���� ��ư UI ������Ʈ�� ������Ʈ�մϴ�.
     */
    public override void Tick(float deltaSeconds)
    {
        EPressState mousePressState = InputManager.Get().GetMousePressState(EMouseButton.LEFT);

        bIsMouseOverButton_ = IsMouseOverButton();
        bIsPressButton_ = (bIsMouseOverButton_ && (mousePressState == EPressState.PRESSED || mousePressState == EPressState.HELD));

        if(bIsMouseOverButton_ && mousePressState == EPressState.RELEASED)
        {
            eventAction_();
        }

        float textureWidth = uiRigidBody_.Width;
        float textureHeight = uiRigidBody_.Height;

        textureWidth *= bIsPressButton_ ? reduceRatio_ : 1.0f;
        textureHeight *= bIsPressButton_ ? reduceRatio_ : 1.0f;

        Texture uiButtonTexture = ContentManager.Get().GetTexture(uiTextureSignature_);

        RenderManager.Get().DrawTexture(ref uiButtonTexture, uiRigidBody_.Center, textureWidth, textureHeight);
    }


    /**
     * @brief ���콺 ��ġ�� UI ��ư ���� �ִ��� Ȯ���մϴ�.
     * 
     * @return ���콺 ��ġ�� UI ��ư ���� ������ true, �׷��� ������ false�� ��ȯ�մϴ�.
     */
    private bool IsMouseOverButton()
    {
        Vector2<float> mousePosition;
        mousePosition.x = (float)(InputManager.Get().CurrCursorPosition.x);
        mousePosition.y = (float)(InputManager.Get().CurrCursorPosition.y);

        uiRigidBody_.GetBoundPosition(out Vector2<float> minPosition, out Vector2<float> maxPosition);

        return (minPosition.x <= mousePosition.x && mousePosition.x <= maxPosition.x)
            && (minPosition.y <= mousePosition.y && mousePosition.y <= maxPosition.y);
    }


    /**
     * @brief ���콺 ��ġ�� UI ��ư ���� �ִ��� Ȯ���մϴ�.
     */
    private bool bIsMouseOverButton_ = false;


    /**
     * @brief ���콺�� UI ��ư�� Ŭ���ߴ��� Ȯ���մϴ�.
     */
    private bool bIsPressButton_ = false;


    /**
     * @brief UI ��ư �ؽ�ó �ñ״�ó �Դϴ�.
     */
    private string uiTextureSignature_;


    /**
     * @brief UI ��ư�� �ٵ��Դϴ�.
     */
    private RigidBody uiRigidBody_;


    /**
     * @brief UI ��ư Ŭ�� �� ������ �׼��Դϴ�.
     */
    Action eventAction_ = () => { };


    /**
     * @brief UI ��ư Ŭ�� �� �پ�� �ؽ�ó�� �����Դϴ�.
     */
    float reduceRatio_ = 1.0f;
}