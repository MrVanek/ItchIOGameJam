using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettings : MonoBehaviour
{

    public bool soundOn = true;
    public bool musicOn = true;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SoundSettings");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
}
