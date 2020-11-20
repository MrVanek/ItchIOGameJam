using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallHandling : MonoBehaviour
{
    public Transform ballSpot;
    private float throwSpeed;
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
    public playerValues pv;

    private AudioScript aud;

    private void Start()
    {
        pv = GameObject.FindGameObjectWithTag("Controller").GetComponent<playerValues>();
        throwSpeed = pv.throwSpeed;
        anim = GetComponentInChildren<Animator>();
        pm = GetComponent<PlayerMovement>();
        playerNumber = pm.playerNumber;
        crosshair = Instantiate(crosshairPrefab);
        ToggleCrosshair(false);
        aud = GetComponent<AudioScript>();
    }



    void Update()
    {
        if (!gamestate.gameOver)
        {

            if (Input.GetButtonDown("P" + playerNumber.ToString() + "Throw"))
            {
                anim.SetTrigger("Throw");
                if (playerNumber == 1 && pv.soundOn)
                    aud.PlaySound(pv.throwP1);
                else if (playerNumber == 2 && pv.soundOn)
                    aud.PlaySound(pv.throwP2);
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
            ThrowBall(false);
        }

        else if (punching)
        {
            if (target.name == "P1Parent" || target.name == "P2Parent")
            {
                Animator tarAnim = target.GetComponentInChildren<Animator>();
                if (!tarAnim.GetCurrentAnimatorStateInfo(0).IsName("Sweep Fall 0") && !tarAnim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Stand Up"))
                {
                   tarAnim.SetTrigger("Fall");

                    if (playerNumber == 1 && pv.soundOn)
                        aud.PlaySound(pv.fallingP2);
                    else if (playerNumber == 2 && pv.soundOn)
                        aud.PlaySound(pv.fallingP1);

                    tarAnim.SetFloat("speedMultiplier", 1f);
                   target.GetComponent<PlayerMovement>().canMove = false;
                    BallHandling tbh = target.GetComponent<BallHandling>();
                    if (tbh.hasBall)
                    {
                        tbh.ThrowBall(true);

                    }
                }
            }
        }
    }

    private void ThrowBall(bool randomDir)
    {
        ToggleCrosshair(false);
        hasBall = false;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.detectCollisions = true;
        rb.transform.position = rb.gameObject.transform.position + (transform.forward);
        if (!randomDir)
        {
            rb.AddForce(ball.transform.forward * throwSpeed, ForceMode.Impulse);
        }
        else
        {
            Vector3 rand = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1f, 1f));
            
            rb.AddForce(rand * throwSpeed * 1.5f, ForceMode.Impulse);
        }
            
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

