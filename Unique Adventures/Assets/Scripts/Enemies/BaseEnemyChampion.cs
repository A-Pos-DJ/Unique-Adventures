using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyChampion : BaseChampion
{
    //initalizer for the champion in battle
    public virtual void EnemyInit(string enemyName)
    {
        base.Init();

        stats.RandomizeEnemyStats(enemyName);
        Debug.Log(stats);
    }

    //Get the enemy agent to decide on an action
    public virtual void DetermineAction()
    {
        SelectAction(actions[0]);
        SelectTarget(BattleManager._manager.playerCharacters[0]);
    }
}
