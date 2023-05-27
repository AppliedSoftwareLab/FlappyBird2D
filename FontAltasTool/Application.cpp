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
	return true;
}


/**
 * @brief ��Ʈ ��Ʋ�󽺸� �����մϴ�.
 * 
 * @note 
 * �ڵ� ����Ʈ�� ������ ���۰� ���� �����մϴ�.
 * ��Ʈ ��Ʋ�� ��Ʈ���� ���� ���� ũ��� �����մϴ�. 
 * 
 * @param fontPath Ʈ�� Ÿ�� ��Ʈ ���ҽ��� ����Դϴ�.
 * @param beginCodePoint �ڵ� ����Ʈ�� �����Դϴ�.
 * @param endCodePoint �ڵ� ����Ʈ�� ���Դϴ�.
 * @param fontSize ��Ʈ ��Ʋ�� �� ������ ũ���Դϴ�.
 * @param outGlyphs[out] ��Ʈ ��Ʋ�� �� ���ڵ��� ��ġ �� ũ���Դϴ�.
 * @param outAtlasBitmapBuffer[out] ��Ʈ ��Ʋ���� ��Ʈ�� �����Դϴ�.
 * @param outAtlasBitmapSize[out] ��Ʈ ��Ʋ�� ��Ʈ���� ũ���Դϴ�.
 * 
 * @return ��Ʈ ��Ʋ�� ������ �����ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�. 
 */
bool GenerateFontAtlas(
	const std::string& fontPath,
	int32_t beginCodePoint,
	int32_t endCodePoint,
	float fontSize,
	std::vector<Glyph>& outGlyphs,
	std::vector<uint8_t>& outAtlasBitmapBuffer,
	int32_t& outAltasBitmapSize
)
{
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