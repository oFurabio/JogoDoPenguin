using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sfx {
    public string name;

    public AudioClip clip;

    public AudioMixerGroup audioMixer;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    [Range(0f, 1f)]
    public float spatialBlend;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
