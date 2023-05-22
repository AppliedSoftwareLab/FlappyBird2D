#pragma once

#include "INISection.h"


/**
 * @brief INI ���� �����Դϴ�.
 */
class INIFormat
{
public:
	/**
	 * @brief INI ���� ������ ����Ʈ �������Դϴ�.
	 */
	INIFormat() = default;


	/**
	 * @brief INI ���� ������ ���� �������Դϴ�.
	 * 
	 * @param instance ������ INI ���� ������ �ν��Ͻ��Դϴ�.
	 */
	INIFormat(INIFormat&& instance) noexcept
		: iniSection_(instance.iniSection_) {}


	/**
	 * @brief INI ���� ������ ���� �������Դϴ�.
	 *
	 * @param instance ������ INI ���� ������ �ν��Ͻ��Դϴ�.
	 */
	INIFormat(const INIFormat& instance)
		: iniSection_(instance.iniSection_) {}


	/**
	 * @brief INI ���� ������ ���� �Ҹ����Դϴ�.
	 */
	virtual ~INIFormat() {}


	/**
	 * @brief INI ���� ������ ���� �������Դϴ�.
	 * 
	 * @param instance ������ INI ���� ������ �ν��Ͻ��Դϴ�.
	 * 
	 * @return ������ INI ���� ������ �����ڸ� ��ȯ�մϴ�.
	 */
	INIFormat& operator=(INIFormat&& instance) noexcept
	{
		if (this == &instance) return *this;

		iniSection_ = instance.iniSection_;

		return *this;
	}


	/**
	 * @brief INI ���� ������ ���� �������Դϴ�.
	 *
	 * @param instance ������ INI ���� ������ �ν��Ͻ��Դϴ�.
	 *
	 * @return ������ INI ���� ������ �����ڸ� ��ȯ�մϴ�.
	 */
	INIFormat& operator=(const INIFormat& instance) noexcept
	{
		if (this == &instance) return *this;

		iniSection_ = instance.iniSection_;

		return *this;
	}


	/**
	 * @brief INI ���� ���信 ������ �߰��մϴ�.
	 * 
	 * @param section �߰��� �����Դϴ�.
	 * 
	 * @throws Ű ���� �����ϴ� ������ �̹� �����ϸ� C++ ǥ�� ���ܸ� �����ϴ�.
	 */
	void AddSection(const std::string& key, const INISection& section);


	/**
	 * @brief INI ���� ������ ������ ����ϴ�.
	 * 
	 * @param key ������ Ű ���Դϴ�.
	 * 
	 * @throws Ű ���� �����ϴ� ������ �������� ������ C++ ǥ�� ���ܸ� �����ϴ�.
	 */
	INISection& GetSection(const std::string& key);


	/**
	 * @brief INI ���� ������ ������ ����ϴ�.
	 *
	 * @param key ������ Ű ���Դϴ�.
	 *
	 * @throws Ű ���� �����ϴ� ������ �������� ������ C++ ǥ�� ���ܸ� �����ϴ�.
	 */
	const INISection& GetSection(const std::string& key) const;


	/**
	 * @brief INI ���� ���� ���� ������ �����մϴ�.
	 * 
	 * @note Ű ���� ��ȿ���� ������ �ƹ� ���۵� �������� �ʽ��ϴ�.
	 * 
	 * @param key ������ ������ Ű ���Դϴ�.
	 */
	void RemoveSection(const std::string& key);


	/**
	 * @brief INI ���� ���� ���� ���ǵ��� ����ϴ�.
	 * 
	 * @return Ű-���� �����͸� ��ȯ�մϴ�.
	 */
	const std::map<std::string, INISection>& GetSections() const { return iniSection_; }


private:
	/**
	 * @brief Ű ���� �����ϴ� ���� �����ϴ��� Ȯ���մϴ�.
	 *
	 * @param key ���� �����ϴ��� Ȯ���� Ű ���Դϴ�.
	 *
	 * @return Ű ���� �����ϴ� ���� �����ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
	 */
	bool IsValidKey(const std::string& key) const;

	
private:
	/**
	 * @brief INI ���� �� �����Դϴ�.
	 */
	std::map<std::string, INISection> iniSection_;
};