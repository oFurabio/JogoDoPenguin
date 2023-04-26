using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Caminho3 : MonoBehaviour {
    /*public Transform[] waypoints;
    public float moveSpeed = 5.0f;
    private int waypointIndex = 0;

    void Update() {
        if (waypoints.Length == 0) return;

        Vector3 targetDirection = waypoints[waypointIndex].position - transform.position;
       
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);
   
        if (transform.position == waypoints[waypointIndex].position) {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, moveSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }*/

    public float speed = 2.0f; // Velocidade de movimento do objeto
    public GameObject waypoints; // GameObject com pontos de patrulha

    private Transform[] waypointList; // Lista de pontos de patrulha
    private int currentWaypoint = 0; // Índice do ponto atual na lista

    private void Start()
    {
        // Inicializar a lista de pontos de patrulha
        int numWaypoints = waypoints.transform.childCount;
        waypointList = new Transform[numWaypoints];

        for (int i = 0; i < numWaypoints; i++)
        {
            waypointList[i] = waypoints.transform.GetChild(i);
        }
    }

    private void Update()
    {
        // Se o objeto chegou no ponto atual, avança para o próximo ponto
        if (Vector3.Distance(transform.position, waypointList[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypointList.Length;
        }

        // Mover em direção ao ponto atual
        transform.position = Vector3.MoveTowards(transform.position, waypointList[currentWaypoint].position, speed * Time.deltaTime);

        // Rotacionar em direção ao próximo ponto
        Vector3 direction = waypointList[currentWaypoint].position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }

}
