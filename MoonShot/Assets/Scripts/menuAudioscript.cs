using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuAudioscript : MonoBehaviour
{

    private AudioSource auds;

    public AudioClip click;
    public AudioClip change;

    
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("menuSound");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        auds = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        auds.clip = clip;
        auds.Play();
    }
}
