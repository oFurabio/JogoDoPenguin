using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStickToMouse : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1f;

    private void Update()
    {
        // Get right analog stick input
        float rightStickX = Input.GetAxis("RightStickX");
        float rightStickY = Input.GetAxis("RightStickY");

        // Translate to mouse movement
        float mouseX = rightStickX * sensitivity;
        float mouseY = rightStickY * sensitivity;

        // Apply mouse movement
        MouseLook(mouseX, mouseY);
    }

    private void MouseLook(float mouseX, float mouseY)
    {
        // Rotate the camera based on mouse movement
        float rotationX = transform.localEulerAngles.y + mouseX;

        // Invert mouseY for natural movement
        float rotationY = transform.localEulerAngles.x - mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        // Apply the new rotation to the camera
        transform.localEulerAngles = new Vector3(rotationY, rotationX, 0f);
    }
}
