using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texto : MonoBehaviour
{
    //public float rayDistance = 10.0f;
    public float distancia = 0;
    //public LayerMask layerMask;
    private Transform player; 
    public Text texto;
    void Start()
    {
        texto.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {     
        Vector3 rayDirection = player.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDirection, out hit, distancia))
        {
            if (hit.transform.CompareTag("Player"))
            {
                texto.enabled = true;             
            }          
        }
        else
        {
            texto.enabled = false;
        }
    }
}
   

