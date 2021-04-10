using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour
{
    [SerializeField] private float _beamSpeed = 15f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _beamSpeed * Time.deltaTime);
        
        if (transform.position.y < -9f)
        {
            //destroy the beam
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage();
        }
    }
}
