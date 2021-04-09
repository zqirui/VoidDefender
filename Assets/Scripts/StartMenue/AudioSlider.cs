using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _audioObject;

    void Start()
    {
        _slider.maxValue = 1.0f;
        _slider.value = _audioObject.GetComponent<AudioSource>().volume;
    }

    public void OnSliderChanged()
    {
        _audioObject.GetComponent<AudioSource>().volume = _slider.value;
    }
}
