using System.Collections;
using System.Collections.Generic;
using R3.Triggers;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] protected float moveSpeed;
    public float GetMoveSpeed() => moveSpeed;
    [SerializeField] protected float patrolRange;
    public float GetPatrolRange() => patrolRange;

    [Header("Attack settings")] 
    [SerializeField] protected float detectionRange;
    public float GetDetectionRange() => detectionRange;
    [SerializeField] protected float attackCooldown;
    public float GetAttackCooldown() => attackCooldown;
    [SerializeField] protected int damage;
    public int GetDamage() => damage;

    
    [Header("Components")]
    protected Rigidbody2D rb;
    public Rigidbody2D GetRB() => rb;
    [SerializeField]protected ObservableCollision2DTrigger chaseTrigger;
    public ObservableCollision2DTrigger GetChaseTrigger() => chaseTrigger;
    [SerializeField]protected ObservableCollision2DTrigger attackTrigger;
    public ObservableCollision2DTrigger GetAttackTrigger() => attackTrigger;
    private CharacterHealth _enemyHealth;
    public CharacterHealth GetHealthComponent() => _enemyHealth;
    
    [Header("DropLoot")]
    [SerializeField] private GameObject[] dropLoot;
    public GameObject[] GetDropLoot() => dropLoot;
    
    private void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();
        _enemyHealth = GetComponent<CharacterHealth>();
    }
    
}
