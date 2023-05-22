#pragma once

#include <string>
#include <unordered_map>


/**
 * @brief Ŀ�ǵ� ������ �Ľ��ϰ� ���� ���� ������ �����ϴ� Ŭ�����Դϴ�.
 *
 * @note �� Ŭ������ ��� ��� �Լ��� ������ ������ ���� Ŭ�����Դϴ�.
 */
class CommandLine
{
public:
	/**
	 * @brief ����� ���� ���� �Ľ��մϴ�.
	 *
	 * @param argc ����� ������ ���Դϴ�.
	 * @param argv ����� �����Դϴ�.
	 */
	static void Parse(int32_t argc, char* argv[]);


	/**
	 * @brief ���� ���� ��θ� ����ϴ�.
	 *
	 * @return ���� ���� ��θ� ��ȯ�մϴ�.
	 */
	static std::string GetExecutePath() { return executePath_; }


	/**
	 * @brief Ű ���� ��ȿ���� �˻��մϴ�.
	 *
	 * @param key ��ȿ���� �˻��� Ű ���Դϴ�.
	 *
	 * @return Ű ���� �����ϴ� ���� �ִٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
	 */
	static bool IsValid(const std::string& key);


	/**
	 * @brief Ű ���� �����ϴ� ���� ����ϴ�.
	 *
	 * @param key �������� �ϴ� ����  Ű ���Դϴ�.
	 *
	 * @return Ű ���� �����ϴ� ���� ��ȯ�մϴ�.
	 */
	static std::string GetValue(const std::string& key);


private:
	/**
	 * @brief ���� ���� ����Դϴ�.
	 */
	static std::string executePath_;


	/**
	 * @brief ����� ������ Ű-�� ���Դϴ�.
	 */
	static std::unordered_map<std::string, std::string> arguments_;
};