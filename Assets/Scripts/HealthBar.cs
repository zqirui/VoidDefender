using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _heartPrefab;
    private int _lives;
    private List<GameObject> _hearts = new List<GameObject>();
    private void Start()
    {
        _lives = _player.GetLife();
        //init health bar
        for (int i = 0; i < _lives; i++)
        {
            var heart = Instantiate(_heartPrefab, new Vector3((8.5f - 0.8f * i), 3.5f, 0f), Quaternion.Euler(270,0,0), this.transform);
            _hearts.Add(heart);
        }
    }

    public void RemoveHeart()
    {
        _lives -= 1;
        var heart = _hearts[_lives];
        _hearts.Remove(heart);
        Destroy(heart.gameObject);
    }
    
    public void AddHeart()
    {
        _lives += 1;
        var heart = Instantiate(_heartPrefab, new Vector3(8.5f - 0.8f *(_lives - 1), 3.5f, 0), Quaternion.Euler(270,0,0), this.transform);
        _hearts.Add(heart);
    }
}
