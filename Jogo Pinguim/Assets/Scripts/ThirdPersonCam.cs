using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour {
    [Header("Referências")]
    public Transform orientation;
    public Transform player, playerObj;
    public Rigidbody rb;

    private static bool some = false;

    public float rotationSpeed;

    void Start() {
        CursorHandler();
    }

    void Update() {
        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player object
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * vInput + orientation.right * hInput;

        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }

    public static void CursorHandler() {
        if (!some) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            some = !some;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            some = !some;
        }
        
    }
}
