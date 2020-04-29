using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChampion : BaseChampion
{
    //initalizer for the champion in battle
    public void PlayerInit(string characterName)
    {
        base.Init();

        stats.RandomizePlayerStats(characterName);
        Debug.Log(stats);
    }

    public override void TakeDamage(int damageTaken)
    {
        base.TakeDamage(damageTaken);
        BattleGUI._battleGUI.UpdateInfoPanels();
    }
}
