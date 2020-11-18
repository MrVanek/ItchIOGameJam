using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    public float jumpForce = 6f;
    public Transform yPivot;
    public float fallMultiplier = 1.5f;
    public float lowMultiplier = 2f;
    public float extraJumpDistance = 5f;
    public int playerNumber = 1;

    public bool canJump = true;
    private bool canCheckJump = true;
    public bool canMove = true;
    private Rigidbody rb;
    private Animator anim;
    private float hMovement, vMovement, distanceToTheGround;
    private bool reachedApex = false;
    private bool jumping = false;


    void Start()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        distanceToTheGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }
    private void Update()
    {
        CheckAnalogInputs();
        CheckButtonInputs();

    }

    private void CheckButtonInputs()
    {
        if (Input.GetButtonDown("P" + playerNumber.ToString() + "Jump"))
        {
            jumping = true;
        }
        else if (!Input.GetButton("P" + playerNumber.ToString() + "Jump"))
        {
            jumping = false;
        }
    }

    private void CheckAnalogInputs()
    {
        hMovement = Input.GetAxis("P" + playerNumber.ToString() + "Horizontal");
        vMovement = Input.GetAxis("P" + playerNumber.ToString() + "Vertical");
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            MovePlayer();
            JumpPlayer();
           // punchPlayer();
        }
    }

    private void punchPlayer()
    {
        anim.SetTrigger("Throw");
    }

    private void MovePlayer()
    {
        
        if (Mathf.Abs(hMovement) < 0.1f) hMovement = 0;
        if (Mathf.Abs(vMovement) < 0.1f) vMovement = 0;
        if (hMovement == 0 && vMovement == 0)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
        }

        Vector3 moveDirection = new Vector3(hMovement * moveSpeed, 0f, vMovement * moveSpeed);

        //setting up where forward is based on the camera
        Vector3 forward = yPivot.transform.TransformDirection(moveDirection);

        //rotate toward forward
        Vector3 newFacing = new Vector3(forward.x, 0f, forward.z);
        Vector3 lookRotation = Vector3.RotateTowards(transform.forward, newFacing, rotateSpeed, 0f);
        rb.MoveRotation(Quaternion.LookRotation(lookRotation));

        //rb.MovePosition(transform.position + (newFacing * Time.deltaTime));

        
        Vector3 direction = (transform.position + (newFacing * Time.deltaTime)) - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, direction.magnitude))
            rb.MovePosition(transform.position + (newFacing * Time.deltaTime));
        else
            rb.MovePosition(hit.point);

    }

    private void JumpPlayer()
    {
  
        if (rb.velocity.y < 0)
        {
            if (anim.GetFloat("speedMultiplier") == 0f && !reachedApex)
            {
                anim.SetFloat("speedMultiplier", 0f);
                reachedApex = true;
            }
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
            canCheckJump = true;
        }
        else if (rb.velocity.y > 0 && !jumping)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * lowMultiplier * Time.deltaTime;
        }

        if (canCheckJump && !canJump && rb.velocity.y <= 0)
        {

            float total = distanceToTheGround + extraJumpDistance;
            Debug.DrawRay(transform.position, Vector3.down * total, Color.yellow);

            LayerMask mask = LayerMask.GetMask("Arena");
            canJump = Physics.Raycast(transform.position, Vector3.down, total, mask);

            if (canJump)
            {
                anim.SetFloat("speedMultiplier", 1f);
                reachedApex = false;
                jumping = false;
            }
        }



        if (jumping && canJump)
        {
            anim.SetTrigger("Jump");
            canJump = false;
            canCheckJump = false;
        }
    }

    public void AddJumpForce()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
}



