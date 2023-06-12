using System.Data;
using System.Data.SQLite;


/**
 * @brief �����ͺ��̽��� �ε��ϰ� �����ϴ� Ŭ�����Դϴ�.
 */
class Database : IContent
{
    /**
     * @brief �����ͺ��̽��� �ε��ϰ� �����ϴ� Ŭ������ �������Դϴ�.
     * 
     * @param path �����ͺ��̽��� ����Դϴ�.
     * 
     * @throws �����ͺ��̽��� ��ȿ���� ������ ���ܸ� �����ϴ�.
     */
    public Database(string path)
    {
        string connectionString = string.Format("Data Source={0}", path);

        sqlConnection_ = new SQLiteConnection(connectionString);
        sqlConnection_.Open();
    }


    /**
     * @brief �����ͺ��̽� ���ҽ��� �����͸� ��������� �����մϴ�.
     */
    public void Release()
    {
        sqlConnection_.Close();
    }


    /**
     * @brief SQLite �����ͺ��̽��� ���� ������ ��Ÿ���ϴ�.
     * 
     * @see https://learn.microsoft.com/ko-kr/dotnet/api/microsoft.data.sqlite.sqliteconnection?view=msdata-sqlite-7.0.0
     */
    SQLiteConnection sqlConnection_;
}