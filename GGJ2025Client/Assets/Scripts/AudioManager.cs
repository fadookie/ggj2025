using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    // public string[] VolumeParameters => volumeParameters;
    // public AudioMixer AudioMixer => audioMixer;

    [SerializeField] private AudioClip BubblePop;
    
    // [SerializeField] private AudioClip[] Music;

    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioSource musicPlayer;

    // [SerializeField] private AudioMixer audioMixer;

    // [SerializeField] private string[] volumeParameters;

    // private Queue<AudioClip> MusicQueue;
    // public bool PlayMusic { get; set; }

    // private IEnumerator musicControlRoutine; 

    public enum Sound
    {
        BubblePop,
    }

    void Awake()
    {
        // Singleton setup;
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;

        // ShuffleMusicQueue();
        // PlayMusic = true;

        /*
        foreach (string volumeParameter in volumeParameters)
        {
            float volume; ;
            if (PlayerPrefs.HasKey(volumeParameter))
            {
                volume = PlayerPrefs.GetFloat(volumeParameter);
                audioMixer.SetFloat(volumeParameter, volume);
            }
            else
            {
                audioMixer.GetFloat(volumeParameter, out volume);
                PlayerPrefs.SetFloat(volumeParameter, volume);
            }
        }
        */
    }

    /*
    private void Update() {
        if (PlayMusic && !musicPlayer.isPlaying) {
            try {
                var newClip = MusicQueue.Dequeue();
                PlayMusicClip(newClip);
            } catch (InvalidOperationException) {
                ShuffleMusicQueue();
            }
        }
        if(musicControlRoutine != null)
        {
            if(!musicControlRoutine.MoveNext())
            {
                musicControlRoutine = null;
            }
        }
    }
    */



    // void ShuffleMusicQueue() {
    //     var musicListShuffled = Music.ToList();
    //     musicListShuffled.Shuffle();
    //     MusicQueue = new Queue<AudioClip>(musicListShuffled);
    // }

    public void PlaySound(Sound sound)
    {
        PlayClip(GetAudioClip(sound));
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        return sound switch
        {
            Sound.BubblePop => BubblePop,
            _ => throw new ArgumentOutOfRangeException(nameof(sound), sound, null)
        };
    }

    private void PlayClip(AudioClip clip)
    {
        Debug.LogWarning($"AudioManager#PlayClip:{clip}");
        sfxPlayer.PlayOneShot(clip);
    }
    
    private void PlayMusicClip(AudioClip clip)
    {
        musicPlayer.PlayOneShot(clip);
    }
}