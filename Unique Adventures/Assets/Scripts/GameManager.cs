using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _game;
    public static PrefabManager _prefabs;
    public static StringManager _strings;
    public static BattleManager _battle;
    public static BattleBackdropManager _backdrop;
    public static string playerName = null;
    public static int seed = 0;                                     //persistant seed to be used throughout random number generation

    public bool playerNameEntered = false;
    public string nameTextfield = "Insert Name Here";
    public GameObject nameButtonPrefab;

    public List<PlayerChampion> playerCharacters;                               //a list of player champions that represent the players

    GameObject mainCharacter;                                                   //the current player


    // Start is called before the first frame update
    void Start()
    {
        _game = this;
        CreateEnterButton(gameObject.transform.position, "Submit Name");
    }

    //initalize the game manager
    public void Init()
    {
        _prefabs = GetComponent<PrefabManager>();
        _strings = GetComponent<StringManager>();
        _battle = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        _backdrop = GameObject.Find("BattleBackdrop").GetComponent<BattleBackdropManager>();

        seed = SeedGenerator.StringToInt(playerName);
        Debug.Log("Current Seed: " + seed.ToString());

        playerCharacters = new List<PlayerChampion>();
        playerCharacters.Add(PlayerGenerator.CreateNewCharacter(_prefabs.playerChampionPrefab, playerName));
        mainCharacter = playerCharacters[0].gameObject;
        playerCharacters.Add(PlayerGenerator.CreateNewCharacter(_prefabs.playerChampionPrefab));

        _battle.Init();
        _backdrop.Init();
    }

    //spawns a button for the user to enter their name
    private void CreateEnterButton(Vector3 position, string buttonName)
    {
        GameObject newButtonObject = Instantiate(nameButtonPrefab, position, Quaternion.identity);
        NameButton newButton = newButtonObject.GetComponent<NameButton>();

        newButton.Init(buttonName);
    }

    void OnGUI()
    {
        if (!playerNameEntered)
        {
            // Make a text field that modifies stringToEdit.
            
            nameTextfield = GUI.TextField(new Rect((Screen.width/2)-75, (Screen.height/2) - 10, 150, 20), nameTextfield, 25);
        }
    }





}
