#pragma once

#include <cstdint>
#include <windows.h>


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
};