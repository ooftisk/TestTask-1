using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;
using Object = System.Object;
using Random = UnityEngine.Random;


public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyFSM fsm, EnemyManager enemyManager) : base(fsm, enemyManager)
    {
        
    }

    
    public override void Enter()
    {
        DisposeOnEnter();
        
        enemyManager.GetRB().velocity = Vector2.zero;
        
        DropItem();
        GameObject.Destroy(enemyManager.gameObject, 0.1f);
    }

    public override void Exit()
    {
        DisposeOnExit();
    }
    private void DropItem()
    {
        GameObject[] randomDropArray = enemyManager.GetDropLoot();
        GameObject randomDrop = randomDropArray[Random.Range(0, enemyManager.GetDropLoot().Length)]; 
        GameObject dropLoot = GameObject.Instantiate(randomDrop, enemyManager.transform.position, enemyManager.transform.rotation);
    }
}
