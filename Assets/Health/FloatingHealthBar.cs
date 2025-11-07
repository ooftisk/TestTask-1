using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    private CharacterHealth _healthComponent;
    private Slider _hpSlider;
    private float _maxHP;
    
    void Start()
    {
        _healthComponent = GetComponentInParent<CharacterHealth>();
        _hpSlider = GetComponent<Slider>();
        _maxHP = _healthComponent.GetMaxHP();
        UpdateUI((int)_maxHP);
        
        _healthComponent.rCurrentHP.Subscribe(hp =>
        {
            UpdateUI(hp);
        }).AddTo(this);
    }

    private void UpdateUI(int hp)
    {
        _hpSlider.value = hp/_maxHP;
    }
}
