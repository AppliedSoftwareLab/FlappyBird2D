#include "AudioModule.h"

#include <algorithm>
#include <memory>
#include <vector>

// @third party code - BEGIN
#include <miniaudio.h>
// @third party code - END

namespace AudioModule
{
	/**
	 * @brief AudioManager�� �ʱ�ȭ�Ǿ����� Ȯ���մϴ�.
	 */
	static bool bIsSetup = false;

	/**
	 * @brief AudioManager���� ������ ���� ���ҽ� ���Դϴ�.
	 */
	static int32_t countSoundResource = 0;

	/**
	 * @brief miniaudio �����Դϴ�.
	 */
	static std::unique_ptr<ma_engine> miniAudioEngine = nullptr;

	/**
	 * @brief ���� ���ҽ� ������Ʈ�Դϴ�.
	 */
	static std::vector<std::unique_ptr<ma_sound>> sounds;


	bool Setup()
	{
		if (bIsSetup) return false;

		miniAudioEngine = std::make_unique<ma_engine>();
		if (ma_engine_init(nullptr, miniAudioEngine.get()) != MA_SUCCESS)
		{
			return false;
		}

		sounds.resize(0);
		bIsSetup = true;

		return true;
	}

	void Cleanup()
	{
		if (!bIsSetup) return;

		for (auto& sound : sounds)
		{
			ma_sound_uninit(sound.get());
			sound.reset();
		}

		ma_engine_uninit(miniAudioEngine.get());
		miniAudioEngine.reset();

		bIsSetup = false;
	}

	int32_t CreateSound(const char* path)
	{
		std::unique_ptr<ma_sound> sound = std::make_unique<ma_sound>();

		if (ma_sound_init_from_file(miniAudioEngine.get(), path, 0, nullptr, nullptr, sound.get()) != MA_SUCCESS)
		{
			return -1;
		}

		sounds.push_back(std::move(sound));
		return countSoundResource++;
	}

	void SetSoundVolume(int32_t soundID, float volume)
	{
		if (soundID == -1) return;

		volume = std::clamp<float>(volume, 0.0f, 1.0f);
		ma_sound_set_volume(sounds[soundID].get(), volume);
	}

	float GetSoundVolume(int32_t soundID)
	{
		if (soundID == -1) return 0.0f;

		return ma_sound_get_volume(sounds[soundID].get());
	}

	void SetSoundLooping(int32_t soundID, bool bIsLoop)
	{
		if (soundID == -1) return;

		ma_sound_set_looping(sounds[soundID].get(), static_cast<ma_bool32>(bIsLoop));
	}

	bool GetSoundLooping(int32_t soundID)
	{
		if (soundID == -1) return false;

		return ma_sound_is_looping(sounds[soundID].get()) == MA_TRUE;
	}

	void PlaySound(int32_t soundID)
	{
		if (soundID == -1) return;

		ma_sound_start(sounds[soundID].get());
	}

	bool IsPlayingSound(int32_t soundID)
	{
		if (soundID == -1) return false;

		return ma_sound_is_playing(sounds[soundID].get()) == MA_TRUE;
	}

	bool IsDoneSound(int32_t soundID)
	{
		if (soundID == -1) return false;

		return ma_sound_at_end(sounds[soundID].get()) == MA_TRUE;
	}

	void StopSound(int32_t soundID)
	{
		if (soundID == -1) return;

		ma_sound_stop(sounds[soundID].get());
	}

	bool IsStoppingSound(int32_t soundID)
	{
		if (soundID == -1) return false;

		return !IsDoneSound(soundID) && !IsPlayingSound(soundID);
	}

	void ResetSound(int32_t soundID)
	{
		if (soundID == -1) return;

		ma_sound_seek_to_pcm_frame(sounds[soundID].get(), 0);
	}
}