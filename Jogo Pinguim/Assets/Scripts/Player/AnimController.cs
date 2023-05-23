using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {
    Animator anim;

    int idleCount = 0;

    public int idleToAFK = 8;

    public KeyCode quackKey = KeyCode.J;

    public GameObject quack1;
    public GameObject quack2;

    public float delay = 2f;
    private bool isQuackActive = false;
    private float activationTime;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(quackKey) && !isQuackActive) {
            anim.SetBool("quack", true);

            quack1.SetActive(true);
            quack2.SetActive(true);
            isQuackActive = true;
            activationTime = Time.time;

        } else {
            anim.SetBool("quack", false);
        }

        if (isQuackActive && Time.time >= activationTime + delay)
        {
            quack1.SetActive(false);
            quack2.SetActive(false);
            isQuackActive = false;
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
