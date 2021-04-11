using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    [SerializeField] 
    private GameObject _bossPrefab;

    [SerializeField] 
    private float _bossSpeed = 5f;

    [SerializeField] 
    private int _bossLife = 100;

    [SerializeField] 
    public bool _bossFight = false;

    [SerializeField] 
    private GameObject _bossBeam;

    private float _nextBeamTime = -1f;

    private float _nextBeamDelay = 2.5f;
    
    [SerializeField] 
    private GameObject _bossTorpedo;

    private float _nextTorpedoTime = -1f;

    private float _nextTorpedoDelay = 4.7f;
    
    [SerializeField] 
    private GameObject _rocketCollisionZone;
    
    private Vector3 _moveDirection = Vector3.right;

    private bool _allowBossActive = false;

    // Update is called once per frame
    void Update()
    {
        if (_bossFight)
        {
            if (transform.position.x >= 8)
            {
                _moveDirection = Vector3.right;
            }
            else if (transform.position.x <= -8)
            {
                _moveDirection = Vector3.left;
            }

            if (_allowBossActive)
            {
                if (_bossLife == 30)
                    _bossSpeed = 7.5f;
                transform.Translate(_moveDirection * _bossSpeed * Time.deltaTime);
                //shoot big beam
                if (Time.time > _nextBeamTime)
                {
                    _nextBeamTime = Time.time + _nextBeamDelay;
                    Instantiate(_bossBeam, this.transform.position + new Vector3(0, 5f, 0), Quaternion.identity);
                }
            
                //shoot torpedos
                if (Time.time > _nextTorpedoTime)
                {
                    _nextTorpedoTime = Time.time + _nextTorpedoDelay;
                    Instantiate(_bossTorpedo, this.transform.position, Quaternion.Euler(90, 0 ,0));
                }
            }
            
            this.transform.Translate(Vector3.down * 0.025f);
            if (this.transform.position.y < 3f)
            {
                transform.position = new Vector3(transform.position.x, y: 3f, z: 0f);
                _allowBossActive = true;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //If Boss and Vaccines collide
        if(other.CompareTag("Vaccine"))
        {
            //only destroy the enemy
            if (!other.name.Contains("Burst"))
            {
                Destroy(other.gameObject);
            }

            if (other.name.Contains("Rocket"))
            {
                Instantiate(_rocketCollisionZone, transform.position, Quaternion.identity);
                _bossLife -= 2;
            }
            //remove one health point
            _bossLife--;
            
        }

        if (_bossLife == 0)
        {
            Destroy(this.gameObject);
            FindObjectOfType<SpawnManager>().PlayEnemyDestroyedSound();
            //Transition to Game Over (Victory) Screen
            GameObject.FindWithTag("GameUIManager").GetComponent<UIManager>().HideVisuals();
            GameObject.FindWithTag("GameUIManager").GetComponent<UIManager>()._bossBeaten = true;
            SceneManager.LoadScene(2);
        }
    }
}
