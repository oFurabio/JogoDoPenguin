using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour {
    public CinemachineFreeLook cam;
    public Toggle x, y;
    public Slider senX, senY;
    
    public void SensivelX()
    {
        cam.m_XAxis.m_MaxSpeed = senX.value;
    }

    public void InvertX()
    {
        cam.m_XAxis.m_InvertInput = x.isOn;
    }

    public void SensivelY()
    {
        cam.m_YAxis.m_MaxSpeed = senY.value;
    }

    public void InvertY()
    {
        cam.m_YAxis.m_InvertInput = y.isOn;
    }
}
