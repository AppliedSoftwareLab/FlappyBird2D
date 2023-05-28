#include <cstdint>


/**
 * @brief �����ͺ��̽� ����Դϴ�.
 */
namespace DatabaseModule
{
	/**
	 * @brief �����ͺ��̽� �ڵ��� �����մϴ�.
	 * 
	 * @param path DB ������ ����Դϴ�.
	 * 
	 * @return �����ͺ��̽� �ڵ��� ���̵� ���� ��ȯ�մϴ�. ������ �����ϸ� -1�� ��ȯ�մϴ�.
	 */
	extern "C" __declspec(dllexport) int32_t CreateDatabaseHandle(const char* path);


	/**
	 * @brief �����ͺ��̽� ��� ���� �����ͺ��̽� �ڵ��� �����մϴ�.
	 */
	extern "C" __declspec(dllexport) void Cleanup();



}