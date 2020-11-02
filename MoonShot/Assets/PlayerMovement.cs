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

    private float extraJumpDistance = .5f;
    private Rigidbody rb;
    private bool canJump = true;
    private float distanceToTheGround;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distanceToTheGround = GetComponentInChildren<Collider>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        MovePlayer();
        JumpPlayer();
    }

    private void MovePlayer()
    {
        float hMovement = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");

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
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * lowMultiplier * Time.deltaTime;
        }



        if (!canJump)
        {
            canJump = Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + extraJumpDistance);
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            canJump = false;
        }
    }
}



