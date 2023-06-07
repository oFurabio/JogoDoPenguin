using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour {
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider ambiente;
    public Slider master;
    public Slider musica;
    public Slider sfx;

    float dbMasterVol, dbVolSfx, dbVolMus, dbVolAmb;
    float sMasterVol, sVolSfx, sVolMus, sVolAmb;

    private void Start() {
        LoadPrefs();
    }

    public void SetMasterVolume(float decimalVolume) {
        dbMasterVol = Mathf.Log10(decimalVolume) * 20;

        if (decimalVolume == 0.0f)
            dbMasterVol = -80.0f;

        sMasterVol = master.value;
        PlayerPrefs.SetFloat("sMasterVol", sMasterVol);

        audioMixer.SetFloat("VMestre", dbMasterVol);
        PlayerPrefs.SetFloat("VMestre", dbMasterVol);
    }

    public void SetMusicVolume(float decimalVolume) {
        dbVolMus = Mathf.Log10(decimalVolume) * 20;

        if (decimalVolume == 0.0f)
            dbVolMus = -80.0f;

        sVolMus = musica.value;
        PlayerPrefs.SetFloat("sMusicVol", sVolMus);

        audioMixer.SetFloat("VMusica", dbVolMus);
        PlayerPrefs.SetFloat("VMusica", dbVolMus);
    }

    public void SetSfxVolume(float decimalVolume) {
        dbVolSfx = Mathf.Log10(decimalVolume) * 20;

        if (decimalVolume == 0.0f)
            dbVolSfx = -80.0f;

        sVolSfx = sfx.value;
        PlayerPrefs.SetFloat("sSfxVol", sVolSfx);

        audioMixer.SetFloat("VSfx", dbVolSfx);
        PlayerPrefs.SetFloat("VSfx", dbVolSfx);
    }

    public void SetAmbientVolume(float decimalVolume)
    {
        dbVolAmb = Mathf.Log10(decimalVolume) * 20;

        if (decimalVolume == 0.0f)
            dbVolAmb = -80.0f;

        sVolAmb = ambiente.value;
        PlayerPrefs.SetFloat("sAmbientVol", sVolAmb);

        audioMixer.SetFloat("VAmbiente", dbVolAmb);
        PlayerPrefs.SetFloat("VAmbiente", dbVolAmb);
    }

    public void LoadPrefs() {
        //  Volume do jogo
        dbMasterVol = PlayerPrefs.GetFloat("VMestre");
        dbVolMus = PlayerPrefs.GetFloat("VMusica");
        dbVolSfx = PlayerPrefs.GetFloat("VSfx");
        dbVolAmb = PlayerPrefs.GetFloat("VAmbiente");

        //  Valor dos sliders
        master.value = PlayerPrefs.GetFloat("sMasterVol");
        musica.value = PlayerPrefs.GetFloat("sMusicVol");
        sfx.value = PlayerPrefs.GetFloat("sSfxVol");
        ambiente.value = PlayerPrefs.GetFloat("sAmbientVol");
    }
}
