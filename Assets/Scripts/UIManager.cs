using System;
using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _score = 0;
    
    [SerializeField] 
    private Text _scoreText;

    [SerializeField]
    private Text _versionText;

    [SerializeField] private PowerUpTimerBar _powerUpBarPrefab;

    [SerializeField] private List<Material> _powerUpBarIconMats;
    void Start()
    {
        _scoreText.text = "Score: " + _score;
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

    public void InstantiatePowerUpBar(ActivePowerUpType type, float powerUpTime)
    {
        var bar = Instantiate(_powerUpBarPrefab, new Vector3(8f,1f,0), Quaternion.Euler(0,0,90), this.transform);
        bar.transform.SetParent(GameObject.FindWithTag("GameUIManager").transform, false);
        GameObject icon = GameObject.FindWithTag("PowerUpBarIcon");
        icon.GetComponent<MeshRenderer>().material = GetCorrespondingMat(type);
        Destroy(bar.gameObject, powerUpTime);
    }

    private Material GetCorrespondingMat(ActivePowerUpType type)
    {
        Material powerUpMat = null;
        switch (type)
        {
            case ActivePowerUpType.Burst:
                foreach (Material mat in _powerUpBarIconMats)
                {
                    if (mat.name.Contains("Burst"))
                        powerUpMat = mat;
                }
                break;
            case ActivePowerUpType.Rocket:
                foreach (Material mat in _powerUpBarIconMats)
                {
                    if (mat.name.Contains("Rocket"))
                        powerUpMat = mat;
                }

                break;
            case ActivePowerUpType.Shield:
                foreach (Material mat in _powerUpBarIconMats)
                {
                    if (mat.name.Contains("Shield"))
                        powerUpMat = mat;
                }
                break;
        }

        return powerUpMat;
    }
}
