using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;

public class EnemyPatrolState : EnemyBaseState
{
    private Vector2 _randomPosition;
    public EnemyPatrolState(EnemyFSM fsm, EnemyManager enemyManager) : base(fsm, enemyManager)
    {
        
    }

    
    public override void Enter()
    {

        DisposeOnEnter();
        HandleDeath();

        var patrol = Observable.EveryUpdate()
            .Do(_ =>
            {
                if (Vector2.Distance(enemyManager.transform.position, _randomPosition) > 0.5f)
                {
                    MoveToPoint();
                }
                else
                {
                    enemyManager.GetRB().velocity = Vector2.zero;
                }
            });
            patrol.Subscribe().AddTo(disposable);
            
            var idleTransition = Observable.Timer(TimeSpan.FromSeconds(2));
            idleTransition.Subscribe(_ =>
            {
                enemyManager.GetRB().velocity = Vector2.zero; 
                fsm.ChangeState(new EnemyIdleState(fsm, enemyManager)); 
            }).AddTo(disposable);

            var chaseTrigger = enemyManager.GetChaseTrigger();
            chaseTrigger.OnTriggerEnter2DAsObservable().Take(1)
                .Where(collision => collision.gameObject.CompareTag("Player"))
                .Subscribe(_ => 
                {
                    fsm.ChangeState(new EnemyChaseState(fsm, enemyManager));
                }).AddTo(disposable);
            
             /* var attackTrigger = enemyManager.GetAttackTrigger();
            attackTrigger.OnTriggerEnter2DAsObservable().Take(1)
                .Where(collision => collision.gameObject.CompareTag("Player"))
                .Subscribe(_ => 
                {
                    Debug.Log("Attack");
                    fsm.ChangeState(new EnemyAttackState(fsm, enemyManager));
                }).AddTo(disposable); */ //Check if needed
            
            
    }

    public override void Exit()
    {
        DisposeOnExit();
    }

    Vector2 GetRandomPosition()
    {
        var randOffset = (Vector2)UnityEngine.Random.insideUnitSphere * enemyManager.GetPatrolRange();
        return randOffset + (Vector2)enemyManager.transform.position;
    }

    private void MoveToPoint()
    {
        Vector2 direction = (_randomPosition - (Vector2)enemyManager.transform.position).normalized;
        enemyManager.GetRB().velocity = direction * enemyManager.GetMoveSpeed();
    }
}
