using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandling : MonoBehaviour
{
    public Transform ballSpot;
    public float throwSpeed = 5f;
    public ScoreHandling gamestate;
    public GameObject crosshairPrefab;
    public Camera myCamera;


    private bool hasBall = false;
    private bool canGetBall = true;
    private float maxDistance = 100;
    private float count = 0;
    private Animator anim;
    private GameObject ball;
    private GameObject crosshair;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        crosshair = Instantiate(crosshairPrefab);
        ToggleCrosshair(false);
    }



    void Update()
    {
        if (!gamestate.gameOver)
            ThrowBall();
    }

    private void ThrowBall()
    {
        if (hasBall)
        {
            PositionCrosshair();
            ball.transform.position = ballSpot.position;
            ball.transform.rotation = transform.rotation;
            if (Input.GetButtonDown("Throw"))
            {
                anim.SetTrigger("Throw");
            }
        }
    }

    public void ReleaseBall()
    {
        ToggleCrosshair(false);
        hasBall = false;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.detectCollisions = true;
        rb.transform.position = rb.gameObject.transform.position + (transform.forward);
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

            ToggleCrosshair(true);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.detectCollisions = false;
            ball = collision.gameObject;
            hasBall = true;
            canGetBall = false;
        }
    }

    void PositionCrosshair()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Transform crosshairStart = ballSpot;
        Vector3 chSpawn = crosshairStart.position;
        Vector3 dir = ray.GetPoint(maxDistance) - transform.position;

        if (Physics.Raycast(chSpawn, dir, out hit, maxDistance))
        {
            crosshair.transform.position = hit.point;
            crosshair.transform.LookAt(myCamera.transform);
        }
    }


    private void ToggleCrosshair(bool enabled)
    {
        crosshair.SetActive(enabled);
    }
}

