using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.InputSystem;

public class WalkState : BaseState
{
    public WalkState(FSM fsm, PlayerManager playerManager) : base(fsm, playerManager)
    {
    }

    public override void Enter()
    {
        DisposeOnEnter();
        
        var moveStream = Observable.EveryUpdate(UnityFrameProvider.FixedUpdate)
            .Select(_ => playerManager.GetMoveAction().ReadValue<Vector2>())
            .Where(direction => direction != Vector2.zero);
        moveStream.Subscribe(MovePlayer).AddTo(disposable);
        
        var idleTransition = Observable.FromEvent<InputAction.CallbackContext>(
                handler => playerManager.GetMoveAction().canceled += handler,
                handler => playerManager.GetMoveAction().canceled -= handler)
            .Take(1);
        idleTransition.Subscribe(_ => fsm.ChangeState(new IdleState(fsm, playerManager))).AddTo(disposable);
    }

    public override void Exit()
    {
        DisposeOnExit();
    }

    private void MovePlayer(Vector2 direction)
    {
        direction.Normalize();
        playerManager.GetRigidbody2D().velocity = direction * playerManager.GetMoveSpeed();
    }
}
