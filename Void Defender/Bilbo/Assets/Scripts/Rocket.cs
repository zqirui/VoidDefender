using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _rocketSpeed;
    

    // Update is called once per frame
    void Update()
    {
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
