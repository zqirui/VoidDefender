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

    [SerializeField] private GameObject _permaUpIconPrefab;

    [SerializeField] private List<Material> _permaUpMats;

    [SerializeField] private GameObject _audioSlider;
    private bool _instantiateIcon = true;
    void Start()
    {
        _scoreText.text = "Score: " + _score;
    }

    private void Update()
    {
        if (_score == 20)
        {
            if(_instantiateIcon)
                InstantiatePermaUpIcon();
            GameObject.FindWithTag("Player").GetComponent<Player>()._useDVaccine = true;
        }

        if (_score > 20 && _score < 50)
            _instantiateIcon = true;
        if (_score == 50)
        {
            if(_instantiateIcon)
                InstantiatePermaUpIcon();
            GameObject.FindWithTag("Player").GetComponent<Player>()._vaccinationRate = 0.1f;
            GameObject.FindWithTag("Player").GetComponent<Player>()._useKeyPressed = true;
        }

        if (_score > 50 && _score < 100)
            _instantiateIcon = true;
        if (_score == 100)
        {
            if(_instantiateIcon)
                InstantiatePermaUpIcon();
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
        _audioSlider.gameObject.SetActive(false);
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

    public void InstantiatePermaUpIcon()
    {
        var icon = Instantiate(_permaUpIconPrefab, new Vector3(0,0,0), Quaternion.Euler(450, -90, 90));
        Debug.Log("Icon Instantiated");
        icon.GetComponent<MeshRenderer>().material = GetPermaUpMaterial();
        Destroy(icon.gameObject, 1f);
        _instantiateIcon = false;
    }

    private Material GetPermaUpMaterial()
    {
        Material permaUpMat = null;
        switch (_score)
        {
            case 20:
                foreach (Material mat in _permaUpMats)
                {
                    if (mat.name.Contains("double"))
                        permaUpMat = mat;
                } 
                break;
            case 50:
                foreach (Material mat in _permaUpMats)
                {
                    if (mat.name.Contains("fast"))
                        permaUpMat = mat;
                } 
                break;
            case 100:
                foreach (Material mat in _permaUpMats)
                {
                    if (mat.name.Contains("scatter"))
                        permaUpMat = mat;
                } 
                break;
        }

        return permaUpMat;
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
