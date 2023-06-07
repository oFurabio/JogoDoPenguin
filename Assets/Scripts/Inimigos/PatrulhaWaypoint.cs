using UnityEngine;

public class PatrulhaWaypoint : MonoBehaviour {
    [Header("Tipo de Inimigo")]
    public bool temDelay = false;
    public bool segueOPlayer = true;

    [Header("Velocidade")]
    public float velocidade = 2.0f;      // Velocidade de movimento do objeto

    [Header("Tempo de espera")]
    public float waitDelay = 3f;    // Tempo de espera
    
    [Header("Pontos de Patrulha")]
    public GameObject waypoints;    // GameObject com pontos de patrulha

    private Vector3 direction;
    private Transform[] waypointList;   // Lista de pontos de patrulha
    private float waitTimer = 3f;       // Contador do tempo de espera
    private int currentWaypoint = 0;    // Índice do ponto atual na lista

    private void Start() {
        // Inicializar a lista de pontos de patrulha
        int numWaypoints = waypoints.transform.childCount;
        waypointList = new Transform[numWaypoints];

        for (int i = 0; i < numWaypoints; i++) {
            waypointList[i] = waypoints.transform.GetChild(i);
        }
    }

    private void Update() {
        Patrulhando();
    }

    private void Patrulhando() {
        if (waitTimer >= waitDelay) {
            // Se o objeto chegou no ponto atual, avança para o próximo ponto e reseta o contador de espera
            if (Vector3.Distance(transform.position, waypointList[currentWaypoint].position) < 0.1f) {
                if(temDelay)
                    waitTimer = 0f;

                currentWaypoint = (currentWaypoint + 1) % waypointList.Length;
            }

            // Mover em direção ao ponto atual
            transform.position = Vector3.MoveTowards(transform.position, waypointList[currentWaypoint].position, velocidade * Time.deltaTime);

            // Rotacionar em direção ao próximo ponto
            direction = waypointList[currentWaypoint].position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
        else
            waitTimer += Time.deltaTime;
    }
}
