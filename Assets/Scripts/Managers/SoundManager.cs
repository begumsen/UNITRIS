using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{

    public bool musicEnabled = true;

    public bool fxEnabled = true;

    [Range(0, 1)]
    public float musicVolume = 1.0f;

    [Range(0, 1)]
    public float fxVolume = 1.0f;

    public AudioClip moveSound;

    public AudioClip fallSound;

    public AudioClip gameOverSound;

    public AudioClip errorSound;

    public AudioSource musicSource;

    // background music clips
    public AudioClip[] musicClips;

    AudioClip randomMusicClip;

    public AudioClip[] vocalClips;

    public AudioClip gameOverVocalClip;



    // Use this for initialization
    void Start()
    {
        randomMusicClip = GetRandomClip(musicClips);
        PlayBackgroundMusic(randomMusicClip);

        // shorter way for playing a random music clip
        //PlayBackgroundMusic (GetRandomClip(m_musicClips));

    }

    // returns a random sound from an array of AudioClips
    public AudioClip GetRandomClip(AudioClip[] clips)
    {
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        return randomClip;
    }


    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        // return if music is disabled or if musicSource is null or is musicClip is null
        if (!musicEnabled || !musicClip || !musicSource)
        {
            return;
        }

        // if music is playing, stop it
        musicSource.Stop();

        musicSource.clip = musicClip;

        // set the music volume
        musicSource.volume = musicVolume;

        // music repeats forever
        musicSource.loop = true;

        // start playing
        musicSource.Play();
    }

    // updates whether we are playing or stopping the music depending on our musicEnabled toggle
    void UpdateMusic()
    {
        if (musicSource.isPlaying != musicEnabled)
        {
            if (musicEnabled)
            {
                randomMusicClip = GetRandomClip(musicClips);
                PlayBackgroundMusic(randomMusicClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }

    void Update()
    {

    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        UpdateMusic();
    }

    public void ToggleFX()
    {
        fxEnabled = !fxEnabled;

    }






}
