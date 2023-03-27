/**
 * @brief 2���� �����Դϴ�.
 */
struct Vector2<T>
{
    /**
     * @brief 2���� ������ �������Դϴ�.
     * 
     * @param xx �ʱ�ȭ �� 2���� ������ ù ��° �����Դϴ�.
     * @param yy �ʱ�ȭ �� 2���� ������ �� ��° �����Դϴ�.
     */
    public Vector2(T xx, T yy)
    {
        X = xx;
        Y = yy;
    }


    /**
     * @brief 2���� ������ X�����Դϴ�.
     */
    public T X;


    /**
     * @brief 2���� ������ Y�����Դϴ�.
     */
    public T Y;
}


/**
 * @brief 3���� �����Դϴ�.
 */
struct Vector3<T>
{
    /**
     * @brief 3���� ������ �������Դϴ�.
     * 
     * @param xx �ʱ�ȭ �� 3���� ������ ù ��° �����Դϴ�.
     * @param yy �ʱ�ȭ �� 3���� ������ �� ��° �����Դϴ�.
     * @param zz �ʱ�ȭ �� 3���� ������ �� ��° �����Դϴ�.
     */
    public Vector3(T xx, T yy, T zz)
    {
        X = xx;
        Y = yy;
        Z = zz;
    }


    /**
     * @brief 3���� ������ X�����Դϴ�.
     */
    public T X;


    /**
     * @brief 3���� ������ Y�����Դϴ�.
     */
    public T Y;


    /**
     * @brief 3���� ������ Z�����Դϴ�.
     */
    public T Z;
}


/**
 * @brief 4���� �����Դϴ�.
 */
struct Vector4<T>
{
    /**
     * @brief 4���� ������ �������Դϴ�.
     * 
     * @param xx �ʱ�ȭ �� 4���� ������ ù ��° �����Դϴ�.
     * @param yy �ʱ�ȭ �� 4���� ������ �� ��° �����Դϴ�.
     * @param zz �ʱ�ȭ �� 4���� ������ �� ��° �����Դϴ�.
     * @param ww �ʱ�ȭ �� 4���� ������ �� ��° �����Դϴ�.
     */
    public Vector4(T xx, T yy, T zz, T ww)
    {
        X = xx;
        Y = yy;
        Z = zz;
        W = ww;
    }


    /**
     * @brief 4���� ������ X�����Դϴ�.
     */
    public T X;


    /**
     * @brief 4���� ������ Y�����Դϴ�.
     */
    public T Y;


    /**
     * @brief 4���� ������ Z�����Դϴ�.
     */
    public T Z;


    /**
     * @brief 4���� ������ W�����Դϴ�.
     */
    public T W;
}