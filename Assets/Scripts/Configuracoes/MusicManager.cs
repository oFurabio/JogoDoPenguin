using System;
using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public Sound[] sounds;

    [SerializeField]
    private string musica;

    void Awake() {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.audioMixer;

            s.source.spatialBlend = s.spatialBlend;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        Play(musica);
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " não encontrado!");
            return;
        }

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        s.source.Play();
    }
}
