using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject playerChampionPrefab;

    public GameObject enemyChampionPrefab;
    public GameObject horusChampionPrefab;
    public GameObject iskanderChampionPrefab;
    public GameObject drakeChampionPrefab;

    public GameObject actionButtonPrefab;




    public GameObject randomEnemyPrefab
    {
        get
        {
            List<GameObject> enemyPrefabList = new List<GameObject>();

            enemyPrefabList.Add(horusChampionPrefab);
            enemyPrefabList.Add(iskanderChampionPrefab);
            enemyPrefabList.Add(drakeChampionPrefab);

            return enemyPrefabList[Randomizer.RandomInt(0, enemyPrefabList.Count)];
        }
    }



}
