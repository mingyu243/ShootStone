using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Play : MonoBehaviour
{
    [SerializeField] GameMain _gameMain;

    [Header("오브젝트")]
    [SerializeField] Slider _powerSlider;

    private void Awake()
    {
        _gameMain.Charger.OnValueChanged += OnValueChanged;
    }

    private void OnValueChanged(float ratio)
    {
        _powerSlider.value = ratio;
    }
}
