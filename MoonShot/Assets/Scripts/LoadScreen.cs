using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{

    private int selected = 0;
    private int maxThings = 3;
    public Font fontDeselected;
    public Font fontSelected;
    public Text ssText;
    public Text aboutText;
    public Text onlineText;
    public Text htpText;

    private bool canSelect = true;


    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            DealWithInputs();
            ChangeFonts();
            SwitchScreens();
        }else //if (scene.buildIndex == 2)
        {
            GoBack();
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
            if (selected == 0)
            {
                SceneManager.LoadScene(1);
            }
            else if (selected == 1)
            {
                //SceneManager.LoadScene(3);
                //Coming Soon
            }

            else if (selected == 2)
            {
                SceneManager.LoadScene(2);
            }
            else if (selected == 3)
            {
                SceneManager.LoadScene(3);
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
        }
        else if (selected == 1)
        {
            ssText.font = fontDeselected;
            aboutText.font = fontDeselected;
            onlineText.font = fontSelected;
            htpText.font = fontDeselected;
        }
        else if (selected == 2)
        {
            ssText.font = fontDeselected;
            aboutText.font = fontSelected;
            onlineText.font = fontDeselected;
            htpText.font = fontDeselected;
        }
        else if (selected == 3)
        {
            ssText.font = fontDeselected;
            aboutText.font = fontDeselected;
            onlineText.font = fontDeselected;
            htpText.font = fontSelected;
        }
    }




    private void DealWithInputs()
    {
        if (Input.GetAxisRaw("P1Vertical") < -0.08f && canSelect)
        {
            selected++;
            if (selected > maxThings)
            {
                selected = 0;
            }
            canSelect = false;
        }
        else if (Input.GetAxisRaw("P1Vertical") > 0.08f && canSelect)
        {
            selected--;
            if (selected < 0)
            {
                selected = maxThings;
            }
            canSelect = false;
        }
        else if (Input.GetAxisRaw("P1Vertical") > -0.08f && Input.GetAxisRaw("P1Vertical") < 0.08f && !canSelect)
        {
            canSelect = true;
        }
    }
}
