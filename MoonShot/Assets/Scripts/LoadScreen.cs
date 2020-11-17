using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{

    private int selected = 0;
    public Font fontDeselected;
    public Font fontSelected;
    public Text ssText;
    public Text aboutText;
    public Text onlineText;
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
        }else if (scene.buildIndex == 2)
        {
            GoBack();
        }
    }

    private void GoBack()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SwitchScreens()
    {
        if (Input.GetButtonDown("Jump"))
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

        }
    }






    private void ChangeFonts()
    {
       if (selected == 0)
      {
            Debug.Log("Selcted 0");
            ssText.font = fontSelected;
            aboutText.font = fontDeselected;
            onlineText.font = fontDeselected;
        
      } else if (selected == 1)
        {
            Debug.Log("Selcted 1");
            ssText.font = fontDeselected;
            aboutText.font = fontDeselected;
            onlineText.font = fontSelected;
        
        }
     else if (selected == 2)
        {
            Debug.Log("Selcted 2");
            ssText.font = fontDeselected;
            aboutText.font = fontSelected;
            onlineText.font = fontDeselected;
        }
    }




    private void DealWithInputs()
    {
        if (Input.GetAxisRaw("Vertical") < -0.08f && canSelect)
        {
            selected++;
            if (selected > 2)
            {
                selected = 0;
            }
            canSelect = false;
        }
        else if (Input.GetAxisRaw("Vertical") > 0.08f && canSelect)
        {
            selected--;
            if (selected < 0)
            {
                selected = 2;
            }
            canSelect = false;
        }
        else if (Input.GetAxisRaw("Vertical") > -0.08f && Input.GetAxisRaw("Vertical") < 0.08f && !canSelect)
        {
            canSelect = true;
        }
    }
}
