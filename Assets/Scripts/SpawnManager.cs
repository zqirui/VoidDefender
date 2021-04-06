using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> _virusPrefabs;

    [SerializeField] private List<GameObject> _powerUpPickUps;

    [SerializeField] private float _delay = 2f;

    [SerializeField] private float _powerUPSpawnrate = 5f;

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
        //forever = as long as the game is running
        while (_spawningON)
        {
            System.Random random = new System.Random();
            int index = random.Next(0, _virusPrefabs.Count);
            //spawn a new virus
            Instantiate(_virusPrefabs[index], new Vector3(x: Random.Range(-7f, 7f), 7f, 0), Quaternion.identity,
                this.transform);
            
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
}

