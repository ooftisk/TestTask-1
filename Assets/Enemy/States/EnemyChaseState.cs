using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyFSM fsm, EnemyManager enemyManager) : base(fsm, enemyManager)
    {
    }
    

    private Vector2 _playerPos;
    public override void Enter()
    {
        DisposeOnEnter();
        HandleDeath();
        
        var playerPos = enemyManager.GetChaseTrigger();
        playerPos.OnTriggerStay2DAsObservable()
            .Where(collider => collider.CompareTag("Player"))
            .Subscribe(_ => 
            {
                _playerPos = _.transform.position;
            }).AddTo(disposable);
        
        var chase = Observable.EveryUpdate(UnityFrameProvider.FixedUpdate);
        chase.Subscribe(_ => ChasePlayer()).AddTo(disposable);
        
        var patrol = enemyManager.GetChaseTrigger();
        patrol.OnTriggerExit2DAsObservable()
            .Where(collision => collision.gameObject.CompareTag("Player"))
            .Subscribe(_ => 
            {
                fsm.ChangeState(new EnemyPatrolState(fsm, enemyManager));
            }).AddTo(disposable);
        
        var attack = enemyManager.GetAttackTrigger();
        attack.OnTriggerStay2DAsObservable().Take(1)
            .Where(collision => collision.gameObject.CompareTag("Player"))
            .Subscribe(_ => 
            {
                fsm.ChangeState(new EnemyAttackState(fsm, enemyManager, _playerPos));
            }).AddTo(disposable);
        
    }

    public override void Exit()
    {
        DisposeOnExit();
    }

    private void ChasePlayer()
    {
        if (Vector2.Distance(_playerPos, enemyManager.transform.position) < 0.1f)
        {
            enemyManager.GetRB().velocity = Vector2.zero;
            fsm.ChangeState(new EnemyAttackState(fsm, enemyManager, _playerPos));
        }
        else
        {
            Vector2 direction = (_playerPos - (Vector2)enemyManager.transform.position).normalized;
            enemyManager.GetRB().velocity = direction * enemyManager.GetMoveSpeed();
        }
    }
}
