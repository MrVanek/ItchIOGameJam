using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControlScript : MonoBehaviour
{
    private PlayerMovement pm;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void RunJumpScript()
    {
        pm.AddJumpForce();
        //pm.canCheckJump = true;
    }

    void StopAnimation()
    {
        anim.speed = 0;
        pm.canCheckJump = true;
    }
}
