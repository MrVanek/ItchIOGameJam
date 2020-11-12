using System;
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
    public ScoreHandling gamestate;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        //if (!gamestate.gameOver)
            ThrowBall();
    }

    private void ThrowBall()
    {
        if (hasBall)
        {
            
            ball.transform.position = ballSpot.position;
            ball.transform.rotation = transform.rotation;
            if (Input.GetButtonDown("Throw"))
            {
                anim.SetBool("Throwing", true);
                // Debug.Log("Throwing");
             
            }
        }
    }

    public void ReleaseBall()
    {
        hasBall = false;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.detectCollisions = true;
        rb.transform.position = rb.gameObject.transform.position + (transform.forward);

        //     Vector3 forward = yPivot.transform.TransformDirection(moveDirection);
        rb.AddForce(ball.transform.forward * throwSpeed, ForceMode.Impulse);
        StartCoroutine("ballWait");
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

