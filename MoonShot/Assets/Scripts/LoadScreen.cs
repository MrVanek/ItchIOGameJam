using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{

    private int selected = 0;
    private int maxThings = 4;
    public Font fontDeselected;
    public Font fontSelected;
    public Text ssText;
    public Text aboutText;
    public Text onlineText;
    public Text htpText;
    public Text SoundText;
    private SoundSettings ss;
    private Scene scene;
    private menuAudioscript aud;

    private bool canSelect = true;

    private void Start()
    {
        aud = FindObjectOfType<menuAudioscript>();
        ss = FindObjectOfType<SoundSettings>();
        scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 5)
        {
            maxThings = 2;
            UpdateSoundText();
        }

    }

    private void UpdateSoundText()
    {
        if (ss.musicOn)
            onlineText.text = "Music On";
        else
            onlineText.text = "Music Off";
        if (ss.soundOn)
            ssText.text = "Sound On";
        else
            ssText.text = "Sound Off";
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            DealWithInputs();
            ChangeFonts();
            SwitchScreens();
        }
        else if (scene.buildIndex == 5)
        {
            //volume scene
            DealWithInputs();
            ChangeFontsSound();
            SwitchScreensSound();

        }
   
        else //if (scene.buildIndex == 2)
        {
            GoBack();
        }
    }

    private void ChangeFontsSound()
    {
        if (selected == 0)
        {
            ssText.font = fontSelected;
            aboutText.font = fontDeselected;
            onlineText.font = fontDeselected;
        }
        else if (selected == 1)
        {
            ssText.font = fontDeselected;
            aboutText.font = fontDeselected;
            onlineText.font = fontSelected;
        }
        else if (selected == 2)
        {
            ssText.font = fontDeselected;
            aboutText.font = fontSelected;
            onlineText.font = fontDeselected;
        }
       
    }

    private void SwitchScreensSound()
    {
        if (Input.GetButtonDown("P1Jump"))
        {
            if (ss.soundOn) aud.PlaySound(aud.click);
            if (selected == 0)
            {
                ss.soundOn = !ss.soundOn;
                UpdateSoundText();
            }
            else if (selected == 1)
            {
                ss.musicOn = !ss.musicOn;
                UpdateSoundText();
            }

            else if (selected == 2)
            {
                GoBack();
            }
        }

    }

    private void GoBack()
    {
        if (Input.GetButtonDown("P1Jump"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SwitchScreens()
    {
        
        if (Input.GetButtonDown("P1Jump"))
        {
            if (ss.soundOn) aud.PlaySound(aud.click);
            if (selected == 0)
            {
                SceneManager.LoadScene(1);
                Destroy(GameObject.Find("MenuAudio"));
            }
            else if (selected == 1)
            {
                SceneManager.LoadScene(4);
            }

            else if (selected == 2)
            {
                SceneManager.LoadScene(2);
            }
            else if (selected == 3)
            {
                SceneManager.LoadScene(3);
            }
            else if (selected == 4)
            {
                SceneManager.LoadScene(5);
            }
        }
    }






    private void ChangeFonts()
    {
       if (selected == 0)
      {
            ssText.font = fontSelected;
            aboutText.font = fontDeselected;
            onlineText.font = fontDeselected;
            htpText.font = fontDeselected;
            SoundText.font = fontDeselected;
        }
        else if (selected == 1)
        {
            ssText.font = fontDeselected;
            aboutText.font = fontDeselected;
            onlineText.font = fontSelected;
            htpText.font = fontDeselected;
            SoundText.font = fontDeselected;
        }
        else if (selected == 2)
        {
            ssText.font = fontDeselected;
            aboutText.font = fontSelected;
            onlineText.font = fontDeselected;
            htpText.font = fontDeselected;
            SoundText.font = fontDeselected;
        }
        else if (selected == 3)
        {
            ssText.font = fontDeselected;
            aboutText.font = fontDeselected;
            onlineText.font = fontDeselected;
            htpText.font = fontSelected;
            SoundText.font = fontDeselected;
        }
        else if (selected == 4)
        {
            ssText.font = fontDeselected;
            aboutText.font = fontDeselected;
            onlineText.font = fontDeselected;
            htpText.font = fontDeselected;
            SoundText.font = fontSelected;
        }
    }




    private void DealWithInputs()
    {
        if (Input.GetButtonDown("P1Throw") && scene.buildIndex != 0)
        {
            GoBack();
        }

        if (Input.GetAxisRaw("P1Vertical") < -0.08f && canSelect)
        {
            selected++;
            if (selected > maxThings)
            {
                selected = 0;
            }
            canSelect = false;
            if(ss.soundOn) aud.PlaySound(aud.change);
        }
        else if (Input.GetAxisRaw("P1Vertical") > 0.08f && canSelect)
        {
            selected--;
            if (selected < 0)
            {
                selected = maxThings;
            }
            canSelect = false;
            if (ss.soundOn) aud.PlaySound(aud.change);
        }
        else if (Input.GetAxisRaw("P1Vertical") > -0.08f && Input.GetAxisRaw("P1Vertical") < 0.08f && !canSelect)
        {
            canSelect = true;
        }


    }
}
