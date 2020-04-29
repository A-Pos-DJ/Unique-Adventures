using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChampionOrder : MonoBehaviour
{
    List<PlayerChampion> playerCharacters { get { return BattleManager._manager.playerCharacters; } }
    List<BaseEnemyChampion> enemyCharacters { get { return BattleManager._manager.enemyCharacters; } }

    public static BaseChampion currentChampionTurn;           //The current champion that is selecting an action
    List<BaseChampion> turnOrder;                             //The order in which champions can select actions

    bool continueBattle {get { return BattleManager._manager.continueBattle; }}

    //initalizer for this class
    public void Init()
    {
        turnOrder = new List<BaseChampion>();

        StartCoroutine(NewChampionRound());
    }

    //determines the order in which enemies and allies go
    public IEnumerator NewChampionRound()
    {
        //if the battle is no longer contiuing... return
        if (!continueBattle) { yield return null; }

        //clear the turn order and rebuild
        turnOrder.Clear();

        //determine which list has the highest number of champions
        int highestCount = playerCharacters.Count >= enemyCharacters.Count ? playerCharacters.Count : enemyCharacters.Count;

        //loop though each champion in both lists to add them both to a single list
        for (int idx = 0; idx <= highestCount - 1; idx++)
        {
            if (idx <= playerCharacters.Count - 1)
                turnOrder.Add(playerCharacters[idx] as BaseChampion);
            if (idx <= enemyCharacters.Count - 1)
                turnOrder.Add(enemyCharacters[idx] as BaseChampion);
        }

        //sort the champions by dexterity and return the list
        List<BaseChampion> sortedOrder = turnOrder.OrderByDescending(champ => champ.stats.dexterity).ToList();
        turnOrder = sortedOrder;

        //reset everyone's actions
        StartCoroutine(ResetActionsTaken());
        
        //set the current champion's turn to the top of the list
        currentChampionTurn = turnOrder[0];
        currentChampionTurn.currentTurn = true;
        yield return null;
    }

    //gets an action from the current champion and then moves on to the next turn order
    public IEnumerator CurrentChampionAction()
    {
        //if the battle is no longer contiuing... return
        if (!continueBattle) { yield return null; }

        //if it is the not the player's turn... have the enemy calculate an action.. otherwise wait for user input
        if (currentChampionTurn.GetComponent<BaseEnemyChampion>() != null)
            currentChampionTurn.GetComponent<BaseEnemyChampion>().DetermineAction();
        //wait for the champion's action to finish before continuing
        while (!currentChampionTurn.actionTaken) { yield return null; }
        currentChampionTurn.currentTurn = false;
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(NewChampionTurn()); //setup for the next champion's turn
        yield return null;
    }

    //puts the next champion in the rotation, starts a new round if the last champion in the round has gone
    public IEnumerator NewChampionTurn()
    {
        //if the battle is no longer contiuing... return
        if (!continueBattle) { yield return null; }

        //loop though the turn order
        for (int idx = 0; idx < turnOrder.Count; idx++)
        {
            //if the current index is greater than or equal to the current champion index
            if (idx >= GetCurrentChampionIndex())
            {
                // if the current champion is not at the end of the turn order...
                if (idx != turnOrder.Count - 1)
                {
                    //if the next champion can take an action
                    if (turnOrder[idx + 1].canTakeAction)
                    {
                        //set the current champion to the next one on the list and break the loop
                        currentChampionTurn = turnOrder[idx + 1];
                        currentChampionTurn.currentTurn = true;
                        break;
                    }
                }
                //if the last champion in the turn order is the selected current champion
                else if (idx == turnOrder.Count - 1)
                {
                    StartCoroutine(NewChampionRound());
                }
                else
                    Debug.Log("ERROR: cannot determine which champion goes next");
            }
        }
        yield return null;
    }

    //get the current champion index
    public int GetCurrentChampionIndex()
    {
        //loop though the turn order
        for (int idx = 0; idx < turnOrder.Count; idx++)
        {
            //find out where in the index the current selected champion is
            if (turnOrder[idx] == currentChampionTurn)
            {
                return idx;
            }
        }

        return -1;
    }

    //reset all champions to have taken no action yet
    public IEnumerator ResetActionsTaken()
    {
        //if the battle is no longer contiuing... return
        if (!continueBattle) { yield return null; }

        for (int idx = 0; idx < turnOrder.Count; idx++)
        {
            turnOrder[idx].actionTaken = false;
        }

        yield return null;
    }

    //a coroutine designed for a keypress to be inputted before continuing with the battle
    IEnumerator WaitForUserInput()
    {
        //if the battle is no longer contiuing... return
        if (!continueBattle) { yield return null; }

        while (!currentChampionTurn.actionTaken)
            yield return null;
    }

    //a coroutine designed for a keypress to be inputted before continuing with the battle
    IEnumerator WaitForKeyPress(KeyCode keyCode)
    {
        //if the battle is no longer contiuing... return
        if (!continueBattle) { yield return null; }

        while (!Input.GetKeyDown(keyCode))
            yield return null;
    }

}
