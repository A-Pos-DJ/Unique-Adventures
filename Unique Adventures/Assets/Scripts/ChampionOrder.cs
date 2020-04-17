using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChampionOrder : MonoBehaviour
{
    List<PlayerChampion> playerCharacters;                    //a list of player champions that represent the player
    List<EnemyChampion> enemyCharacters;                      //a list of enemy champions that represent the enemies

    public BaseChampion currentChampionTurn;                  //The current champion that is selecting an action
    List<BaseChampion> turnOrder;                             //The order in which champions can select actions


    //initalizer for this class
    public void Init(List<PlayerChampion> _playerCharacters, List<EnemyChampion> _enemyCharacters)
    {
        playerCharacters = _playerCharacters;
        enemyCharacters = _enemyCharacters;
        turnOrder = new List<BaseChampion>();

        StartCoroutine(NewChampionRound());
    }

    
    //determines the order in which enemies and allies go
    public IEnumerator NewChampionRound()
    {
        //clear the turn order and rebuild
        Debug.Log("Starting a new round of combat");
        turnOrder.Clear();

        //determine which list has the highest number of champions
        int highestCount = playerCharacters.Count >= enemyCharacters.Count ? playerCharacters.Count : enemyCharacters.Count;

        //loop though each champion in both lists to add them both to a single list
        for (int idx = 0; idx < highestCount; idx++)
        {
            if (highestCount <= playerCharacters.Count)
                turnOrder.Add(playerCharacters[idx] as BaseChampion);
            if (highestCount <= enemyCharacters.Count)
                turnOrder.Add(enemyCharacters[idx] as BaseChampion);
        }

        //sort the champions by dexterity and return the list
        turnOrder.OrderBy(champ => champ.stats.dexterity).ToList();

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
        //if it is the not the player's turn... have the enemy calculate an action.. otherwise wait for user input
        if (currentChampionTurn.GetComponent<PlayerChampion>() == null)
            currentChampionTurn.GetTurnAction();
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
        //loop though the turn order
        for (int idx = 0; idx < turnOrder.Count; idx++)
        {
            //find out where in the index the current selected champion is
            if (turnOrder[idx] == currentChampionTurn)
            {
                // if the current champion is not at the end of the turn order...
                if (idx != turnOrder.Count - 1)
                {
                    //set the current champion to the next one on the list and break the loop
                    currentChampionTurn = turnOrder[idx + 1];
                    currentChampionTurn.currentTurn = true;
                    break;
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

    //reset all champions to have taken no action yet
    public IEnumerator ResetActionsTaken()
    {
        for (int idx = 0; idx < turnOrder.Count; idx++)
        {
            turnOrder[idx].actionTaken = false;
        }

        yield return null;
    }

    //a conditional checker to see if everyone has had a turn yet
    bool HasEveryoneHadAnAction()
    {
        if (turnOrder[turnOrder.Count - 1].actionTaken)
            return true;
        else
            return false;
    }

    //a coroutine designed for a keypress to be inputted before continuing with the battle
    IEnumerator WaitForUserInput()
    {
        while (!currentChampionTurn.actionTaken)
            yield return null;
    }

    //a coroutine designed for a keypress to be inputted before continuing with the battle
    IEnumerator WaitForKeyPress(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode))
            yield return null;
    }

}
