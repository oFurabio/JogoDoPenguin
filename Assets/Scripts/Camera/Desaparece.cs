using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desaparece : MonoBehaviour {
    public SkinnedMeshRenderer player, olhos;
    public CinemachineFreeLook cam;

    private void Update()
    {
        if (cam.m_YAxis.Value <= 0.1f)
        {
            player.enabled = false;
            olhos.enabled = false;
        }
        else
        {
            player.enabled = true;
            olhos.enabled = true;
        }
    }
}
