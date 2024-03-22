using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup _musicGroup;
    [SerializeField] private AudioMixerGroup _sfxGroup;

    public Sounds[] sounds;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);


        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (s.name.StartsWith("M_") == true)
                s.source.outputAudioMixerGroup = _musicGroup;
            else if (s.name.StartsWith("SFX_") == true)
                s.source.outputAudioMixerGroup = _sfxGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("M_MainMenu");
    }

    /// <summary>
    /// Play a sound 
    /// </summary>
    /// <param name="name">ID name of the sound that will be played</param>
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Stop();
    }

}
