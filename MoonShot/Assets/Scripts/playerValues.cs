using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerValues : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    public float jumpForce = 6f;

    public float fallMultiplier = 1.5f;
    public float lowMultiplier = 2f;

    public float throwSpeed = 5f;

    public float rotationSpeed = 5f;
    public float maxRotation = 45f;
    public float minRotation = -20f;

    public Text wt;
    public PlayerMovement p1;
    public PlayerMovement p2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ReadyStart");
    }
    private IEnumerator ReadyStart()
    {
        float count = 0;

        wt.text = "READY";

        while (count < 2f)
        {
            count += Time.deltaTime;
            yield return null;
        }

        wt.text = "Start!";

        count = 0;
        while (count < .5f)
        {
            count += Time.deltaTime;
            yield return null;
        }
        p1.canMove = true;
        p2.canMove = true;
        wt.text = "";
    }

}
