#pragma once

#include <map>


/**
 * @brief INI�� ���� �����Դϴ�.
 */
class INISection
{
public:
	/**
	 * @brief INI ���� ������ ����Ʈ �������Դϴ�.
	 */
	INISection() = default;


	/**
	 * @brief INI ���� ������ ���� �������Դϴ�.
	 * 
	 * @param instance ������ INI ���� ������ �ν��Ͻ��Դϴ�.
	 */
	INISection(INISection&& instance) noexcept
		: sectionData_(instance.sectionData_) {}


	/**
	 * @brief INI ���� ������ ���� �������Դϴ�.
	 *
	 * @param instance ������ INI ���� ������ �ν��Ͻ��Դϴ�.
	 */
	INISection(const INISection& instance)
		: sectionData_(instance.sectionData_) {}


	/**
	 * @brief INI ���� ������ ���� �Ҹ����Դϴ�.
	 */
	virtual ~INISection() {}


	/**
	 * @brief INI ���� ������ ���� �������Դϴ�.
	 * 
	 * @param instance ������ INI ���� ������ �ν��Ͻ��Դϴ�.
	 * 
	 * @return ������ INI ���� ������ ��ȯ�մϴ�.
	 */
	INISection& operator=(INISection&& instance) noexcept
	{
		if (this == &instance) return *this;

		sectionData_ = instance.sectionData_;

		return *this;
	}


	/**
	 * @brief INI ���� ������ ���� �������Դϴ�.
	 *
	 * @param instance ������ INI ���� ������ �ν��Ͻ��Դϴ�.
	 *
	 * @return ������ INI ���� ������ ��ȯ�մϴ�.
	 */
	INISection& operator=(const INISection& instance)
	{
		if (this == &instance) return *this;

		sectionData_ = instance.sectionData_;

		return *this;
	}
	

	/**
	 * @brief ���ǿ� Ű-�� �����͸� �߰��մϴ�.
	 * 
	 * @param key �߰��� Ű ���Դϴ�.
	 * @param value �߰��� Ű ���� �����ϴ� �������Դϴ�.
	 * 
	 * @throws Ű ���� �̹� ���� ���� �����ϸ� C++ ǥ�� ���ܸ� �����ϴ�.
	 */
	void AddData(const std::string& key, const std::string& value);

	
	/**
	 * @brief ���ǿ� Ű-�� �����͸� ������ �߰��մϴ�.
	 * 
	 * @note �̹� Ű ���� ���� ���� �����ϸ� ���� ���ϴ�.
	 * 
	 * @param key �߰��� Ű ���Դϴ�.
	 * @param value �߰��� Ű ���� �����ϴ� �������Դϴ�.
	 */
	void AddEnforceData(const std::string& key, const std::string& value);


	/**
	 * @brief ���� �� �����͸� ����ϴ�.
	 * 
	 * @param key ���� ���� Ű ���Դϴ�.
	 * 
	 * @throws Ű ���� �����ϴ� ���� ������ C++ ǥ�� ���ܸ� �����ϴ�.
	 * 
	 * @return ���� �� Ű-�� ���� ��ȯ�մϴ�.
	 */
	const std::pair<std::string, std::string>& GetData(const std::string& key) const;


	/**
	 * @brief ���ǿ� Ű�� �� Ű�� �����ϴ� ���� �����մϴ�.
	 * 
	 * @note Ű ���� ��ȿ���� ������ �ƹ� ���۵� �������� �ʽ��ϴ�.
	 * 
	 * @param key ���� ���� Ű ���Դϴ�.
	 */
	void RemoveData(const std::string& key);


	/**
	 * @brief Ű ���� �����ϴ� ���� ����ϴ�.
	 * 
	 * @param key ���� ���� Ű ���Դϴ�.
	 * 
	 * @throws Ű ���� �����ϴ� ���� �������� ������ C++ ǥ�� ���ܸ� �����ϴ�.
	 * 
	 * @return Ű ���� �����ϴ� ���� ��ȯ�մϴ�.
	 */
	const std::string& GetValue(const std::string& key) const;

	
	/**
	 * @brief ������ Ű-�� �����͸� ����ϴ�.
	 * 
	 * @return ������ Ű-�� �����͸� ��ȯ�մϴ�.
	 */
	const std::map<std::string, std::string>& GetSectionData() const { return sectionData_; }


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
	 * @brief INI ���� �� Ű-�� ���� �������Դϴ�.
	 */
	std::map<std::string, std::string> sectionData_;
};