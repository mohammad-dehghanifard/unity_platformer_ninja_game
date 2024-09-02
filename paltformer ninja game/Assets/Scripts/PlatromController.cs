using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatromController : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public Transform platform;
    private int currentPointIndex;

    void Start()
    {
        
    }

    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[currentPointIndex].position,speed * Time.deltaTime);

        if(Vector3.Distance(platform.position, points[currentPointIndex].position) <= 0.05f)
        {
            currentPointIndex++;
        }

        if(currentPointIndex >= points.Length)
        {
            currentPointIndex = 0;
        }
    }
}
