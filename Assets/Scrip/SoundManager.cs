using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : GlobalReference<SoundManager>
{
    public AudioSource MusicSource;
    public AudioSource SoundSource;

    public List<GetAudioTag> MusicClip;
    public List<GetAudioTag> SoundClip;

    private void Start()
    {
        PlayMusic("Main");
    }
    public void PlayMusic(string tag)
    {
        foreach (var music in MusicClip)
        {
            MusicSource.clip = music.clip;
            MusicSource.Play();
        }
    }
    public void PlaySound(string tag)
    {
        foreach (var sound in SoundClip)
        {
            if (sound.tag == tag)
            {
                SoundSource.PlayOneShot(sound.clip);
            }
        }
    }
}
[Serializable]
public class GetAudioTag
{
    public string tag;
    public AudioClip clip;
}