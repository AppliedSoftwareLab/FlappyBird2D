#pragma once

#include <shlwapi.h>

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


	/**
	 * @brief ���۸� ���Ͽ� ���ϴ�.
	 * 
	 * @note ���� ��δ� UTF-8 ���ڿ��Դϴ�.
	 * 
	 * @param path ������ ����Դϴ�.
	 * @param buffer ���Ͽ� �� �����Դϴ�.
	 * 
	 * @throws ���� ���⿡ �����ϸ� C++ ǥ�� ���ܸ� �����ϴ�.
	 * 
	 * @see https://learn.microsoft.com/ko-kr/windows/win32/api/fileapi/nf-fileapi-createfilea
	 */
	static inline void WriteBufferFromFile(const std::string& path, const std::vector<uint8_t>& buffer)
	{
		HANDLE fileHandle = CreateFileA(path.c_str(), GENERIC_WRITE, FILE_SHARE_READ, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
		CHECK((fileHandle != INVALID_HANDLE_VALUE), "failed to create file...");

		DWORD bytesWrite;

		CHECK(WriteFile(fileHandle, &buffer[0], static_cast<DWORD>(buffer.size()), &bytesWrite, nullptr), "failed to write file...");
		CHECK(CloseHandle(fileHandle), "failed to close file...");
	}


	/**
	 * @brief ���۸� ���Ͽ� ���ϴ�.
	 *
	 * @note ���� ��δ� UTF-16 ���ڿ��Դϴ�.
	 *
	 * @param path ������ ����Դϴ�.
	 * @param buffer ���Ͽ� �� �����Դϴ�.
	 *
	 * @throws ���� ���⿡ �����ϸ� C++ ǥ�� ���ܸ� �����ϴ�.
	 * 
	 * @see https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew
	 */
	static inline void WriteBufferFromFile(const std::wstring& path, const std::vector<uint8_t>& buffer)
	{
		HANDLE fileHandle = CreateFileW(path.c_str(), GENERIC_WRITE, FILE_SHARE_READ, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
		CHECK((fileHandle != INVALID_HANDLE_VALUE), "failed to create file...");

		DWORD bytesWrite;

		CHECK(WriteFile(fileHandle, &buffer[0], static_cast<DWORD>(buffer.size()), &bytesWrite, nullptr), "failed to write file...");
		CHECK(CloseHandle(fileHandle), "failed to close file...");
	}


	/**
	 * @brief ��ΰ� ���丮���� �˻��մϴ�.
	 * 
	 * @note ���丮 ��δ� UTF-8 ���ڿ��Դϴ�.
	 * 
	 * @param directoryPath ���丮���� �˻��� ����Դϴ�.
	 * 
	 * @return ��ΰ� ���丮��� true, �׷��� ������ false�� ��ȯ�մϴ�.
	 */
	static inline bool IsValidDirectory(const std::string& directoryPath)
	{
		return PathIsDirectoryA(directoryPath.c_str());
	}


	/**
	 * @brief ��ΰ� ���丮���� �˻��մϴ�.
	 *
	 * @note ���丮 ��δ� UTF-16 ���ڿ��Դϴ�.
	 *
	 * @param directoryPath ���丮���� �˻��� ����Դϴ�.
	 *
	 * @return ��ΰ� ���丮��� true, �׷��� ������ false�� ��ȯ�մϴ�.
	 */
	static inline bool IsValidDirectory(const std::wstring& directoryPath)
	{
		return PathIsDirectoryW(directoryPath.c_str());
	}


	/**
	 * @brief ������ ��ȿ���� �˻��մϴ�.
	 * 
	 * @note ���丮 ��δ� UTF-8 ���ڿ��Դϴ�.
	 * 
	 * @param filePath ��ȿ���� �˻��� ���� ����Դϴ�.
	 * 
	 * @return ������ ��ȿ�ϴٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
	 */
	static inline bool IsValidFile(const std::string& filePath)
	{
		return PathFileExistsA(filePath.c_str());
	}


	/**
	 * @brief ������ ��ȿ���� �˻��մϴ�.
	 *
	 * @note ���丮 ��δ� UTF-16 ���ڿ��Դϴ�.
	 *
	 * @param filePath ��ȿ���� �˻��� ���� ����Դϴ�.
	 *
	 * @return ������ ��ȿ�ϴٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
	 */
	static inline bool IsValidFile(const std::wstring& filePath)
	{
		return PathFileExistsW(filePath.c_str());
	}
};