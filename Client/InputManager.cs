using System;
using SDL2;


/**
 * @brief �Է� ó���� �����ϴ� �̱��� Ŭ�����Դϴ�.
 */
class InputManager
{
    /**
     * @brief �Է� ó���� �����ϴ� �Ŵ����� �ν��Ͻ��� ����ϴ�.
     * 
     * @return �Է� ó�� �Ŵ����� �ν��Ͻ��� ��ȯ�մϴ�. 
     */
    public static InputManager Get()
    {
        if(inputManager_ == null)
        {
            inputManager_ = new InputManager();
        }

        return inputManager_;
    }


    /**
     * @brief �����ڴ� �ܺο��� ȣ���� �� ������ ����ϴ�.
     */
    private InputManager() { }


    /**
     * @brief �Է� ó���� �����ϴ� �Ŵ����� �ν��Ͻ��Դϴ�.
     */
    private static InputManager inputManager_;
}