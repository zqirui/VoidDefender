using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTorpedo : MonoBehaviour
{
    [SerializeField]
    private float _torpedoSpeed = 2f;

    private float _nextDirectionTimer = -1f;

    private float _delay = 1.5f;
    
    private float _angleRotation;

    [SerializeField] private RocketCollisionZone _collisionZone;
    [SerializeField] private GameObject _firePrefab;
    private bool _fire = true;
    
    // Update is called once per frame
    private void Start()
    {
        _angleRotation = UnityEngine.Random.Range(-45f, 45f);
    }

    void Update()
    {
        
        if (_fire)
        {
            Instantiate(_firePrefab,this.transform.position,Quaternion.Euler(-90,0,0),this.transform);
            _fire = false;
        }
        
        // Shooting down
        transform.Translate(Vector3.forward * _torpedoSpeed * Time.deltaTime);

        if (Time.time > _nextDirectionTimer)
        {
            _nextDirectionTimer = Time.time + _delay;
            _angleRotation = UnityEngine.Random.Range(-45f, 45f);
            transform.Rotate( 0,0,_angleRotation,Space.World);
        }
        
        
        
        
        //if the position of the torpedo is smaller than -7
        //destroy the Torpedo

        if (transform.position.y < -7f)
        {
            //destroy the torpedo
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(_collisionZone, this.transform.position, Quaternion.identity);
            other.GetComponent<Player>().Damage();
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
    }
}
