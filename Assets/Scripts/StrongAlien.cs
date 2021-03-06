using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAlien : MonoBehaviour
{
    [SerializeField]
    private float _pandemicSpeed = 1.5f;

    [SerializeField] 
    private GameObject _StrongAlienPrefab;

    [SerializeField] 
    private float _pandemicSpread = 5f;

    [SerializeField] 
    private float _health = 3f;

    [SerializeField] 
    private GameObject _rocketCollisionZone;

    //1 = right, -1= left
    private float _verticalDirection;

    // Start is called before the first frame update
    void Start()
    {
        _verticalDirection = Random.Range(-1f, 1f);
        transform.position = new Vector3(x:Random.Range(-7f, 7f), y:_pandemicSpread, z:0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _pandemicSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * _verticalDirection * _pandemicSpeed * Time.deltaTime);
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
                Destroy(this.gameObject);
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }
            //remove one health point
            _health--;
            
        }

        if (_health == 0)
        {
            Destroy(this.gameObject);
            FindObjectOfType<SpawnManager>().PlayEnemyDestroyedSound();
            GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
        }
    }
}
