using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Attack,
    Defend
};

public class BaseChampion : MonoBehaviour
{
    public bool currentTurn;
    public bool actionTaken;

    public ChampionStats stats;
    public Sprite body;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //initalizer for the champion in battle
    public virtual void Init()
    {
        stats = new ChampionStats();
        body = gameObject.transform.GetChild(0).GetComponent<Sprite>();
    }

    //asks the user/agent for the action the champion wants to take
    public virtual void GetTurnAction() { }
    public virtual void AttemptAction(Action actionType) { }
}
