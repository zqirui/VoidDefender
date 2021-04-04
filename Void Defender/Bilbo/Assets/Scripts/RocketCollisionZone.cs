using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RocketCollisionZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Virus"))
        {
            Destroy(other.gameObject);
            GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
        }
    }

    private void Awake()
    {
        Destroy(this.gameObject, 0.5f);
    }
}
