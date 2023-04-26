using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAInimigo : MonoBehaviour {
    private NavMeshAgent inimigo;
    private Transform point;
    private GameObject player;
    public float distancia = 0.0f;  
    
    void Start() {
        player = GameObject.FindWithTag("Player");
        //  DetectaPlayer();
    }
    
    void Update() {
        if(Vector3.Distance (transform.position, player.transform.position) <= distancia) {
            Debug.Log("Te achei");
            DetectaPlayer();
            inimigo.SetDestination(point.position);
        }
    }
    private void DetectaPlayer() {
        inimigo = GetComponent<NavMeshAgent>();
        point = GameObject.Find("Player").transform;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(this.gameObject);
        }
    }
}
