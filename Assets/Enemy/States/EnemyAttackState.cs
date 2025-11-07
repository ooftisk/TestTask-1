using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.TextCore.Text;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyFSM fsm, EnemyManager enemyManager) : base(fsm, enemyManager)
    {
        
    }

    private Vector2 _playerPos;
    public EnemyAttackState(EnemyFSM fsm, EnemyManager enemyManager, Vector2 playerPosition) : base(fsm, enemyManager)
    {
        _playerPos = playerPosition;
    }
    
    
    public override void Enter()
    {
        DisposeOnEnter();
        HandleDeath();
        
        enemyManager.GetRB().velocity = Vector2.zero;
        //Simple implementation to deal damage
        Vector2 origin = enemyManager.transform.position;
        float radius = 1f;                     
        Vector2 direction = (_playerPos - origin).normalized;
        float distance = Vector2.Distance(origin, _playerPos); 
        int layerMask = LayerMask.GetMask("Player"); 

       
        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
        
        //Debug.DrawLine(origin, origin + direction * distance, Color.red, 1f);
        
        if (hit.collider != null)
        {
            CharacterHealth healthComponent = hit.collider.GetComponentInParent<CharacterHealth>();
            healthComponent.TakeDamage(enemyManager.GetDamage());
        }
        
        var delay = Observable.Timer(TimeSpan.FromSeconds(1.5));
        delay.Subscribe(_ =>
            {
                fsm.ChangeState(new EnemyChaseState(fsm, enemyManager));
            }).AddTo(disposable);
    }

    public override void Exit()
    {
        DisposeOnExit();
    }
}
