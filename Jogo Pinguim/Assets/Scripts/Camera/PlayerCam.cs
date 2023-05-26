using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour {
    public Transform orientation, player, playerObj, chaseLookAt;
    public GameObject thirdPersonCam, chaseCam, topdownCam;
    public Rigidbody rb;
    Vector3 inputDir;

    public float rotationSpeed;

    public CameraStyle currentStyle;

    public enum CameraStyle {
        Basic,
        Chase,
        Topdown
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        //  Trocar estilo de camera
        if (Input.GetKeyDown(KeyCode.Alpha1)/*!PlayerMovement.sliding*/) SwitchCameraStyle(CameraStyle.Basic);
        //if (Input.GetKeyDown(KeyCode.Alpha2)/*PlayerMovement.sliding*/) SwitchCameraStyle(CameraStyle.Chase);
        //if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.Topdown);

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if(currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown) {
            float hInput = Input.GetAxis("Horizontal");
            float vInput = Input.GetAxis("Vertical");

            if (!Health.dead)
                inputDir = orientation.forward * vInput + orientation.right * hInput;
            else
                inputDir = new(0f, 0f, 0f);

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        else if(currentStyle == CameraStyle.Chase) {
            Vector3 dirToChaseLookAt = chaseLookAt.position - new Vector3(transform.position.x, chaseLookAt.position.y, transform.position.z);
            orientation.forward = dirToChaseLookAt.normalized;

            playerObj.forward = dirToChaseLookAt.normalized;
        }
        
    }

    public void SwitchCameraStyle(CameraStyle newStyle) {
        chaseCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        topdownCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Chase) chaseCam.SetActive(true);
        if (newStyle == CameraStyle.Topdown) topdownCam.SetActive(true);

        currentStyle = newStyle;
    }
}
