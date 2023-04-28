using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltandoAoCair : MonoBehaviour
{
   
    public float X = 0;
    public float y = 0;
    public float z = 0;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -90)
        {
            player.transform.position = new Vector3(X, y, z);
        }
    }
}
