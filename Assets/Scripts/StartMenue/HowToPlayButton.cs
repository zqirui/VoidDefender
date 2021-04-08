using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HowToPlayButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToUnshown;
    [SerializeField] private Button _returnButton;
    [SerializeField] private Text _howToPlayText;

    public void ShowHowToPlay()
    {
        foreach (GameObject obj in _objectsToUnshown)
        {
            obj.gameObject.SetActive(false);
        }
        _returnButton.gameObject.SetActive(true);
        _howToPlayText.gameObject.SetActive(true);
    }
}
