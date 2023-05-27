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
};