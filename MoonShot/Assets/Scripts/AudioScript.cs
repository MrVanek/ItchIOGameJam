using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    private AudioSource auds;



    private void Awake()
    {
        auds = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        auds.clip = clip;
        auds.Play();
    }
}
