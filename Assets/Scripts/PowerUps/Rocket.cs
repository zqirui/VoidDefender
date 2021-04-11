using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _rocketSpeed;

    [SerializeField] private GameObject _firePrefab;

    private bool _fire = true;
    

    // Update is called once per frame
    void Update()
    {
        if (_fire)
        {
            Instantiate(_firePrefab,this.transform.position,Quaternion.Euler(90,0,0),this.transform);
            _fire = false;
        }
        
        // Shooting up!
        transform.Translate(Vector3.forward * _rocketSpeed * Time.deltaTime);

        transform.Rotate( 0,5,0,Space.World);
        //if the position of our vaccine is y > 7
        //destroy the shot

        if (transform.position.y > 7f)
        {
            //destroy the vaccine
            Destroy(this.gameObject);
        }
    }
    
}
