using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class HowToPlayButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToUnshown;
    [SerializeField] private Button _returnButton;
    [SerializeField] private GameObject _howToPlayScrollView;

    public void ShowHowToPlay()
    {
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
        foreach (GameObject obj in _objectsToUnshown)
        {
            obj.gameObject.SetActive(false);
        }
        _returnButton.gameObject.SetActive(true);
        _howToPlayScrollView.SetActive(true);
    }
}
