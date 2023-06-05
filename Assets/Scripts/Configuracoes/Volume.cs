using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour {
    public AudioMixer am;

    public void CMasterVolume(float decimalVolume) {
        float dbVolume = Mathf.Log10(decimalVolume) * 20;

        if (decimalVolume == 0.0f)
            dbVolume = -80.0f;

        am.SetFloat("VMestre", dbVolume);
    }

    public void CMusicVolume(float decimalVolume)
    {
        float dbVolume = Mathf.Log10(decimalVolume) * 20;

        if (decimalVolume == 0.0f)
            dbVolume = -80.0f;

        am.SetFloat("VMusica", dbVolume);
    }

    public void CSfxVolume(float decimalVolume)
    {
        float dbVolume = Mathf.Log10(decimalVolume) * 20;

        if (decimalVolume == 0.0f)
            dbVolume = -80.0f;

        am.SetFloat("VSfx", dbVolume);
    }

    public void CAmbientVolume(float decimalVolume)
    {
        float dbVolume = Mathf.Log10(decimalVolume) * 20;

        if (decimalVolume == 0.0f)
            dbVolume = -80.0f;

        am.SetFloat("VAmbiente", dbVolume);
    }
}
