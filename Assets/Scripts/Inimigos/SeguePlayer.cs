using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguePlayer : MonoBehaviour
{
    private Transform[] waypointList;
    public GameObject waypoints;

    public float moveSpeed = 0f;
    public float minDistance = 3f;
    public Transform target;
    public float dash = 0.0f;
    public float parar = 3;

    private int currentWaypointIndex = 0;
    private bool isFollowingPlayer = false;

    void Start()
    {
        int numWaypoints = waypoints.transform.childCount;
        waypointList = new Transform[numWaypoints];

        for (int i = 0; i < numWaypoints; i++)
        {
            waypointList[i] = waypoints.transform.GetChild(i);
        }

        transform.position = waypointList[currentWaypointIndex].position;

       
    }

    void Update()
    {

        if (!isFollowingPlayer)
        {

            transform.position = Vector3.MoveTowards(transform.position, waypointList[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

            if (transform.position == waypointList[currentWaypointIndex].position)
            {
                currentWaypointIndex++;

                if (currentWaypointIndex >= waypointList.Length)
                {
                    currentWaypointIndex = 0;
                }
            }

            if (Vector3.Distance(transform.position, target.position) <= minDistance)
            {
                isFollowingPlayer = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new(target.position.x, transform.position.y, target.position.z), moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) > minDistance * 2)
            {
                isFollowingPlayer = false;
            }
        }

        if (isFollowingPlayer)
        {
            //novo
            Vector3 directionPlayer = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionPlayer), 0.1f);
        }
        else
        {
            //novo
            Vector3 direction = waypointList[currentWaypointIndex].position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }

        if (Vector3.Distance(transform.position, target.position) >= minDistance / 2)
        {
            moveSpeed = 5;
        }

        if (Vector3.Distance(transform.position, target.position) <= parar)
        {
            moveSpeed = 0;
        }

        


    }

   


}
