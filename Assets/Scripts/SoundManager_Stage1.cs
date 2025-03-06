using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            instance = (SoundManager)FindAnyObjectByType(typeof(SoundManager));

            if (instance == null)
            {
                GameObject obj = new GameObject("SoundManager", typeof(SoundManager));
                instance = obj.AddComponent<SoundManager>();
            }

            return instance;
        }
    }
    public AudioSource bgmPlayer;
    public AudioSource sfxPlayer;
    
    public AudioClip bgmClip;
    public AudioClip jumpClip;
    public AudioClip coinClip;
    public AudioClip hurtClip;
    public AudioClip diedClip;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    private void Start()
    {
            PlayBGM();
    }
    public void PlayBGM()
    {
        bgmPlayer.clip = bgmClip;
        bgmPlayer.Play();
    }

    public void PlaySFX(string sfxName)
    {
        if(sfxName == "Jump")
        {
            sfxPlayer.PlayOneShot(jumpClip);
        }
        if(sfxName == "Coin")
        {
            sfxPlayer.PlayOneShot(coinClip);
        }
        if(sfxName == "Hurt")
        {
            sfxPlayer.PlayOneShot(hurtClip);
        }
        if(sfxName == "Died")
        {
            sfxPlayer.PlayOneShot(diedClip);
        }
    }


}
