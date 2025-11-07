using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    [HideInInspector] public BaseState currentState;
    private PlayerManager _playerManager;

    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
        ChangeState(new IdleState(this, _playerManager));
    }
    
    public void ChangeState(BaseState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        
        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter();
        }
    }
}
