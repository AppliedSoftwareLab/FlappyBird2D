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
 * @param path ���� ���ҽ��� ����Դϴ�.
 *
 * @return ������ ���� ���ҽ��� ���̵� ��ȯ�մϴ�. ������ �����ϸ� -1�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) int32_t CreateSound(const char* path);


/**
 * @brief ������ ũ�⸦ �����մϴ�.
 *
 * @param soundID ũ�⸦ ������ ������ ���̵��Դϴ�.
 * @param volume ������ ũ���Դϴ�. ������ 0.0 ~ 1.0 �Դϴ�.
 */
extern "C" __declspec(dllexport) void SetSoundVolume(int32_t soundID, float volume);


/**
 * @brief ���� ������ ũ�⸦ ����ϴ�.
 *
 * @param soundID �Ҹ� ũ�⸦ ���� ������ ���̵��Դϴ�.
 *
 * @return ���� ������ ũ�⸦ ����ϴ�.
 */
extern "C" __declspec(dllexport) float GetSoundVolume(int32_t soundID);


/**
 * @brief ������ �ݺ� ���θ� �����մϴ�.
 *
 * @param soundID �ݺ� ���θ� ������ ������ ���̵��Դϴ�.
 * @param bIsLoop ���� �ݺ� �����Դϴ�.
 */
extern "C" __declspec(dllexport) void SetSoundLooping(int32_t soundID, bool bIsLoop);


/**
 * @brief ������ �ݺ� ���θ� ����ϴ�.
 *
 * @param soundID �ݺ� ���θ� Ȯ���� ������ ���̵��Դϴ�.
 *
 * @return ���尡 �ݺ��Ѵٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) bool GetSoundLooping(int32_t soundID);


/**
 * @brief ���带 �÷����մϴ�.
 *
 * @note ������ ������ ���� �ִٸ� ������ �������� �÷��̵˴ϴ�.
 *
 * @param soundID �÷����� ������ ���̵��Դϴ�.
 */
extern "C" __declspec(dllexport) void PlaySound(int32_t soundID);


/**
 * @brief ���尡 �÷��������� Ȯ���մϴ�.
 *
 * @param soundID �÷��� ������ Ȯ���� ���� ���̵��Դϴ�.
 *
 * @return ���尡 �÷��� ���̶�� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) bool IsPlayingSound(int32_t soundID);


/**
 * @brief ���� �÷��̰� �������� Ȯ���մϴ�.
 *
 * @param �÷��̰� �������� Ȯ���� ���� ���̵��Դϴ�.
 *
 * @return ���� �÷��̰� �����ٸ� true, �׷��� �ʴٸ� false�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) bool IsDoneSound(int32_t soundID);


/**
 * @brief ���� �÷��̸� �����մϴ�.
 *
 * @param soundID �÷��̸� ������ ���� ���̵��Դϴ�.
 */
extern "C" __declspec(dllexport) void StopSound(int32_t soundID);


/**
 * @brief ���� �÷��̰� ���� �Ǿ����� Ȯ���մϴ�.
 *
 * @param soundID �÷��̰� ���� �Ǿ����� Ȯ���� ���� ���̵��Դϴ�.
 *
 * @return ���� �÷��̰� ���� �Ǿ��ٸ� true, �׷��� ������ false�� ��ȯ�մϴ�.
 */
extern "C" __declspec(dllexport) bool IsStoppingSound(int32_t soundID);


/**
 * @brief ���带 �ʱ�ȭ�մϴ�.
 *
 * @param �ʱ�ȭ�� ������ ���̵��Դϴ�.
 */
extern "C" __declspec(dllexport) void ResetSound(int32_t soundID);