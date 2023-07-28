using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {

    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<SoundName, AudioClip> audioClips =
        new Dictionary<SoundName, AudioClip>();

    
    public static bool Initialized
    {
        get { return initialized; }
    }

    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(SoundName.MoveSound,
            Resources.Load<AudioClip>("MoveSound"));
        audioClips.Add(SoundName.GameOverSound,
            Resources.Load<AudioClip>("GameOverSound"));
        audioClips.Add(SoundName.CorrectlyPlacedSound,
             Resources.Load<AudioClip>("CorrectlyPlacedSound"));
        audioClips.Add(SoundName.MisplacedSound,
             Resources.Load<AudioClip>("MisplacedSound"));
        audioClips.Add(SoundName.MenuSelectButton,
              Resources.Load<AudioClip>("MenuSelectButton"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(SoundName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
