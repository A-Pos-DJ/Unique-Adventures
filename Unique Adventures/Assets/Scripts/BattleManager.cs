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

    bool continueBattle;

    public GameObject playerChampionPrefab;
    public GameObject enemyChampionPrefab;

    List<PlayerChampion> playerCharacters;              //a list of player champions that represent the player
    List<EnemyChampion> enemyCharacters;                //a list of enemy champions that represent the enemies

    ChampionOrder championOrder;                        //a champion order object to help with turn order
    BattleGUI battleGUI;                                //a battle GUI for players to see stats and make actions

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

    // Start is called before the first frame update
    void Start()
    {
        InitManager();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //initalize the battle manager
    void InitManager()
    {
        playerCharacters = new List<PlayerChampion>();
        enemyCharacters = new List<EnemyChampion>();
        championOrder = gameObject.transform.GetChild(0).GetComponent<ChampionOrder>();
        battleGUI = gameObject.transform.GetChild(1).GetComponent<BattleGUI>();

        GeneratePlayer();
        GenerateEnemy();
        InitChampions();
        InitGUI();
        InitBattle();
        StartCoroutine(BattleLoop());
    }

    //creates a player
    void GeneratePlayer()
    {
        GameObject playerChampion = Instantiate(playerChampionPrefab);
        playerCharacters.Add(playerChampion.GetComponent<PlayerChampion>());
    }


    //creates all enemies
    void GenerateEnemy()
    {
        GameObject enemyChampion = Instantiate(enemyChampionPrefab);
        enemyCharacters.Add(enemyChampion.GetComponent<EnemyChampion>());
    }

    //initalize all champions to prepare for battle
    void InitChampions()
    {
        for (int idx = 0; idx < playerCharacters.Count; idx++)
        {
            playerCharacters[idx].Init();
        }

        for (int idx = 0; idx < enemyCharacters.Count; idx++)
        {
            enemyCharacters[idx].Init();
        }
    }

    //initalize the GUI with all of the proper info
    void InitGUI()
    {
        battleGUI.Init(playerCharacters);
        battleGUI.UpdateGUI();
    }

    //initalize the battle before the loop starts
    void InitBattle()
    {
        continueBattle = true;
        championOrder.Init(playerCharacters, enemyCharacters);
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
            battleGUI.SetBattleMessage("It is " + championOrder.currentChampionTurn.stats.name + "'s turn");
            yield return StartCoroutine(championOrder.CurrentChampionAction());
            battleGUI.UpdateGUI();
        }
        yield return null;
    }








}
