using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : MonoBehaviour
{
    [SerializeField] private float _burstSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        // Shooting up!
        transform.Translate(Vector3.forward * _burstSpeed * Time.deltaTime);
        
        //if the position of our vaccine is y > 7
        //destroy the shot

        if (transform.position.y > 7f)
        {
            //GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            //destroy the vaccine
            Destroy(this.gameObject);
        }
    }
}
