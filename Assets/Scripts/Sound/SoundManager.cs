using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {

    static bool initialized = false;
    static bool musicEnabled = true;
    static bool fxEnabled = true;
    static AudioSource audioSource;
    static Dictionary<SoundName, AudioClip> audioClips =
        new Dictionary<SoundName, AudioClip>();
    public static ToggleIcon musicIconToggle;
    public static ToggleIcon fxIconToggle;

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
        audioClips.Add(SoundName.MenuSelectSound,
              Resources.Load<AudioClip>("MenuSelectSound"));
        audioClips.Add(SoundName.BackgroundMusic,
              Resources.Load<AudioClip>("BackgroundMusic1"));
        EventManager.AddListener(EventName.MusicToggleEvent, ToggleMusic);
        EventManager.AddListener(EventName.FxToggleEvent, ToggleFX);
        EventManager.AddListener(EventName.GameOver, HandleGameOver);
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void PlayFX(SoundName name)
    {
        if (fxEnabled == false) return;

        audioSource.PlayOneShot(audioClips[name], 0.6f);
    }

    public static void PlayBackgroundMusic()
    {
        if (!musicEnabled) return;
        // if music is playing, stop it
        audioSource.Stop();

        audioSource.clip = audioClips[SoundName.BackgroundMusic];

        // music repeats forever
        audioSource.loop = true;

        // start playing
        audioSource.Play();
    }

    public static void ToggleMusic(int a)
    {
        musicEnabled = !musicEnabled;
        if (!musicEnabled)
        {
            audioSource.Stop();
        }
        else
        {
            PlayBackgroundMusic();
        }

    }

    static void HandleGameOver(int victory)
    {
        audioSource.Stop();
        musicEnabled = false;
        PlayFX(SoundName.GameOverSound);
    }

    

    public static void ToggleFX(int b)
    {
       fxEnabled = !fxEnabled;

    }
}
