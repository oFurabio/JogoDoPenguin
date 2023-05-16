using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltandoAoCair : MonoBehaviour {
    public float X = 0;
    public float y = 0;
    public float z = 0;
    [SerializeField] GameObject player;

    void Update() {
        if (transform.position.y <= -90) {
            player.transform.position = new Vector3(X, y, z);
        }
    }
}
