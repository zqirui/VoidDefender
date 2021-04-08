using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenueButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToUnshown;
    [SerializeField] private List<GameObject> _objectsToShow;
    
    public void ReturnToMenue()
    {
        foreach (GameObject obj in _objectsToUnshown)
        {
            obj.gameObject.SetActive(false);
        }

        foreach (GameObject obj in _objectsToShow)
        {
            obj.gameObject.SetActive(true);
        }
    }
}
