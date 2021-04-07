using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;

public class HeartContainer : MonoBehaviour
{

    [SerializeField] 
    private float _speed = 2f;

    void Start()
    {
        transform.Rotate(270,0,0, Space.Self);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
        transform.Rotate(0,0,5, Space.Self);
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ActivatePowerUp(true);
            Destroy(this.gameObject);
        }
    }
}
