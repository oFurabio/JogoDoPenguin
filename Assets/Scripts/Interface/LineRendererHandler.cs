using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHandler : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector3[] points = new Vector3[2];

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 99);
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, 99);

        points[0] = transform.position;
        points[1] = target.transform.position;

        lineRenderer.SetPositions(points);
    }
}
