using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public string nextScene;
    public int spawnId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainManager.Instance.ChangeScene(nextScene, spawnId);
    }
}
