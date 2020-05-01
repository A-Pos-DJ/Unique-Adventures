using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                                                                //
    //                                         Variables                                              //
    //                                                                                                //
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static BattleManager _manager;

    int maxEnemyWidthX = 12;         //the maximum X value that can be applied to an enemy to still see them on screen

    public bool continueBattle;

    public List<PlayerChampion> playerCharacters;              //a list of player champions that represent the player
    public List<BaseEnemyChampion> enemyCharacters;            //a list of enemy champions that represent the enemies

    ChampionOrder championOrder;                               //a champion order object to help with turn order
    BattleGUI battleGUI;                                       //a battle GUI for players to see stats and make actions


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                                                                //
    //                                          Methods                                               //
    //                                                                                                //
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                                                                //
    //                                       Init  Methods                                            //
    //                                                                                                //
    ////////////////////////////////////////////////////////////////////////////////////////////////////


    //initalize the battle manager
    public void Init()
    {
        _manager = this;
        playerCharacters = GameManager._game.playerCharacters;
        enemyCharacters = new List<BaseEnemyChampion>();
        championOrder = gameObject.transform.GetChild(0).GetComponent<ChampionOrder>();
        battleGUI = gameObject.transform.GetChild(1).GetComponent<BattleGUI>();

        GenerateRandomEnemy();
        GenerateRandomEnemy();
        GenerateRandomEnemy();

        InitEnemyChampions();
        InitGUI();
        InitBattle();
        StartCoroutine(BattleLoop());
    }

    //create a random enemy
    void GenerateRandomEnemy()
    {
        Vector3 spawnPosition = new Vector3(0f, 0.0f, 5f);
        GameObject enemyChampion = Instantiate(GameManager._prefabs.randomEnemyPrefab, spawnPosition, Quaternion.identity);
        enemyCharacters.Add(enemyChampion.GetComponent<BaseEnemyChampion>());
    }

    //initalize all champions to prepare for battle
    void InitEnemyChampions()
    {
        for (int idx = 0; idx < enemyCharacters.Count; idx++)
        {
            enemyCharacters[idx].Init();
            enemyCharacters[idx].stats.championName += " " + (idx + 1);
        }
    }

    //initalize the GUI with all of the proper info
    void InitGUI()
    {
        battleGUI.Init(playerCharacters);
        battleGUI.UpdateGUIForNextTurn();
    }

    //initalize the battle before the loop starts
    void InitBattle()
    {
        ArrangeEnemyChampions();
        continueBattle = true;
        championOrder.Init();
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                                                                //
    //                                     Turn/Action  Methods                                       //
    //                                                                                                //
    ////////////////////////////////////////////////////////////////////////////////////////////////////


    //starts the battle
    IEnumerator BattleLoop()
    {
        while (continueBattle)
        {
            battleGUI.SetBattleMessage("It is " + ChampionOrder.currentChampionTurn.stats.championName + "'s turn");
            battleGUI.UpdateActionsAvailable(ChampionOrder.currentChampionTurn);
            yield return StartCoroutine(championOrder.CurrentChampionAction());
            battleGUI.UpdateGUIForNextTurn();
        }

        EndBattle();

        yield return null;
    }

    //removes a defeated player or enemy from the battle
    public void RemoveFromBattle(BaseChampion removedChampion)
    {
        if (removedChampion.GetComponent<PlayerChampion>() != null)
            playerCharacters.Remove(removedChampion.GetComponent<PlayerChampion>());
        else if (removedChampion.GetComponent<BaseEnemyChampion>() != null)
            enemyCharacters.Remove(removedChampion.GetComponent<BaseEnemyChampion>());

        ShouldBattleContinue();
    }

    //check to see if a battle should continue
    void ShouldBattleContinue()
    {
        if (playerCharacters.Count == 0 || enemyCharacters.Count == 0) { continueBattle = false; }
    }

    //ends the battle with a message
    void EndBattle()
    {
        if (enemyCharacters.Count == 0)
        {
            battleGUI.SetBattleMessage("The players are victorious!!!");
        }
        else if (playerCharacters.Count == 0)
        {
            battleGUI.SetBattleMessage("The players have been defeated!");
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //                                                                                                //
    //                                         Misc  Methods                                          //
    //                                                                                                //
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    //arranges the enemy champions on screen to not overlap
    void ArrangeEnemyChampions()
    {
        //if there are 1 or fewer enemies... no adjustements are needed
        if (enemyCharacters.Count <= 1) { return; }

        //set the amount of space between enemies
        float enemySpacing = maxEnemyWidthX * 2 / (enemyCharacters.Count -1);

        //loop though the enemies to adjust their X values
        for (int idx = 0; idx < enemyCharacters.Count; idx++)
        {
            Vector3 newPosition = new Vector3(0, 0, 0);

            //if there is an even amount of enemies...
            if (enemyCharacters.Count % 2 == 0)
            {
                //if we are left of the center
                if (idx + 1 <= enemyCharacters.Count / 2)
                {
                    //adjust the X position according to the left edge inward
                    newPosition.x = -maxEnemyWidthX + (idx * enemySpacing);
                }
                //if we are to the right of the center
                else
                {
                    //adjust the X position acording to the right edge inward 
                    newPosition.x = maxEnemyWidthX + ((idx - (enemyCharacters.Count -1)) * enemySpacing);
                }
            }
            //if there are an odd amount of enemies...
            else
            {
                //if we are left of the center
                if (idx + 1 < enemyCharacters.Count / 2)
                {
                    //adjust the X position according to the left edge inward
                    newPosition.x = -maxEnemyWidthX + (idx * enemySpacing);
                }
                //if we are in the center
                else if (idx + 1 == (enemyCharacters.Count / 2) + 1)
                {
                    //do nothing with the X value
                    newPosition.x = 0;
                }
                //if we are to the right of the center
                else
                {
                    //adjust the X position acording to the right edge inward 
                    newPosition.x = maxEnemyWidthX + ((idx - (enemyCharacters.Count - 1)) * enemySpacing);
                }
            }
            //move the enemy to its proper destination 
            enemyCharacters[idx].gameObject.transform.Translate(newPosition);
        }
    }

    public static GameObject CreateBattleObject(GameObject prefabArg, Vector3 positionArg, Quaternion rotationArg)
    {
        return Instantiate(prefabArg, positionArg, rotationArg);
    }

    public static void DestroyBattleObject(GameObject gameObjectArg)
    {
        Destroy(gameObjectArg.gameObject);
    }


}
