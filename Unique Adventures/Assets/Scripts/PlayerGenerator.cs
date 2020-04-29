using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//generates players to be
public class PlayerGenerator : MonoBehaviour
{
    //create a new character with no name input
    public static PlayerChampion CreateNewCharacter(GameObject playerPrefab)
    {
        Vector3 spawnPosition = new Vector3(0f, 0f, -10f);
        GameObject playerChampion = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        playerChampion.GetComponent<PlayerChampion>().PlayerInit(GetUniqueName());
        return playerChampion.GetComponent<PlayerChampion>();
    }

    //create a new character with name input (main character)
    public static PlayerChampion CreateNewCharacter(GameObject playerPrefab, string championNameArg)
    {
        Vector3 spawnPosition = new Vector3(0f, 0f, -10f);
        GameObject playerChampion = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        playerChampion.GetComponent<PlayerChampion>().PlayerInit(championNameArg);
        return playerChampion.GetComponent<PlayerChampion>();
    }

    //ensures that a name does not get picked twice
    public static string GetUniqueName()
    {
        string nameChosen = GameManager._strings.randomPlayerName;
        bool nameFound = false;
        List<PlayerChampion> currentPlayers = GameManager._game.playerCharacters;


        //loop though the player list and see if the name already exists
        while (!nameFound)
        {
            //unique name flag set to be true until proven otherwise
            bool uniqueName = true;
            //loop though the current players and see if this name is taken
            for (int idx = 0; idx < currentPlayers.Count; idx++)
            {
                //if the name matches one that is already being used.. set unique name to false, else true
                uniqueName = currentPlayers[idx].stats.championName == nameChosen ? false : true;
            }

            //if a unique name was not found... get another random one and start loop again.. otherwise set the nameFound flag true
            if (!uniqueName) { nameChosen = GameManager._strings.randomPlayerName; }
            else nameFound = true;
        }

        //return the unique name
        return nameChosen;
    }

}
