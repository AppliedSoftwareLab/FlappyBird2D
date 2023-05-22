#pragma once

#include "Macro.h"


/**
 * @brief ���� ó���� ���� ���� ����� �����մϴ�.
 * 
 * @note �� Ŭ������ ��� �Լ� ��ΰ� ������ ����(static) Ŭ�����Դϴ�.
 */
class FileHelper
{
public:
	/**
	 * @brief ������ �а� ���ۿ� �����մϴ�.
	 *
	 * @note ���� ����� ���ڿ��� UTF-8 �Դϴ�.
	 *
	 * @see https://learn.microsoft.com/ko-kr/windows/win32/api/fileapi/nf-fileapi-createfilea
	 *
	 * @param path ������ ����Դϴ�.
	 * @param outBuffer[out] ���� ������ ������ �����Դϴ�.
	 *
	 * @throws
	 * ���� ������ �����ϸ� C++ ǥ�� ���ܸ� �����ϴ�.
	 * ���� �б⿡ �����ϸ� C++ ǥ�� ���ܸ� �����ϴ�.
	 */
	static inline void ReadBufferFromFile(const std::string& path, std::vector<uint8_t>& outBuffer)
	{
		HANDLE fileHandle = CreateFileA(path.c_str(), GENERIC_READ, FILE_SHARE_READ, nullptr, OPEN_EXISTING, 0, nullptr);
		CHECK((fileHandle != INVALID_HANDLE_VALUE), "failed to create file");

		DWORD fileSize = GetFileSize(fileHandle, nullptr);

		outBuffer.resize(fileSize);
		DWORD bytesRead;

		CHECK(ReadFile(fileHandle, &outBuffer[0], fileSize, &bytesRead, nullptr), "failed read file...");
		CHECK(CloseHandle(fileHandle), "failed to close file...");
	}


	/**
	 * @brief ������ �а� ���ۿ� �����մϴ�.
	 *
	 * @note ���� ����� ���ڿ��� UTF-16 �Դϴ�.
	 *
	 * @param path ������ ����Դϴ�.
	 * @param outBuffer[out] ���� ������ ������ �����Դϴ�.
	 *
	 * @throws ���� �б⿡ �����ϸ� C++ ǥ�� ���ܸ� �����ϴ�.
	 */
	static inline void ReadBufferFromFile(const std::wstring& path, std::vector<uint8_t>& outBuffer)
	{
		HANDLE fileHandle = CreateFileW(path.c_str(), GENERIC_READ, FILE_SHARE_READ, nullptr, OPEN_EXISTING, 0, nullptr);
		CHECK((fileHandle != INVALID_HANDLE_VALUE), "failed to create file...");

		DWORD fileSize = GetFileSize(fileHandle, nullptr);

		outBuffer.resize(fileSize);
		DWORD bytesRead;

		CHECK(ReadFile(fileHandle, &outBuffer[0], fileSize, &bytesRead, nullptr), "failed read file...");
		CHECK(CloseHandle(fileHandle), "failed to close file...");
	}
};