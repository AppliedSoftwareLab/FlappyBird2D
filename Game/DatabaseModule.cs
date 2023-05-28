using System.Runtime.InteropServices;


/**
 * @brief �����ͺ��̽� ��� ���̺귯���� C# ���ε��Դϴ�.
 */
class DatabaseModule
{
	/**
	 * @brief �����ͺ��̽� �ڵ��� �����մϴ�.
	 * 
	 * @param path DB ������ ����Դϴ�.
	 * 
	 * @return �����ͺ��̽� �ڵ��� ���̵� ���� ��ȯ�մϴ�. ������ �����ϸ� -1�� ��ȯ�մϴ�.
	 */
	[DllImport("DatabaseModule.dll", EntryPoint = "CreateDatabaseHandle", CallingConvention = CallingConvention.Cdecl)]
	public static extern int CreateDatabaseHandle([MarshalAs(UnmanagedType.LPStr)] string path);


	/**
	 * @brief �����ͺ��̽� ��� ���� �����ͺ��̽� �ڵ��� �����մϴ�.
	 */
	[DllImport("DatabaseModule.dll", EntryPoint = "Cleanup", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Cleanup();
}