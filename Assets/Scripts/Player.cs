using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 7f;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject _vaccinePrefab;

    [SerializeField]
    private GameObject _doubleVaccine;

    [SerializeField] 
    private List<GameObject> _powerUps;
    
    [Header("Vaccination Parameters")]
    [SerializeField] 
    public float _vaccinationRate = 0.4f;
    private float _timeToVaccinate = -1f;
    
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField] 
    private SpawnManager _spawnManager;

    [SerializeField] 
    private HealthBar _healthBar;
    
    [SerializeField]
    public bool _usePowerUp = false;

    [SerializeField] 
    public bool _useDVaccine = false;

    [SerializeField] 
    public bool _useTVaccine = false;

    [Header("Powerup Parameters")]
    [SerializeField] 
    private float _powerupTimeout = 5f;

    private int _powerUpsIndex;
    
    void Start()
    {
        //Reset Player position on Start
        transform.position = new Vector3(x: 0, y: -4.5f, z: 0);
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        Vaccinate();
        
    }

    public void Damage()
    {
        //reduce _lives by one
        //if health is 0 destroy the player
        _lives -= 1;
        _healthBar.RemoveHeart();
        PlayAudioSourceByGameObjectName("DamageSound");
        
        if (_lives == 0)
        {
            if (_spawnManager != null)
            {
                _spawnManager.onPlayerDeath();
            }
            else
            {
                Debug.LogError("Spawn Manager not assigned!");
            }
            Destroy(this.gameObject);
            
            _uiManager.HideVisuals();
            //transition to Game Over Scene
            SceneManager.LoadScene(1);
        }
    }

    public void RelayScore(int score)
    {
        _uiManager.AddScore(score);
    }
    void Vaccinate()
    {
        // if spacebar pressed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _timeToVaccinate)
        {
            // add the rate to the current time to set the future possible vaccination time
            _timeToVaccinate = Time.time + _vaccinationRate;
            //Instantiating Prefab
            
            if (!_usePowerUp)
            {
                if (_useDVaccine)
                {
                    Instantiate(_doubleVaccine, transform.position + new Vector3(x: 0, y: 0.3f, z: 0),
                        Quaternion.identity);
                }
                else if (_useTVaccine)
                {
                    Instantiate(_vaccinePrefab, transform.position + new Vector3(x: 0, y: 0.85f, z: 0), Quaternion.identity);
                    Instantiate(_vaccinePrefab, transform.position + new Vector3(x: 0, y: 0.85f, z: 0), Quaternion.Euler(0,0,25));
                    Instantiate(_vaccinePrefab, transform.position + new Vector3(x: 0, y: 0.85f, z: 0), Quaternion.Euler(0,0,-25));
                }
                else
                {
                    Instantiate(_vaccinePrefab, transform.position + new Vector3(x: 0, y: 0.85f, z: 0), Quaternion.identity);
                }
            }
            else
            {
                Instantiate(_powerUps[_powerUpsIndex], transform.position + new Vector3(x: 0, y: 0.7f, z: 0),
                    Quaternion.Euler(270,0,0));
            }
        }
    }
    
    //player movement function
    void playerMovement()
    {
        
        //read player input on x and y axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Rotation (Barrel-Roll and Reset)
        transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * _speed * -10f * Time.deltaTime, 0), Space.World);
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            float yRot = transform.GetChild(0).localRotation.eulerAngles.y;
            transform.GetChild(0).Rotate(new Vector3(0,-yRot, 0), Space.World);
        }
        //apply player movement
        Vector3 playerTranslate = new Vector3(x: 1f * horizontalInput * _speed * Time.deltaTime,
            y: 1f * verticalInput * _speed * Time.deltaTime, 0f);
        transform.Translate(playerTranslate);
        
        //Setting up the vertical boundaries for the player
        //Check if play position is greater than 0 on y axis
        if (transform.position.y > 0)
        {
            //Keep player y position at 0
            transform.position = new Vector3(transform.position.x, y: 0, z: 0);
        }
        //Check for lower boundary
        else if (transform.position.y < -4.5)
        {
            transform.position = new Vector3(transform.position.x, y: -4.5f, z: 0f);
        }
        //Setting up horizontal boundaries for the player
        //Check if player escaped from the right side

        if (transform.position.x >= 8)
        {
            //Move player to the left side of the screen
            transform.position = new Vector3(x: -8, transform.position.y, z: 0);
            transform.GetChild(0).Rotate(new Vector3(0,horizontalInput,0),Space.World);
        }
        //Check if player escaped from the left side
        else if (transform.position.x <= -8)
        {
            //move player to the right side of the screen
            transform.position = new Vector3(x: 8, transform.position.y, z: 0);
            transform.GetChild(0).Rotate(new Vector3(0,horizontalInput,0),Space.World);
        }
    }

    public void ActivatePowerUp(bool isHeartContainer)
    {
        _usePowerUp = true;
        if (!isHeartContainer)
        {
            PlayAudioSourceByGameObjectName("PowerUpPickUpSound");
            //randomize power up each time when collecting
            System.Random rand = new System.Random();
            _powerUpsIndex = rand.Next(0, _powerUps.Count);
            Debug.Log("Index: " + _powerUpsIndex);
        }
        else
        {
            _lives += 1;
            _healthBar.AddHeart();
        }
        
        StartCoroutine(DeactivatePowerUp());
    }

    IEnumerator DeactivatePowerUp()
    {
        yield return new WaitForSeconds(_powerupTimeout);
        _usePowerUp = false;
    }

    public int GetLife()
    {
        return _lives;
    }

    private void PlayAudioSourceByGameObjectName(string gameObjectName)
    {
        foreach (AudioSource audio in this.GetComponentsInChildren<AudioSource>())
        {
            if (audio.gameObject.name == gameObjectName)
            {
                audio.Play();
            }
        }
    }
}
