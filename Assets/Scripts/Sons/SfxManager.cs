using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class SfxManager : MonoBehaviour
{
    public Sfx[] efeitos;

    private void Awake()
    {
        foreach (Sfx e in efeitos)
        {
            e.source = gameObject.AddComponent<AudioSource>();
            e.source.clip = e.clip;
            e.source.outputAudioMixerGroup = e.audioMixer;

            e.source.spatialBlend = e.spatialBlend;
            e.source.loop = e.loop;
            e.source.Stop();

        }
    }

    public void Play(string name)
    {
        Sfx e = Array.Find(efeitos, sound => sound.name == name);

        if (e == null)
        {
            Debug.LogWarning("Efeito sonoro: " + name + " n�o encontrado!");
            return;
        }

        e.source.volume = e.volume;
        e.source.pitch = e.pitch;

        e.source.Play();
    }
    public void StopPlaying(string name)
    {
        Sfx e = Array.Find(efeitos, sound => sound.name == name);
        if (e == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        e.source.Stop();
    }
}
