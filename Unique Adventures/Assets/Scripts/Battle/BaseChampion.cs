using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseChampion : MonoBehaviour
{
    public BaseChampion targetChampion;
    public BattleActions currentAction;
    public List<BattleActions> actions;

    public bool currentTurn;
    public bool targetConfirmed;
    public bool actionTaken;
    public bool canTakeAction;

    public ChampionStats stats;
    public Sprite body;
    public BoxCollider2D colliderObj;


    //initalizer for the champion in battle
    public virtual void Init()
    {
        stats = new ChampionStats();
        body = gameObject.transform.GetChild(0).GetComponent<Sprite>();
        colliderObj = GetComponent<BoxCollider2D>();
        actions = new List<BattleActions>();
        canTakeAction = true;

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

    //the champion takes damage
    public virtual void TakeDamage(int damageTaken)
    {
        //if the amount of damage taken is less than or equal to 0... return
        if (damageTaken <= 0) { return; }
        //if the amount of damage taken is greater than the amount of HP left.. kill this champion
        if (stats.HPcurrent - damageTaken <= 0)
        {
            stats.HPcurrent = 0;
            Death();
        }
        else
            stats.HPcurrent -= damageTaken;
    }

    //this champion dies
    public virtual void Death()
    {
        BattleGUI._battleGUI.SetBattleMessage(stats.championName + " has been defeated!");
        BattleManager._manager.RemoveFromBattle(this);
        canTakeAction = false;
        gameObject.SetActive(false);
    }

    //when this object is destoyed...
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    //when the button gets clicked...
    void OnMouseDown()
    {
        ChampionOrder.currentChampionTurn.SelectTarget(GetComponent<BaseChampion>());
    }
}
