using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMovement : MonoBehaviour
{
    private bool movingUp = true;
    private float movementCount = 0;
    public float moveSpeed = 0.5f;
    public float moveAmount = 3f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp && movementCount < moveAmount)
        {
            transform.position = transform.position + new Vector3(0, moveSpeed * Time.deltaTime, 0);
            movementCount += moveSpeed + Time.deltaTime;
            if (movementCount >= moveAmount)
            {
                movingUp = false;
                movementCount = 0; 
            }
        }
        else if (!movingUp && movementCount < moveAmount)
        {
            transform.position = transform.position + new Vector3(0, -moveSpeed * Time.deltaTime, 0);
            movementCount += moveSpeed + Time.deltaTime;
            if (movementCount >= moveAmount)
            {
                movingUp = true;
                movementCount = 0;
            }
        }


    }
}
