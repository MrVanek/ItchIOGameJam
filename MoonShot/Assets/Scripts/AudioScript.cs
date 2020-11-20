using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

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

    public AudioClip bgMusic;



    // Start is called before the first frame update
    void Start()
    {
        auds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        auds.clip = clip;
        auds.Play();
    }
}
