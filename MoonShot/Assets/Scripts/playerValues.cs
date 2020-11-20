using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerValues : MonoBehaviour
{
    public bool soundOn = true;
    public bool musicOn = true;

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
    private AudioScript aud;
    public AudioScript MusicPlayer;


    private AudioSource auds;

    public AudioClip jumpP1;
    public AudioClip jumpP2;

    public AudioClip throwP1;
    public AudioClip throwP2;

    public AudioClip ready;
    public AudioClip start;

    public AudioClip gameOverP1;
    public AudioClip gameOverP2;

    public AudioClip goalP1;
    public AudioClip goalP2;

    public AudioClip fallingP1;
    public AudioClip fallingP2;

    public AudioClip bgMusic;


    // Start is called before the first frame update
    void Start()
    {
        soundOn = FindObjectOfType<SoundSettings>().soundOn;
        musicOn = FindObjectOfType<SoundSettings>().musicOn;
            aud = GetComponent<AudioScript>();
            if (musicOn)
            {
                MusicPlayer.PlaySound(bgMusic);
            }
            StartCoroutine("ReadyStart");
       
    }
    private IEnumerator ReadyStart()
    {
        float count = 0;

        wt.text = "READY";
        if (soundOn) aud.PlaySound(ready);


        while (count < 2f)
        {
            count += Time.deltaTime;
            yield return null;
        }

        wt.text = "Start!";
        if (soundOn) aud.PlaySound(start);

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
