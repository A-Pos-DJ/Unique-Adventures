using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseChampion : MonoBehaviour
{
    public BaseChampion targetChampion;
    BattleActions currentAction;
    public List<BattleActions> actions;

    public bool currentTurn;
    public bool targetConfirmed;
    public bool actionTaken;

    public ChampionStats stats;
    public Sprite body;


    //initalizer for the champion in battle
    public virtual void Init()
    {
        stats = new ChampionStats();
        body = gameObject.transform.GetChild(0).GetComponent<Sprite>();
        actions = new List<BattleActions>();

        gameObject.AddComponent<BattleActions.Attack>();
        gameObject.AddComponent<BattleActions.Defend>();

        actions.Add(GetComponent<BattleActions.Attack>());
        actions.Add(GetComponent<BattleActions.Defend>());
    }

    //attempt an action for this champion
    public virtual void SelectAction(BattleActions action)
    {
        //returns function if it is not this champion's current turn
        if (!currentTurn) return;

        currentAction = action;
        StartCoroutine(action.AttemptAction(this));
    }

    //select a target for a champion
    public virtual void SelectTarget(BaseChampion targetArg)
    {
        //returns function if it is not this champion's current turn
        if (!currentTurn) return;

        targetChampion = targetArg;
        targetConfirmed = true;
    }
}
