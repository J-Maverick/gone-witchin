using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{

    public GameObject credits;
    public GameObject startButton;
    public GameObject quitButton;
    public GameObject musicController;
    public GameObject title;

    public void toggleCredits()
    {
        if (credits.activeSelf)
        {
            credits.SetActive(false);
            startButton.SetActive(true);
            quitButton.SetActive(true);
            title.SetActive(true);
            musicController.GetComponent<Animator>().SetBool("Credits", false);
        }
        else if (!credits.activeSelf)
        {
            credits.SetActive(true);
            startButton.SetActive(false);
            quitButton.SetActive(false);
            title.SetActive(false);
            musicController.GetComponent<Animator>().SetBool("Credits", true);
        }
    }
}
