using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            Time.timeScale = 1f;
        if (Input.GetKeyDown(KeyCode.Keypad2))
            Time.timeScale = .5f;
        if (Input.GetKeyDown(KeyCode.Keypad3))
            Time.timeScale = .25f;
    }
}
