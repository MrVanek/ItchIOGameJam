using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandling : MonoBehaviour
{
    public Transform ballSpot;

    private bool hasBall = false;
    private GameObject ball;
    

    void Start()
    {
        
    }


    void Update()
    {
        //Debug.Log(hasBall);
        if (hasBall)
        {
            ball.transform.position = ballSpot.position;

            if (Input.GetButtonDown("Throw"))
            {
                Debug.Log("Throwing");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
            collision.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            ball = collision.gameObject;
            hasBall = true;
        }
        
    }
}
