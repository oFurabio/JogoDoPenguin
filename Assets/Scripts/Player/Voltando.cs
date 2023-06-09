using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voltando : MonoBehaviour
{
    public Transform player; 
    public Transform teleportObject;
    public float teleportYThreshold = 10f; 

    private void Update()
    {
        if (player.position.y <= teleportYThreshold)
        {
            TeleportPlayerToObject();
        }
    }

    private void TeleportPlayerToObject()
    {
        player.position = teleportObject.position;
        
    }
}
