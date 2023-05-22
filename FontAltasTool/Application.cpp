// @third party code - BEGIN
#include <stb_rect_pack.h>
#include <stb_truetype.h>
// @third party code - END

#include <iostream>
#include <vector>

#include "FileHelper.hpp"
#include "Glyph.h"
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
	INISection section;
	section.AddData("A", "1");
	section.AddData("B", "2");
	section.AddData("C", "3");
	section.AddData("D", "4");

	auto sectionData0 = section.GetSectionData();
	for (auto data : sectionData0)
	{
		std::cout << data.first << ", " << data.second << std::endl;
	}

	auto data1 = section.GetData("A");
	std::cout << data1.first << ", " << data1.second << std::endl;

	std::string data2 = section.GetValue("B");
	std::cout << data2 << std::endl;

	section.RemoveData("C");
	auto sectionData1 = section.GetSectionData();
	for (auto data : sectionData1)
	{
		std::cout << data.first << ", " << data.second << std::endl;
	}

	return 0;
}