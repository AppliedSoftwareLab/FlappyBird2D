#pragma once

#include <windows.h>
#include <dbghelp.h>
#include <vector>
#include <string>


/**
 * @brief ���α׷� ���� �� �߻��� ġ������ ������ ó���ϴ� Ŭ�����Դϴ�.
 * 
 * @note �� Ŭ������ ��� ��� ���� �� �Լ��� ������ ���� Ŭ�����Դϴ�.
 */
class CrashHandler
{
public:
	/**
	 * @brief ũ���� ���� ���� ��θ� �����մϴ�.
	 * 
	 * @param crashDumpFilePath ������ ũ���� ���� ���� ����Դϴ�.
	 */
	static void SetCrashDumpFilePath(const std::string& crashDumpFilePath)
	{
		crashDumpFilePath_ = crashDumpFilePath;
	}


	/**
	 * @brief ũ���� ������ ���� ������ �����մϴ�.
	 * 
	 * @param crashSourceFile ũ���� ������ �߻��� �ҽ� �����Դϴ�.
	 * @param crashErrorLine ũ���� ������ �߻��� �ҽ� ���� ���� ���� ��ȣ�Դϴ�.
	 * @param crashErrorMessage ũ���� ������ ���� �޽����Դϴ�.
	 */
	static void RecordCrashError(const std::string& crashSourceFile, const int32_t& crashErrorLine, const std::string& crashErrorMessage)
	{
		crashErrorSourceFileName_ = crashSourceFile;
		crashErrorLine_ = crashErrorLine;
		lastCrashErrorMessage_ = crashErrorMessage;
	}


	/**
	 * @brief ���ø����̼��� ũ���ð� �����ϰ�, ũ���� ������ �ݽ����� ����մϴ�.
	 * 
	 * @param exceptionPointer ���� ������ ���� ������ ���Դϴ�.
	 */
	static LONG DetectApplicationCrash(EXCEPTION_POINTERS* exceptionPointer);


private:
	/**
	 * @brief ũ���� ������ ������� ũ���� ���� ������ �����մϴ�.
	 *
	 * @param exceptionPointer ���� ������ ���� ������ ���Դϴ�.
	 */
	static void GenerateCrashDumpFile(EXCEPTION_POINTERS* exceptionPointer);


private:
	/**
	 * @brief ũ���� ���� ������ ����� ����Դϴ�.
	 */
	static std::string crashDumpFilePath_;


	/**
	 * @brief ���� �ֱٿ� ��ϵ� �浹 ���� �޽����Դϴ�.
	 */
	static std::string lastCrashErrorMessage_;


	/**
	 * @brief ũ���ð� �߻��� �ҽ� ������ �̸��Դϴ�.
	 */
	static std::string crashErrorSourceFileName_;


	/**
	 * @brief ũ���ð� �߻��� �ҽ� ���� ���� ���� ��ġ�Դϴ�.
	 */
	static int32_t crashErrorLine_;
};