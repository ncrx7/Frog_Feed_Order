using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] private List<SoundClip> _soundClips;

    private void OnEnable()
    {
        EventSystem.PlaySoundClip += HandlePlayingClip;
    }

    private void OnDisable()
    {
        EventSystem.PlaySoundClip -= HandlePlayingClip;
    }

    //WARNING: CAN BE SEPERATE THIS FUNCTION AS A SOUND CLIP, IF WANTED TO AVOID FROM O(N^2) COST
    private void HandlePlayingClip(SoundType soundType)
    {
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is null");
            return;
        }

        foreach (SoundClip soundObject in _soundClips) //CREATE DICT FOR OPTIMIZATION
        {
            if (soundObject.clip == null)
            {
                Debug.LogError("AudioClip is null for sound type: " + soundObject.soundType);
                continue;
            }

            if (soundType == soundObject.soundType)
            {
                _audioSource.PlayOneShot(soundObject.clip);
                break;
            }
        }
    }

}

public enum SoundType
{
    HIT_CORRECT_GRAPE,
    HIT_CORRECT_ARROW,
    COLLECT_HITTABLE_OBJECT,
    WIN_ROUND,
    LOSE_ROUND
}

