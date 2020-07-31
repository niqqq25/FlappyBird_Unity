using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager {

    #region Fields

    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();
    static bool isInitialized = false;

    #endregion

    #region Methods

    public static void Initialize(AudioSource audio)
    {
        audioSource = audio;
        audioClips.Add(AudioClipName.BirdDie, (AudioClip)Resources.Load("Audio/die"));
        audioClips.Add(AudioClipName.BirdHit, (AudioClip)Resources.Load("Audio/hit"));
        audioClips.Add(AudioClipName.BirdSwoosh, (AudioClip)Resources.Load("Audio/swoosh"));
        audioClips.Add(AudioClipName.ScoreIncrease, (AudioClip)Resources.Load("Audio/point"));
        isInitialized = true;
    }

    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    #endregion

    #region Properties

    public static bool IsInitialized
    {
        get { return isInitialized; }
    }

    #endregion
}
