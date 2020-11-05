using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandling : MonoBehaviour
{
    public Transform ballSpot;
    public float throwSpeed = 5f;
    private bool hasBall = false;
    private bool canGetBall = true;
    private GameObject ball;
    private float count = 0;

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
                hasBall = false;
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.detectCollisions = true;
                rb.transform.position = rb.gameObject.transform.position + new Vector3(0, 0, 1);   
                rb.AddForce(transform.forward,ForceMode.Impulse);
                StartCoroutine("ballWait");
            }
        }
    }


    private IEnumerator ballWait()
    {
        count = 0;

        while (count < .25f)
        {
            count += Time.deltaTime;
            yield return null;
        }

        canGetBall = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball" && canGetBall)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.detectCollisions = false;
            ball = collision.gameObject;
            hasBall = true;
            canGetBall = false;
        }
        
    }
}
