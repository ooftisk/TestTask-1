using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    public int GetMaxHP() => maxHP;
    private int currentHP;
    public int GetCurrentHP() => currentHP;
    public ReactiveProperty<int> rCurrentHP { get; private set; }
    

    private void Awake()
    {
        currentHP = maxHP;
        rCurrentHP = new ReactiveProperty<int>(currentHP);
    }
        

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        rCurrentHP.Value = currentHP;
        if(currentHP <= 0)
            Die();
    }

    private void Die() // tip for later(no time for imp): move logic in Interface and override logic for death. Add logic in player death(load game/other things)
    {
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    public void SetHealth(int health)
    {
        currentHP = health;
    }
}
