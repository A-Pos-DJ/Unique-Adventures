using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _game;
    public static string name = "DJ?";
    public static int seed = SeedGenerator.StringToInt(name);             //persistant seed to be used throughout random number generation


    GameObject playerPrefab;                    //player prefab
    GameObject player;                          //the current player


    // Start is called before the first frame update
    void Start()
    {
        _game = this;
        Debug.Log("Current Seed: " + seed.ToString());
        //player = GenerateRandomPlayer();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject GenerateRandomPlayer()
    {


    }
    */
}
