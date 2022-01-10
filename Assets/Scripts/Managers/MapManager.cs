using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public List<SceneSwitcher> switchers;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Already Instanciated");
        }
    }

    public Vector3 GetSpawn(int _spawnId)
    {
        Transform spawnTransform = switchers[_spawnId].transform.GetChild(0);
        return (spawnTransform.position);
    }
}
