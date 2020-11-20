using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketShipSctipt : MonoBehaviour
{

    float rocketSpeed = 40;

    public Transform waypoint;
    private Transform[] points;
 
    private Vector3 initialPosition;


    private void Awake()
    {

    }
    
    void Start()
    {
        initialPosition = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, waypoint.position) < .5f)
        {
            GetNextWaypoint();
        }

        transform.LookAt (waypoint.position);
        transform.position = Vector3.MoveTowards(transform.position, waypoint.position, rocketSpeed * Time.deltaTime);
    }

    private void GetNextWaypoint()
    {
        Debug.Log("Resetting");
        transform.position = initialPosition;
    }
}
