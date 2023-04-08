#include <string>
#include <cstdint>


/**
 * @brief ����� ����� �ʱ�ȭ�մϴ�.
 * 
 * @return ����� ��� �ʱ�ȭ�� �����ϸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) bool Setup();


/**
 * @brief ����� ����� �ʱ�ȭ�� �����մϴ�.
 */
extern "C" __declspec(dllexport) void Cleanup();


/**
 * @brief ���� ���ҽ��� �����մϴ�.
 *
 * @param Path ���� ���ҽ��� ����Դϴ�.
 *
 * @return ������ ���� ���ҽ��� ���̵� ��ȯ�մϴ�. ������ �����ϸ� -1�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) int32_t CreateSound(const std::string& Path);


/**
 * @brief ������ ũ�⸦ �����մϴ�.
 *
 * @param SoundID ũ�⸦ ������ ������ ���̵��Դϴ�.
 * @param Volume ������ ũ���Դϴ�. ������ 0.0 ~ 1.0 �Դϴ�.
 */
extern "C" __declspec(dllexport) void SetSoundVolume(int32_t SoundID, float Volume);


/**
 * @brief ���� ������ ũ�⸦ ����ϴ�.
 *
 * @param SoundID �Ҹ� ũ�⸦ ���� ������ ���̵��Դϴ�.
 *
 * @return ���� ������ ũ�⸦ ����ϴ�.
 */
extern "C" __declspec(dllexport) float GetSoundVolume(int32_t SoundID);


/**
 * @brief ������ �ݺ� ���θ� �����մϴ�.
 *
 * @param SoundID �ݺ� ���θ� ������ ������ ���̵��Դϴ�.
 * @param bIsLoop ���� �ݺ� �����Դϴ�.
 */
extern "C" __declspec(dllexport) void SetSoundLooping(int32_t SoundID, bool bIsLoop);


/**
 * @brief ������ �ݺ� ���θ� ����ϴ�.
 *
 * @param SoundID �ݺ� ���θ� Ȯ���� ������ ���̵��Դϴ�.
 *
 * @return ���尡 �ݺ��Ѵٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) bool GetSoundLooping(int32_t SoundID);


/**
 * @brief ���带 �÷����մϴ�.
 *
 * @note ������ ������ ���� �ִٸ� ������ �������� �÷��̵˴ϴ�.
 *
 * @param SoundID �÷����� ������ ���̵��Դϴ�.
 */
extern "C" __declspec(dllexport) void PlaySound(int32_t SoundID);


/**
 * @brief ���尡 �÷��������� Ȯ���մϴ�.
 *
 * @param SoundID �÷��� ������ Ȯ���� ���� ���̵��Դϴ�.
 *
 * @return ���尡 �÷��� ���̶�� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) bool IsPlayingSound(int32_t SoundID);


/**
 * @brief ���� �÷��̰� �������� Ȯ���մϴ�.
 *
 * @param �÷��̰� �������� Ȯ���� ���� ���̵��Դϴ�.
 *
 * @return ���� �÷��̰� �����ٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) bool IsDoneSound(int32_t SoundID);


/**
 * @brief ���� �÷��̸� �����մϴ�.
 *
 * @param SoundID �÷��̸� ������ ���� ���̵��Դϴ�.
 */
extern "C" __declspec(dllexport) void StopSound(int32_t SoundID);


/**
 * @brief ���� �÷��̰� ���� �Ǿ����� Ȯ���մϴ�.
 *
 * @param SoundID �÷��̰� ���� �Ǿ����� Ȯ���� ���� ���̵��Դϴ�.
 *
 * @return ���� �÷��̰� ���� �Ǿ��ٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) bool IsStoppingSound(int32_t SoundID);


/**
 * @brief ���带 �ʱ�ȭ�մϴ�.
 *
 * @param �ʱ�ȭ�� ������ ���̵��Դϴ�.
 */
extern "C" __declspec(dllexport) void ResetSound(int32_t SoundID);