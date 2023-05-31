using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travada : MonoBehaviour
{
    public PlayerCam pc;
    public GameObject final;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pc.SwitchCameraStyle(PlayerCam.CameraStyle.Chase);

            Invoke(nameof(CabouSe), 5f);
        }
    }

    public void CabouSe() {
        final.SetActive(true);
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
