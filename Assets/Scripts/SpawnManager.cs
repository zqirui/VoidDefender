using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _normalAlienPrefab;
    [SerializeField] private GameObject _strongAlienPrefab;

    [SerializeField] private List<GameObject> _powerUpPickUps;

    [SerializeField] private float _delay = 2f;

    [SerializeField] private float _powerUPSpawnrate = 5f;

    [SerializeField] private GameObject _bossPrefab;

    public bool _spawnStrongAliens = false;
    private bool _spawningON = true;
    void Start()
    {
        StartCoroutine(spawnSystem());
        StartCoroutine(spawnPowerUp());
    }

    public void onPlayerDeath()
    {
        _spawningON = false;
        Destroy(this.gameObject);
    }
    
    IEnumerator spawnSystem()
    {
        System.Random rand = new System.Random();
        //forever = as long as the game is running
        while (_spawningON)
        {
            if (_spawnStrongAliens)
            {
                Debug.Log("Strong Alien Spawn active");
                //randomely spawn weak or strong alien after score 30
                int random = rand.Next(0, 2);
                Debug.Log("Index: " + random);
                if (random == 0)
                {
                    //spawn a strong alien
                    Instantiate(_strongAlienPrefab, new Vector3(x: Random.Range(-7f, 7f), 7f, 0), Quaternion.identity,
                        this.transform);
                }
                else
                {
                    //spawn a new virus
                    Instantiate(_normalAlienPrefab, new Vector3(x: Random.Range(-7f, 7f), 7f, 0), Quaternion.identity,
                        this.transform);
                }
            }
            else
            {
                //spawn a new virus
                Instantiate(_normalAlienPrefab, new Vector3(x: Random.Range(-7f, 7f), 7f, 0), Quaternion.identity,
                    this.transform);
            }
            
            //wait for 2 seconds
            yield return new WaitForSeconds(_delay);
        }
    }

    IEnumerator spawnPowerUp()
    {
        while (_spawningON)
        {
            System.Random random = new System.Random();
            int index = random.Next(0, _powerUpPickUps.Count);
            Instantiate(_powerUpPickUps[index], new Vector3(x: Random.Range(-7f, 7f), 7f, 0), Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_powerUPSpawnrate);
        }
    }

    public void PlayEnemyDestroyedSound()
    {
        PlayAudioSourceByGameObjectName("EnemyDestroyedSound");
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

    public void SpawnBoss()
    {
        Instantiate(_bossPrefab, new Vector3(Random.Range(-8f, 8f), 6f, 0f), Quaternion.Euler(0, 180, 0), this.transform);
        FindObjectOfType<UIManager>()._spawnBoss = false;
    }
}

