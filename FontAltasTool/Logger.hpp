#pragma once

#include <cstdint>
#include <iostream>
#include <windows.h>

#include "Macro.h"


/**
 * @brief �α� ó���� ���� ���� ����� �����մϴ�.
 * 
 * @note �� Ŭ������ ��� �Լ� ��ΰ� ������ ����(static) Ŭ�����Դϴ�.
 */
class Logger
{
public:
	/**
	 * @brief �α��� �����Դϴ�.
	 * 
	 * @note
	 * �Ϲ��� ��� ������� ǥ�õ˴ϴ�.
	 * ����� ��� ��������� ǥ�õ˴ϴ�.
	 * ������ ��� ���������� ǥ�õ˴ϴ�.
	 */
	enum class ELevel
	{
		NORMAL  = 0x00,
		WARNING = 0x01,
		ERR     = 0x02,
		SUCCESS = 0x03,
	};


public:
	/**
	 * @brief �α׸� ȭ�鿡 ����մϴ�.
	 * 
	 * @param level ȭ�鿡 ����� �α��� �����Դϴ�.
	 * @param message ȭ�鿡 ����� �α� �޽����Դϴ�.
	 */
	static inline void Display(const ELevel& level, const std::string& message)
	{
		std::string logMessage;

		switch (level)
		{
		case ELevel::NORMAL:
			logMessage = "[NORMAL]";
			SetConsoleColor(normalMessageColor_);
			break;

		case ELevel::WARNING:
			logMessage = "[WARNING]";
			SetConsoleColor(warningMessageColor_);
			break;

		case ELevel::ERR:
			logMessage = "[ERR]";
			SetConsoleColor(errorMessageColor_);
			break;

		case ELevel::SUCCESS:
			logMessage = "[SUCCESS]";
			SetConsoleColor(successMessageColor_);
			break;

		default:
			ENFORCE_THROW_EXCEPTION("undedefined logging level...");
		}

		std::cout << logMessage << message << '\n';
		SetConsoleColor(EConsoleColor::WHITE);
	}


	/**
	 * @brief �α׸� ȭ�鿡 ����մϴ�.
	 *
	 * @param level ȭ�鿡 ����� �α��� �����Դϴ�.
	 * @param message ȭ�鿡 ����� �α� �޽����Դϴ�.
	 */
	static inline void Display(const ELevel& level, const std::wstring& message)
	{
		std::wstring logMessage;

		switch (level)
		{
		case ELevel::NORMAL:
			logMessage = L"[NORMAL]";
			SetConsoleColor(normalMessageColor_);
			break;

		case ELevel::WARNING:
			logMessage = L"[WARNING]";
			SetConsoleColor(warningMessageColor_);
			break;

		case ELevel::ERR:
			logMessage = L"[ERR]";
			SetConsoleColor(errorMessageColor_);
			break;

		case ELevel::SUCCESS:
			logMessage = L"[SUCCESS]";
			SetConsoleColor(successMessageColor_);
			break;

		default:
			ENFORCE_THROW_EXCEPTION("undedefined logging level...");
		}

		std::wcout << logMessage << message << '\n';
		SetConsoleColor(EConsoleColor::WHITE);
	}


private:
	/**
	 * @brief �ܼ��� �ؽ�Ʈ �÷��Դϴ�.
	 */
	enum class EConsoleColor : int32_t
	{
		BLACK       = 0,
		BLUE        = 1,
		GREEN       = 2,
		AQUA        = 3,
		RED         = 4,
		PURPLE      = 5,
		YELLOW      = 6,
		WHITE       = 7,
		GRAY        = 8,
		LIGHTBLUE   = 9,
		LIGHTGREEN  = 10,
		LIGHTAQUA   = 11,
		LIGHTRED    = 12,
		LIGHTPURPLE = 13,
		LIGHTYELLOW = 14,
		BRIGHTWHITE = 15
	};


private:
	/**
	 * @brief �ܼ� â�� �ؽ�Ʈ ������ �����մϴ�.
	 *
	 * @param consoleColor ������ �����Դϴ�.
	 *
	 * @throws ������ �����ϸ� C++ ǥ�� ���ܸ� �����ϴ�.
	 */
	static inline void SetConsoleColor(const EConsoleColor& consoleColor)
	{
		CHECK(SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), static_cast<WORD>(consoleColor)), "failed to set text color...");
	}


private:
	/**
	 * @brief �Ϲ� �α� �޽����� �ؽ�Ʈ �����Դϴ�.
	 */
	static const EConsoleColor normalMessageColor_ = EConsoleColor::WHITE;


	/**
	 * @brief ��� �޽����� �ؽ�Ʈ �����Դϴ�.
	 */
	static const EConsoleColor warningMessageColor_ = EConsoleColor::YELLOW;


	/**
	 * @brief ���� �޽����� �ؽ�Ʈ �����Դϴ�.
	 */
	static const EConsoleColor errorMessageColor_ = EConsoleColor::RED;


	/**
	 * @brief ���� �޽����� �ؽ�Ʈ �����Դϴ�.
	 */
	static const EConsoleColor successMessageColor_ = EConsoleColor::BLUE;
};