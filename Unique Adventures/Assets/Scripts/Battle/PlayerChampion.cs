using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChampion : BaseChampion
{
    //initalizer for the champion in battle
    public override void Init()
    {
        base.Init();

        stats.RandomizePlayerStats();
        Debug.Log(stats);
    }
}
