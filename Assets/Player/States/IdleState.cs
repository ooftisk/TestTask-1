using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{
    public IdleState(FSM fsm, PlayerManager playerManager) : base(fsm, playerManager)
    {
        
    }

    public override void Enter()
    {
        DisposeOnEnter();
        
        var walkTransition = Observable.FromEvent<InputAction.CallbackContext>(
                handler => playerManager.GetMoveAction().performed += handler,
                handler => playerManager.GetMoveAction().performed -= handler)
            .Take(1);
        walkTransition.Subscribe(_ => fsm.ChangeState(new WalkState(fsm, playerManager))).AddTo(disposable);
    }

    public override void Exit()
    {
        DisposeOnExit();
    }
}
