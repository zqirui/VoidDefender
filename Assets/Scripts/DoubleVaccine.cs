using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleVaccine : MonoBehaviour
{
    [SerializeField] 
    private float _vaccineSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        // Shooting up!
        transform.Translate(Vector3.up * _vaccineSpeed * Time.deltaTime);
        
        //if the position of the shot is y > 7
        //destroy the shot

        if (transform.position.y > 7f)
        {
            //destroy the shots
            Destroy(this.gameObject);
        }
    }
}
