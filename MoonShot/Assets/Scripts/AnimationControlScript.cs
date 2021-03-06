﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControlScript : MonoBehaviour
{
    private PlayerMovement pm;
    private BallHandling bh;
    private Animator anim;
 

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
        bh = GetComponentInParent<BallHandling>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void RunJumpScript()
    {
        pm.AddJumpForce();
        
    }

    void StopAnimation()
    {
        if (!pm.canJump)
        {

            anim.SetFloat("speedMultiplier", 0f);
        }
    }
    void AllowMove()
    {
        pm.canMove = true;
    }

    void StartAnimation()
    {
        anim.SetFloat("speedMultiplier", 1f);
        //anim.speed = 1;
    }
    void ReleaseBall()
    {
        bh.ReleaseBall();

    }
    
}
