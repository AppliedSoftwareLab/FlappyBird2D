// @third party code - BEGIN
#include <stb_rect_pack.h>
#include <stb_truetype.h>
#include <stb_image_write.h>
// @third party code - END

#include <array>
#include <iostream>
#include <sstream>
#include <vector>

#include "CommandLine.h"
#include "FileHelper.hpp"
#include "Glyph.h"
#include "INIFormat.h"
#include "INISection.h"
#include "Logger.hpp"
#include "StringHelper.hpp"

/**
 * @brief ��Ʈ ��Ʋ�� ������ ���� ����� �μ��� ��� ���޵Ǿ����� �˻��մϴ�.
 * 
 * @return ��Ʈ ��Ʋ�� ������ ���� ����� �μ��� ��� ���޵Ǿ��ٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
 */
bool IsPassArgumentForFontAtlas()
{
	std::array<std::string, 5> arguments = {
		"FontPath",
		"BeginCodePoint",
		"EndCodePoint",
		"FontSize",
		"OutputPath",
	};

	for (const auto& argument : arguments)
	{
		if (CommandLine::IsValid(argument))
		{
			Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("confirming the pass of parameter %s... => VALID!", argument.c_str()));
		}
		else
		{
			Logger::Display(Logger::ELevel::ERR, StringHelper::Format("confirming the pass of parameter %s... => INVALID!", argument.c_str()));
			return false;
		}
	}

	return true;
}

/**
 * @brief ��Ʈ ��Ʋ�� ������ ���� ����� �μ��� ��ȿ���� �˻��մϴ�.
 * 
 * @param outFontPath[out] Ʈ�� Ÿ�� ��Ʈ�� ����Դϴ�.
 * @param outBeginCodePoint[out] �ڵ� ����Ʈ�� �������Դϴ�.
 * @param outEndCodePoint[out] �ڵ� ����Ʈ�� �����Դϴ�.
 * @param outFontSize[out] ��Ʈ�� ũ���Դϴ�.
 * @param outOutputPath[out] ��Ʈ ��Ʋ���� ��� ����Դϴ�.
 * 
 * @return ��Ʈ ��Ʋ�� ������ ���� ����� �μ��� ��ȿ�ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
 */
bool IsValidArgumentForFontAtlas(
	std::string& outFontPath,
	int32_t& outBeginCodePoint,
	int32_t& outEndCodePoint,
	float& outFontSize,
	std::string& outOutputPath
)
{
	std::string fontPath = CommandLine::GetValue("FontPath");
	if (fontPath.find(".ttf") == std::string::npos)
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("%s is not true type font file...", fontPath.c_str()));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("%s is true type font file...", fontPath.c_str()));
		outFontPath = fontPath;
	}

	if (!FileHelper::IsValidFile(fontPath))
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("%s is invalid type font file...", fontPath.c_str()));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("%s is valid true type font file...", fontPath.c_str()));
	}

	int32_t beginCodePoint = 0;
	std::stringstream beginCodePointStream(CommandLine::GetValue("BeginCodePoint"));
	beginCodePointStream >> beginCodePoint;
	if (beginCodePointStream.fail() || beginCodePoint < 0)
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("invalid begin code point => %d...", beginCodePoint));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("valid begin code point => %d...", beginCodePoint));
		outBeginCodePoint = beginCodePoint;
	}

	int32_t endCodePoint = 0;
	std::stringstream endCodePointStream(CommandLine::GetValue("EndCodePoint"));
	endCodePointStream >> endCodePoint;
	if (endCodePointStream.fail() || endCodePoint < 0)
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("invalid end code point => %d...", endCodePoint));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("valid end code point => %d...", endCodePoint));
		outEndCodePoint = endCodePoint;
	}

	if (beginCodePoint > endCodePoint)
	{
		Logger::Display(Logger::ELevel::WARNING, StringHelper::Format("BeginCodePoint is bigger than EndCodePoint..."));
		Logger::Display(Logger::ELevel::WARNING, StringHelper::Format("swap BeginCodePoint and EndCodePoint..."));
		outBeginCodePoint = endCodePoint;
		outEndCodePoint = beginCodePoint;
	}
	
	float fontSize = 0.0f;
	std::stringstream fontSizeStream(CommandLine::GetValue("FontSize"));
	fontSizeStream >> fontSize;
	if (fontSizeStream.fail() || fontSize < 0.0f)
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("invalid font size => %f...", fontSize));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("valid font size => %f...", fontSize));
		outFontSize = fontSize;
	}

	std::string outputPath = CommandLine::GetValue("OutputPath");
	if (!FileHelper::IsValidDirectory(outputPath))
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("invalid output path => %s...", outputPath.c_str()));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("valid output path => %s...", outputPath.c_str()));
		outOutputPath = outputPath;
	}
	
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
	std::vector<uint8_t> buffer;
	FileHelper::ReadBufferFromFile(fontPath, buffer);

	stbtt_fontinfo fontInfo;
	const unsigned char* bufferPtr = reinterpret_cast<const unsigned char*>(&buffer[0]);
	int32_t offsetIndex = stbtt_GetFontOffsetForIndex(reinterpret_cast<const unsigned char*>(&buffer[0]), 0);

	if (!stbtt_InitFont(&fontInfo, bufferPtr, offsetIndex))
	{
		Logger::Display(Logger::ELevel::ERR, "failed to initialize stb_truetype...");
		return false;
	}

	std::size_t codePointRange = static_cast<std::size_t>(endCodePoint - beginCodePoint + 1);

	outGlyphs.clear();
	outGlyphs.resize(codePointRange);

	std::vector<stbtt_packedchar> packedchars(codePointRange);

	int32_t success = 0;
	stbtt_pack_context packContext;

	for (int32_t size = 16; size < 8192; size *= 2)
	{
		outAtlasBitmapBuffer.clear();
		outAtlasBitmapBuffer.resize(size * size);

		success = stbtt_PackBegin(&packContext, &outAtlasBitmapBuffer[0], size, size, 0, 1, nullptr);
		stbtt_PackSetOversampling(&packContext, 1, 1);

		success = stbtt_PackFontRange(
			&packContext,
			reinterpret_cast<const unsigned char*>(&buffer[0]),
			0,
			fontSize,
			beginCodePoint,
			static_cast<int>(packedchars.size()),
			&packedchars[0]
		);

		if (success)
		{
			stbtt_PackEnd(&packContext);
			outAltasBitmapSize = size;
			break;
		}
		else
		{
			stbtt_PackEnd(&packContext);
		}
	}

	if (!success)
	{
		Logger::Display(Logger::ELevel::ERR, "failed to generate font bitmap...");
		return false;
	}

	for (std::size_t index = 0; index < packedchars.size(); ++index)
	{
		outGlyphs[index].codePoint = static_cast<int32_t>(index + beginCodePoint);

		outGlyphs[index].x0 = packedchars[index].x0;
		outGlyphs[index].y0 = packedchars[index].y0;
		outGlyphs[index].x1 = packedchars[index].x1;
		outGlyphs[index].y1 = packedchars[index].y1;

		outGlyphs[index].xoffset = packedchars[index].xoff;
		outGlyphs[index].yoffset = packedchars[index].yoff;

		outGlyphs[index].xoffset2 = packedchars[index].xoff2;
		outGlyphs[index].yoffset2 = packedchars[index].yoff2;

		outGlyphs[index].xadvance = packedchars[index].xadvance;
	}

	Logger::Display(Logger::ELevel::NORMAL, "successed generate font atlas...");
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

	if (!IsPassArgumentForFontAtlas())
	{
		Logger::Display(Logger::ELevel::ERR, "font atlas argument is missing...");
		return -1;
	}

	std::string fontPath;
	int32_t beginCodePoint;
	int32_t endCodePoint;
	float fontSize;
	std::string outputPath;

	if (!IsValidArgumentForFontAtlas(fontPath, beginCodePoint, endCodePoint, fontSize, outputPath))
	{
		Logger::Display(Logger::ELevel::ERR, "font atlas argument is invalid...");
		return -1;
	}

	std::vector<Glyph> glyphs;
	std::vector<uint8_t> atlasBitmapBuffer;
	int32_t altasBitmapSize;

	if (!GenerateFontAtlas(fontPath, beginCodePoint, endCodePoint, fontSize, glyphs, atlasBitmapBuffer, altasBitmapSize))
	{
		Logger::Display(Logger::ELevel::ERR, "failed to generate font atlas...");
	}

	return 0;
}