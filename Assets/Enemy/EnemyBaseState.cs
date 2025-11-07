using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public abstract class EnemyBaseState 
{
    protected EnemyFSM fsm;
    protected EnemyManager enemyManager;
    protected CompositeDisposable disposable;

    public EnemyBaseState(EnemyFSM fsm, EnemyManager enemyManager)
    {
        this.fsm = fsm;
        this.enemyManager = enemyManager;
    }
    
    public virtual void Enter()
    {
    
    }
    
    public virtual void Exit()
    {
    
    }
    
    protected void DisposeOnEnter()
    {
        disposable?.Dispose();
        disposable = new CompositeDisposable();
    }

    protected void DisposeOnExit()
    {
        disposable?.Dispose();
        disposable = null;
    }

    protected void HandleDeath()
    {
        CharacterHealth healthComp = enemyManager.GetHealthComponent();
        healthComp.rCurrentHP.Subscribe(hp =>
        {
            if (hp <= 0)
            {
                fsm.ChangeState(new EnemyDeathState(fsm, enemyManager));
            }
        }).AddTo(disposable);
    }
    
}
