using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTimerBar : MonoBehaviour
{

    [SerializeField] private Slider _bar;
    private float _powerUpTime;
    private bool _stopTimer;
    private float _startTime;
    
    // Update is called once per frame
    void Update()
    {
        float time = _powerUpTime - (Time.time - _startTime);
        if (time <= 0)
            _stopTimer = true;
        if (!_stopTimer)
            _bar.value = time;
    }

    void Awake()
    {
        _stopTimer = false;
        _powerUpTime = GameObject.FindWithTag("Player").GetComponent<Player>().GetPowerUpTimeout();
        _bar.maxValue = _powerUpTime;
        _bar.value = _powerUpTime;
        //get time of creation
        _startTime = Time.time;
    }
}
