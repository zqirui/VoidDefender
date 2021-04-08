using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void ExitGame()
    {
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
        Application.Quit(0);
    }
}
