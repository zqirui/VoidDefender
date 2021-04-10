using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using UnityEngine.EventSystems;

public class GameOverReplayButton : MonoBehaviour
{
    public void RestartGame()
    {
        //Debug.LogWarning("Button clicked");
        //Delete artifacts from old game
        var oldCanvas = GameObject.FindWithTag("GameUIManager");
        if(oldCanvas != null)
            Destroy(oldCanvas.gameObject);
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
        //restart Game Scene
        SceneManager.LoadScene(1);
    }

}
