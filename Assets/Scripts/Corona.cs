using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Corona : MonoBehaviour
{
    [SerializeField]
    private float _pandemicSpeed = 3.5f;

    [SerializeField] 
    private GameObject _coronaPrefab;

    [SerializeField] 
    private float _pandemicSpread = 5f;

    [SerializeField] 
    private GameObject _rocketCollisionZone;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(x:Random.Range(-7f, 7f), y:_pandemicSpread, z:0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.down * _pandemicSpeed * Time.deltaTime);
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(x:Random.Range(-7f, 7f), y:_pandemicSpread, z: 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if the object we collide with is the player
        if (other.CompareTag("Player"))
        {
            //destroy/damage player and the virus
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
            
        }
        //but if Virus and Vaccine collide
        else if(other.CompareTag("Vaccine"))
        {
            //only destroy the enemy
            if (!other.name.Contains("Burst"))
            {
                Destroy(other.gameObject);
            }

            if (other.name.Contains("Rocket"))
            {
                Instantiate(_rocketCollisionZone, transform.position, Quaternion.identity);
            }
            //destroy Virus and Vaccine
            GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            FindObjectOfType<SpawnManager>().PlayEnemyDestroyedSound();
            Destroy(this.gameObject);
        }
    }
}
