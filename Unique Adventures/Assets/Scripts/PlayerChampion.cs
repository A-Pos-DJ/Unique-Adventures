using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChampion : BaseChampion
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

        stats.RandomizePlayerStats();
        Debug.Log(stats);
    }


    //attempt an action
    public override void AttemptAction(Action actionType)
    {
        //if it is currently this champion's turn...
        if (currentTurn)
        {
            //attempt an action...
            switch (actionType)
            {
                case Action.Attack:
                    //insert attacking code here
                    Debug.Log(stats.name + " attacks!");
                    actionTaken = true;
                    return;
                case Action.Defend:
                    //inser attacking code here
                    actionTaken = true;
                    return;
                default:
                    Debug.Log("ERROR: unable to determine which action is being attempted");
                    actionTaken = true;
                    return;
            }
        }
    }

}
