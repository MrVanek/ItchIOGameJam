using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandling : MonoBehaviour
{
    public Transform ballSpawn;
    public Text P1Text;
    public Text P2Text;
    public Text WinText;
    public int endScore = 6;

    public bool gameOver = false;

    private int P1Score = 0;
    private int P2Score = 0;

    private AudioScript aud;
    private playerValues pv;


    private void Start()
    {
        aud = GameObject.FindGameObjectWithTag("Controller").GetComponent<AudioScript>();
        pv = GameObject.FindGameObjectWithTag("Controller").GetComponent<playerValues>();
    }
    void Update()
    {
        CheckGameEnd();
       
    }

    private void CheckGameEnd()
    {
        if (!gameOver && P1Score > endScore - 1)
        {
            WinText.text = "Player 1 Wins!";
            gameOver = true;
            if (pv.soundOn) aud.PlaySound(pv.gameOverP1);

        }
        if (!gameOver && P2Score > endScore - 1)
        {
            WinText.text = "Player 2 Wins!";
            gameOver = true;
            if (pv.soundOn) aud.PlaySound(pv.gameOverP2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1Goal")
        {
            P1Score++;
            P1Text.text = "Score: " + P1Score.ToString();
            transform.position = ballSpawn.position;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            if (pv.soundOn) aud.PlaySound(pv.goalP1);
        }
        else if (other.tag == "Player2Goal")
        {
            P2Score++;
            P2Text.text = "Score: " + P2Score.ToString();
            transform.position = ballSpawn.position;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            if (pv.soundOn) aud.PlaySound(pv.goalP2);
        }
    }

}
