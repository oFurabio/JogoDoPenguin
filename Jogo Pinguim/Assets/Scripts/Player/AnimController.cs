using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {
    public PlayerMovement pm;
    Animator anim;

    int idleCount = 0;

    public int idleToAFK = 8;

    public KeyCode quackKey = KeyCode.J;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(quackKey)) {
            anim.SetBool("quack", true);
        } else {
            anim.SetBool("quack", false);
        }
    }

    public void CountIdle() {
        idleCount++;
        if (idleCount == idleToAFK) {
            anim.SetBool("AFK", true);
        } else {
            anim.SetBool("AFK", false);
        }
    }

    public void ResetIdleCounter() {
        idleCount = 0;
        anim.SetBool("AFK", false);
    }
}
