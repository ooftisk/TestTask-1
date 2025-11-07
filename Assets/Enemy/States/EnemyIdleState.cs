using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;


public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyFSM fsm, EnemyManager enemyManager) : base(fsm, enemyManager)
    {
        
    }

    
    public override void Enter()
    {
        DisposeOnEnter();
        
        enemyManager.GetRB().velocity = Vector2.zero;

        HandleDeath();
        
        var patrol = Observable.Timer(TimeSpan.FromSeconds(2));
            patrol.Subscribe(_ =>
                fsm.ChangeState(new EnemyPatrolState(fsm, enemyManager)))
            .AddTo(disposable);
        
        var chaseTrigger = enemyManager.GetChaseTrigger();
        chaseTrigger.OnTriggerEnter2DAsObservable().Take(1)
            .Where(collision => collision.gameObject.CompareTag("Player"))
            .Subscribe(_ => 
            {
                fsm.ChangeState(new EnemyChaseState(fsm, enemyManager));
            }).AddTo(disposable);

        var attackTrigger = enemyManager.GetAttackTrigger();
        attackTrigger.OnTriggerEnter2DAsObservable().Take(1)
            .Where(collision => collision.gameObject.CompareTag("Player"))
            .Subscribe(_ => 
            {
                fsm.ChangeState(new EnemyAttackState(fsm, enemyManager));
            }).AddTo(disposable);
    }

    public override void Exit()
    {
        DisposeOnExit();
    }
    
}
