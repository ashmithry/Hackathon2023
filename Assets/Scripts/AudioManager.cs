using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;


    void Awake()
    {

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name, float volume = 1)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s.source.isPlaying) return;

        s.source.volume = volume;
        s.source.PlayOneShot(s.clip, s.volume);
    }
}