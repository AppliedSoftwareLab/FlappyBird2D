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

	auto sectionData = section.GetSectionData();
	for (auto data : sectionData)
	{
		std::cout << data.first << ", " << data.second << std::endl;
	}

	return 0;
}