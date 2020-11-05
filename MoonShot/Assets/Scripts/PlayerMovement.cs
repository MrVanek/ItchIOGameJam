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

    private bool jumping = false;
    private float extraJumpDistance = .75f;
    private Rigidbody rb;
    private bool canJump = true;
    private float hMovement, vMovement, distanceToTheGround;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distanceToTheGround = GetComponentInChildren<Collider>().bounds.extents.y;
    }
    private void Update()
    {
        hMovement = Input.GetAxis("Horizontal");
        vMovement = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        } else if (Input.GetButtonUp("Jump"))
        {
            jumping = false;
        }
    }
    void FixedUpdate()
    {
        MovePlayer();
        JumpPlayer();
    }

    private void MovePlayer()
    {

        Vector3 moveDirection = new Vector3(hMovement * moveSpeed, 0f, vMovement * moveSpeed);

        //setting up where forward is based on the camera
        Vector3 forward = yPivot.transform.TransformDirection(moveDirection);

        //rotate toward forward
        Vector3 newFacing = new Vector3(forward.x, 0f, forward.z);
        Vector3 lookRotation = Vector3.RotateTowards(transform.forward, newFacing, rotateSpeed, 0f);
        rb.MoveRotation(Quaternion.LookRotation(lookRotation));



        rb.MovePosition(transform.position + (newFacing * Time.deltaTime));

    }

    private void JumpPlayer()
    {

        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !jumping)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * lowMultiplier * Time.deltaTime;
        }



        if (!canJump && rb.velocity.y <= 0)
        {
            canJump = Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + extraJumpDistance);
        }

        if (jumping && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            canJump = false;
        }
    }
}



