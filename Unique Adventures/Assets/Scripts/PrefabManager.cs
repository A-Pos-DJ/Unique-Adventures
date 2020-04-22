using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager _prefabs;

    private void Start()
    {
        _prefabs = this;
    }

    public GameObject actionButtonPrefab;
}
