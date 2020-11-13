﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigScript : MonoBehaviour
{
    public Transform player;
    public Transform yPivot;
    public Transform xPivot;

    public float rotationSpeed = 5f;
    public float maxRotation = 45f;
    public float minRotation = -20f;


    void Update()
    {
        PivotRotation();
    }

    private void PivotRotation()
    {
        transform.position = player.position;

        float hRotation = Input.GetAxis("RHorizontal");
        float vRotation = Input.GetAxis("RVertical");

        Vector3 yRotation = yPivot.transform.eulerAngles;
        Vector3 xRotation = xPivot.transform.eulerAngles;


        yRotation.y += hRotation * rotationSpeed;

        xRotation.x += vRotation * rotationSpeed;
        xRotation.y = yRotation.y;


        yPivot.transform.eulerAngles = yRotation;
        xPivot.transform.eulerAngles = xRotation;

        if (xPivot.eulerAngles.x > maxRotation && xPivot.eulerAngles.x < 180f)
        {

            xPivot.rotation = Quaternion.Euler(maxRotation, xPivot.eulerAngles.y, xPivot.eulerAngles.z);
        }
        if (xPivot.eulerAngles.x < 360f + minRotation && xPivot.eulerAngles.x > 180f)
        {

            xPivot.rotation = Quaternion.Euler(360f + minRotation, xPivot.eulerAngles.y, xPivot.eulerAngles.z);
        }


    }
}