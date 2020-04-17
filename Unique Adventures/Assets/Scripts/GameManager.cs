using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int seed = 12345;             //persistant seed to be used throughout random number generation

    GameObject playerPrefab;                    //player prefab


    GameObject player;                          //the current player

    /*
    // Start is called before the first frame update
    void Start()
    {
        player = GenerateRandomPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject GenerateRandomPlayer()
    {


    }
    */
}
