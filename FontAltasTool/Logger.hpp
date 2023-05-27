#pragma once

#include <cstdint>
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
		NOR  = 0x00,
		WARN = 0x01,
		ERR  = 0x02,
	};


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
};