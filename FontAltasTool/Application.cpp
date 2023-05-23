// @third party code - BEGIN
#include <stb_rect_pack.h>
#include <stb_truetype.h>
#include <stb_image_write.h>
// @third party code - END

#include <array>
#include <iostream>
#include <vector>

#include "CommandLine.h"
#include "FileHelper.hpp"
#include "StringHelper.hpp"
#include "Glyph.h"
#include "INIFormat.h"
#include "INISection.h"


/**
 * @brief ��Ʈ ��Ʋ�� ������ ���� ����� �μ��� ��ȿ���� �˻��մϴ�.
 * 
 * @return ��Ʈ ��Ʋ�� ������ ���� ����� �μ��� ��ȿ�ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
 */
bool IsValidArgumentForFontAtlas()
{
	std::array<std::string, 6> arguments = {
		"Crash",
		"FontPath",
		"BeginCodePoint",
		"EndCodePoint",
		"FontSize",
		"OutputPath",
	};

	for (const auto& argument : arguments)
	{
		std::cout << "check " << argument << " argument...\n";

		if (!CommandLine::IsValid(argument))
		{
			std::cout << "invaild [" << argument << "] argument for font atlas...\n";
			return false;
		}

		std::cout << "=>" << argument << " : " << CommandLine::GetValue(argument) << "\n";
	}

	return true;
}


/**
 * @brief ���ø����̼��� �������Դϴ�.
 *
 * @param argc ����� ������ ���Դϴ�.
 * @param argv ����� �����Դϴ�.
 *
 * @return �ٸ� ���α׷��� ������ �� �ִ� ���� �ڵ带 ��ȯ�մϴ�.
 */
int32_t main(int32_t argc, char** argv)
{
	CommandLine::Parse(argc, argv);

	if (!IsValidArgumentForFontAtlas())
	{
		std::cout << "failed to generate font altas...\n";
		return -1;
	}
	
	return 0;
}