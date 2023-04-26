using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo2 : MonoBehaviour {
    private NavMeshAgent inimigo;
    private Transform point;
    private GameObject player;
    
    void Start() {
        player = GameObject.FindWithTag("Player");
        //  DetectaPlayer();
    }
    
    void Update() {
        //  Debug.Log("Te achei");
        DetectaPlayer();
        inimigo.SetDestination(point.position);       
    }

    private void DetectaPlayer() {
        inimigo = GetComponent<NavMeshAgent>();
        point = GameObject.Find("Player").transform;      
    }   
}
