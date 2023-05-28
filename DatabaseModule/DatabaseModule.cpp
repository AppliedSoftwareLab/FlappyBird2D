// @third party code - BEGIN
#include <sqlite3.h>
// @third party code - END

#include <vector>
#include <unordered_map>

#include "DatabaseModule.h"

namespace DatabaseModule
{
	/**
	 * @brief ������ �����ͺ��̽� �ڵ� ���Դϴ�.
	 */
	static int32_t countOfDBHandle = 0;

	/**
	 * @brief �����ͺ��̽� ����� �����ϴ� �����ͺ��̽� �ڵ��Դϴ�.
	 */
	static std::vector<sqlite3*> databaseHandles;
	
	int32_t CreateDatabaseHandle(const char* path)
	{
		sqlite3* databaseHandle = nullptr;
		int32_t result = sqlite3_open(path, &databaseHandle);
		
		if (result == SQLITE_OK)
		{
			databaseHandles.push_back(databaseHandle);
			return countOfDBHandle++;
		}
		else
		{
			sqlite3_close(databaseHandle);
			return -1;
		}
	}

	void Cleanup()
	{
		for (sqlite3* databaseHandle : databaseHandles)
		{
			sqlite3_close(databaseHandle);
			databaseHandle = nullptr;
		}
	}
}