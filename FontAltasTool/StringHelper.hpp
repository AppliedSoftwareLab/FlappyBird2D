#pragma once

#include <string>
#include <vector>
#include <memory>
#include <stdexcept>
#include <windows.h>

#include "Macro.h"


/**
 * @brief ǥ�� ���ڿ��� ���� ���� ����� �����մϴ�.
 * 
 * @note �� Ŭ������ ��� �Լ� ��ΰ� ������ ����(static) Ŭ�����Դϴ�.
 */
class StringHelper
{
public:
	/**
	 * @brief ����ȭ�� ���ڿ��� ��ȯ�մϴ�. �� �޼���� snprintf�� ��ü�ϱ� ���� �޼����Դϴ�.
	 *
	 * @see https://stackoverflow.com/questions/2342162/stdstring-formatting-like-sprintf
	 *
	 * @param formatString UTF-8 ����� ���� ���ڿ��Դϴ�.
	 * @param ... argument �������� ������ �����͸� �����ϴ� �μ��Դϴ�.
	 *
	 * @return �������� �Ϸ�� ���ڿ��� ��ȯ�մϴ�.
	 */
	template<typename ... Args>
	static inline std::string Format(const std::string& formatString, Args ... argument)
	{
		size_t size = static_cast<size_t>(std::snprintf(nullptr, 0, formatString.c_str(), argument ...)) + 1;

		auto buffer = std::make_unique<char[]>(size);
		std::snprintf(buffer.get(), size, formatString.c_str(), argument ...);

		return std::string(buffer.get(), buffer.get() + size - 1);
	}


	/**
	 * @brief ����ȭ�� ���ڿ��� ��ȯ�մϴ�. �� �޼���� swprintf�� ��ü�ϱ� ���� �޼����Դϴ�.
	 *
	 * @see https://stackoverflow.com/questions/2342162/stdstring-formatting-like-sprintf
	 *
	 * @param formatString UTF-16 ����� ���� ���ڿ��Դϴ�.
	 * @param ... argument �������� ������ �����͸� �����ϴ� �μ��Դϴ�.
	 *
	 * @return �������� �Ϸ�� ���ڿ��� ��ȯ�մϴ�.
	 */
	template<typename ... Args>
	static inline std::wstring Format(const std::wstring& formatString, Args ... argument)
	{
		size_t size = static_cast<size_t>(std::swprintf(nullptr, 0, formatString.c_str(), argument ...)) + 1;

		auto buffer = std::make_unique<wchar_t[]>(size);
		std::swprintf(buffer.get(), size, formatString.c_str(), argument ...);

		return std::wstring(buffer.get(), buffer.get() + size - 1);
	}


	/**
	 * @brief ���ڿ��� Ư�� �������� ����� ���ͷ� ����ϴ�.
	 *
	 * @param textToSplit Ư�� �������� ����� ���ͷ� ���� ���ڿ��Դϴ�.
	 * @param splitDelimiter ���ڿ��� ���� �����Դϴ�.
	 *
	 * @return �������� ���ڿ��� ���͸� ��ȯ�մϴ�.
	 */
	static inline std::vector<std::string> Split(const std::string& textToSplit, const std::string& splitDelimiter)
	{
		std::string splitText = textToSplit;
		std::vector<std::string> tokens;
		std::size_t position = 0;

		while ((position = splitText.find(splitDelimiter)) != std::string::npos)
		{
			tokens.push_back(splitText.substr(0, position));
			splitText.erase(0, position + splitDelimiter.length());
		}

		tokens.push_back(splitText);
		return tokens;
	}


	/**
	 * @brief ���ڿ��� Ư�� �������� ����� ���ͷ� ����ϴ�.
	 *
	 * @param textToSplit Ư�� �������� ����� ���ͷ� ���� ���ڿ��Դϴ�.
	 * @param splitDelimiter ���ڿ��� ���� �����Դϴ�.
	 *
	 * @return �������� ���ڿ��� ���͸� ��ȯ�մϴ�.
	 */
	static inline std::vector<std::wstring> Split(const std::wstring& textToSplit, const std::wstring& splitDelimiter)
	{
		std::wstring splitText = textToSplit;
		std::vector<std::wstring> tokens;
		std::size_t position = 0;

		while ((position = splitText.find(splitDelimiter)) != std::wstring::npos)
		{
			tokens.push_back(splitText.substr(0, position));
			splitText.erase(0, position + splitDelimiter.length());
		}

		tokens.push_back(splitText);
		return tokens;
	}


	/**
	 * @brief UTF-8 ���ڿ��� UTF-16 ���ڿ��� ��ȯ�մϴ�.
	 *
	 * @see https://learn.microsoft.com/ko-kr/windows/win32/api/stringapiset/nf-stringapiset-multibytetowidechar
	 *
	 * @param utf8String UTF-8 ����� ���ڿ��Դϴ�.
	 *
	 * @return ��ȯ�� UTF-16 ���ڿ��� ��ȯ�մϴ�.
	 */
	static inline std::wstring Convert(const std::string& utf8String)
	{
		int32_t size = MultiByteToWideChar(CP_UTF8, 0, &utf8String[0], static_cast<int32_t>(utf8String.size()), nullptr, 0);
		CHECK((size != 0), "failed to convert UTF-8 to UTF-16");

		std::wstring utf16String(size, 0);
		CHECK((MultiByteToWideChar(CP_UTF8, 0, &utf8String[0], static_cast<int32_t>(utf8String.size()), &utf16String[0], size) != 0), "failed to convert UTF-8 to UTF-16");

		return utf16String;
	}


	/**
	 * @brief UTF-16 ���ڿ��� UTF-8 ���ڿ��� ��ȯ�մϴ�.
	 *
	 * @see https://learn.microsoft.com/en-us/windows/win32/api/stringapiset/nf-stringapiset-widechartomultibyte
	 *
	 * @param utf16String UTF-16 ����� ���ڿ��Դϴ�.
	 *
	 * @return ��ȯ�� UTF-8 ���ڿ��� ��ȯ�մϴ�.
	 */
	static inline std::string Convert(const std::wstring& utf16String)
	{
		int32_t size = WideCharToMultiByte(CP_ACP, 0, &utf16String[0], static_cast<int32_t>(utf16String.size()), nullptr, 0, nullptr, nullptr);
		CHECK((size != 0), "failed to convert UTF-16 to UTF-8");

		std::string utf8String(size, 0);
		CHECK((WideCharToMultiByte(CP_UTF8, 0, &utf16String[0], static_cast<int32_t>(utf16String.size()), &utf8String[0], size, nullptr, nullptr) != 0), "failed to convert UTF-16 to UTF-8");

		return utf8String;
	}


	/**
	 * @brief ���ڿ��� �����ϴ� �ؽ� ���� �����մϴ�.
	 *
	 * @param utf8String �ؽ� ���� ������ UTF-8 ���ڿ��Դϴ�.
	 *
	 * @return �Է��� ���ڿ��κ��� ������ �ؽ����� ��ȯ�մϴ�.
	 */
	static inline std::size_t GetUTF8Hash(const std::string& utf8String)
	{
		return std::hash<std::string>{}(utf8String);
	}


	/**
	 * @brief ���ڿ��� �����ϴ� �ؽ� ���� �����մϴ�.
	 *
	 * @param utf16String �ؽ� ���� ������ UTF-16 ���ڿ��Դϴ�.
	 *
	 * @return �Է��� ���ڿ��κ��� ������ �ؽ����� ��ȯ�մϴ�.
	 */
	static inline std::size_t GetUTF16Hash(const std::wstring& utf16String)
	{
		return std::hash<std::wstring>{}(utf16String);
	}
};