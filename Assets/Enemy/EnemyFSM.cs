using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
   [HideInInspector] public EnemyBaseState currentState;
   private EnemyManager _enemyManager;

   private void Start()
   {
      _enemyManager = GetComponent<EnemyManager>();
      ChangeState(new EnemyIdleState(this, _enemyManager));
   }
   
   public void ChangeState(EnemyBaseState newState)
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
