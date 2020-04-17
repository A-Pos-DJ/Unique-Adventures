using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChampion : BaseChampion
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //initalizer for the champion in battle
    public override void Init()
    {
        base.Init();

        stats.RandomizeEnemyStats();
        Debug.Log(stats);
    }

    //Gets the enemy agent's descision on an action for this champion
    public override void GetTurnAction()
    {

        Debug.Log("An action has been taken for " + stats.name);
        actionTaken = true;
    }

    //attempt an action
    public override void AttemptAction(Action actionType)
    {

    }


}
