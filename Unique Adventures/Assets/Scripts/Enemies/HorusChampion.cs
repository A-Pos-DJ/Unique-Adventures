﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorusChampion : BaseEnemyChampion
{
    //initalizer for the champion in battle
    public override void Init()
    {
        base.Init();

        stats.RandomizeEnemyStats("Horus");
        Debug.Log(stats);
    }

    //Get the enemy agent to decide on an action
    public override void DetermineAction()
    {
        SelectAction(actions[0]);
        SelectTarget(BattleManager._manager.playerCharacters[0]);
    }
}
