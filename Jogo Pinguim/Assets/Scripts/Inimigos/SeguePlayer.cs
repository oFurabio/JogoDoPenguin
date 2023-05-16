using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguePlayer : MonoBehaviour {
    public Transform[] waypoints;
    public float moveSpeed = 0f;
    public float minDistance = 0f;
    public Transform target;
    public float dash = 0.0f;
    public float parar = 0;

    private int currentWaypointIndex = 0;
    private bool isFollowingPlayer = false;

    void Start() {
        //player = GameObject.FindWithTag("Player");
        transform.position = waypoints[currentWaypointIndex].position;
    }

    void Update() {
        if (!isFollowingPlayer) {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[currentWaypointIndex].position) {
                currentWaypointIndex++;

                if (currentWaypointIndex >= waypoints.Length) {
                    currentWaypointIndex = 0;
                }
            }

            if (Vector3.Distance(transform.position, target.position) <= minDistance) {
                isFollowingPlayer = true;
            }
        } else {

            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) > minDistance * 2) {
                isFollowingPlayer = false;
            }


        }

        if (Vector3.Distance(transform.position, target.position) <= minDistance / 2) {
            moveSpeed = dash;
        }

        if (Vector3.Distance(transform.position, target.position) >= minDistance / 2) {
            moveSpeed = 10;
        }

        if (Vector3.Distance(transform.position, target.position) <= parar) {
            moveSpeed = 0;
        }

    }
}
