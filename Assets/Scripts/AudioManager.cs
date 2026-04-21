using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    AudioSource bgmPlayer;
    AudioSource sePlayer;

    void Awake()
    {
        Instance = this;
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmPlayer.playOnAwake = false;
        sePlayer = gameObject.AddComponent<AudioSource>();
        sePlayer.playOnAwake = false;
    }

    public void PlayBgm(string path)
    {
        if (bgmPlayer.isPlaying == false)
        {
            AudioClip clip = Resources.Load<AudioClip>(path);
            bgmPlayer.clip = clip;
            bgmPlayer.Play();
        }
    }

    public void StopBgm()
    {
        if (bgmPlayer.isPlaying == true)
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySe(string path)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        sePlayer.PlayOneShot(clip);
    }

}
