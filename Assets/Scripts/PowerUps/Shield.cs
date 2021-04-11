using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Virus"))
        {
            Destroy(other.gameObject);
            if (!(other.name.Contains("BossBeam") || other.name.Contains("BioTorpedo")))
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }
            FindObjectOfType<SpawnManager>().PlayEnemyDestroyedSound();
        }
    }

    private void Awake()
    {
        this.transform.SetParent(GameObject.FindWithTag("Player").GetComponent<Player>().transform, false);
    }
}
