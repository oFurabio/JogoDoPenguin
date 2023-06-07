using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour {
    public CinemachineFreeLook cam;
    public Toggle y;
    public Slider sensibilidade;



    private void Start() {
        LoadPrefs();
    }

    public void Sensibilidade(float x)    {
        switch (x) {
            case 5f:
                cam.m_XAxis.m_MaxSpeed = 250f;
                cam.m_YAxis.m_MaxSpeed = 2f;
                break;
            case 4f:
                cam.m_XAxis.m_MaxSpeed = 200f;
                cam.m_YAxis.m_MaxSpeed = 1.6f;
                break;
            case 3f:
                cam.m_XAxis.m_MaxSpeed = 150f;
                cam.m_YAxis.m_MaxSpeed = 1.2f;
                break;
            case 2f:
                cam.m_XAxis.m_MaxSpeed = 100f;
                cam.m_YAxis.m_MaxSpeed = 0.8f;
                break;
            case 1f:
                cam.m_XAxis.m_MaxSpeed = 50f;
                cam.m_YAxis.m_MaxSpeed = 0.4f;
                break;
        }

        PlayerPrefs.SetFloat("sens", x);
    }

    public void InvertY() {
        cam.m_YAxis.m_InvertInput = !y.isOn;
    }

    private void LoadPrefs() {
        PlayerPrefs.GetFloat("sens");
        y.isOn = cam.m_YAxis.m_InvertInput;
    }
}
