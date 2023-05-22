// @third party code - BEGIN
#include <stb_rect_pack.h>
#include <stb_truetype.h>
// @third party code - END

#include <iostream>
#include <vector>

#include "FileHelper.hpp"
#include "Glyph.h"
#include "INIFormat.h"
#include "INISection.h"


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
	INIFormat iniFormat;

	INISection section0;
	section0.AddData("A", "1");
	section0.AddData("B", "2");
	section0.AddData("C", "3");
	section0.AddData("D", "4");
	iniFormat.AddSection("x", section0);
	
	INISection section1;
	section1.AddData("E", "1");
	section1.AddData("F", "2");
	section1.AddData("G", "3");
	section1.AddData("H", "4");
	iniFormat.AddSection("y", section1);

	auto sections = iniFormat.GetSections();

	for (const auto& section : sections)
	{
		auto sectionData = section.second.GetSectionData();
		for (const auto data : sectionData)
		{
			std::cout << data.first << ", " << data.second << std::endl;
		}

		std::cout << "\n\n";
	}

	return 0;
}