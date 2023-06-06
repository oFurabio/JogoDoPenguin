using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds, LoopSounds;
    public AudioSource musicSource, sfxSource, loopSource;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        PlayMusic("Thema");
    }

    public void PlayMusic(string name) {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        
        if (s == null) {
            Debug.Log("Sound Not Found");
        } else {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name) {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null) {
            Debug.Log("Sound Not Found");
        } else {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayLoop(string name)
    {
        Sound s = Array.Find(LoopSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }


}
