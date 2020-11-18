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

    private int playerNumber;
    private PlayerMovement pm;
    private bool hasBall = false;
    private bool canGetBall = true;
    private float maxDistance = 100;
    private float count = 0;
    private Animator anim;
    private GameObject ball, crosshair, target;
    private bool punching = false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pm = GetComponent<PlayerMovement>();
        playerNumber = pm.playerNumber;
        crosshair = Instantiate(crosshairPrefab);
        ToggleCrosshair(false);
    }



    void Update()
    {
        if (!gamestate.gameOver)
        {

            if (Input.GetButtonDown("P" + playerNumber.ToString() + "Throw"))
            {
                anim.SetTrigger("Throw");
            }

            CarryBall();
        }
    }

    private void CarryBall()
    {
        if (hasBall)
        {
            PositionCrosshair();
            ball.transform.position = ballSpot.position;
            ball.transform.rotation = transform.rotation;
        }
    }

    public void ReleaseBall()
    {
        if (hasBall)
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

        else if (punching)
        {
            if (target.name == "P1Parent" || target.name == "P2Parent")
            {
                Animator tarAnim = target.GetComponentInChildren<Animator>();
                if (!tarAnim.GetCurrentAnimatorStateInfo(0).IsName("Sweep Fall 0") && !tarAnim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Stand Up"))
                {
                   tarAnim.SetTrigger("Fall");
                   tarAnim.SetFloat("speedMultiplier", 1f);
                   target.GetComponent<PlayerMovement>().canMove = false;
                    if (target.GetComponent<BallHandling>().hasBall)
                    {
                        //launch ball.
                    }
                }
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

    private void OnTriggerEnter(Collider other)
    {
        punching = true;
        target = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        punching = false;
    }

}

