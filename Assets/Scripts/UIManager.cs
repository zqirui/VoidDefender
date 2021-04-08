using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _score = 0;
    
    [SerializeField] 
    private Text _scoreText;

    [SerializeField]
    private Text _versionText;

    void Start()
    {
        _scoreText.text = "Score: " + _score;
    }

    private void Update()
    {
        if (_score >= 20)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>()._useDVaccine = true;
        }
        if (_score >= 50)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>()._vaccinationRate = 0.1f;
        }

        if (_score >= 100)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>()._useDVaccine = false;
            GameObject.FindWithTag("Player").GetComponent<Player>()._useTVaccine = true;
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
    }

    public int GetScore()
    {
        return _score;
    }

    public void HideVisuals()
    {
        _scoreText.gameObject.SetActive(false);
        _versionText.gameObject.SetActive(false);
    }

    private void Awake()
    {
        //keep UIManager alive after Scene change
        DontDestroyOnLoad(transform.gameObject);
    }
}
