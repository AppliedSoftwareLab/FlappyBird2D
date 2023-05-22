#pragma once

#include <cstdint>

/**
 * @brief �ؽ�ó ��Ʋ�󽺿� ǥ�õ� ���� �����Դϴ�.
 */
struct Glyph
{
	int32_t codePoint;
	int32_t x0;
	int32_t y0;
	int32_t x1;
	int32_t y1;
	float xoffset;
	float yoffset;
	float xoffset2;
	float yoffset2;
	float xadvance;
};