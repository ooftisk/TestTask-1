using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public abstract class BaseState
{
    protected FSM fsm;
    protected PlayerManager playerManager;
    protected CompositeDisposable disposable;

    public BaseState(FSM fsm, PlayerManager playerManager)
    {
        this.fsm = fsm;
        this.playerManager = playerManager;
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
}
