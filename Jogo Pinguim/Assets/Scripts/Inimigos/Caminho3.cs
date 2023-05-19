using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Caminho3 : MonoBehaviour {
    public float speed = 2.0f; // Velocidade de movimento do objeto
    public GameObject waypoints; // GameObject com pontos de patrulha

    private Transform[] waypointList; // Lista de pontos de patrulha
    private int currentWaypoint = 0; // �ndice do ponto atual na lista

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
        // Se o objeto chegou no ponto atual, avan�a para o pr�ximo ponto
        if (Vector3.Distance(transform.position, waypointList[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypointList.Length;
        }

        // Mover em dire��o ao ponto atual
        transform.position = Vector3.MoveTowards(transform.position, waypointList[currentWaypoint].position, speed * Time.deltaTime);

        // Rotacionar em dire��o ao pr�ximo ponto
        Vector3 direction = waypointList[currentWaypoint].position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
