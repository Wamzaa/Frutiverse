using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public GameObject player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Already Instanciated");
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void ChangeScene(string sceneName, int spawnId)
    {
        StartCoroutine(ChangeSceneCoroutine(sceneName, spawnId));
    }

    IEnumerator ChangeSceneCoroutine(string sceneName, int spawnId)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        yield return asyncOp;

        player.transform.position = MapManager.Instance.GetSpawn(spawnId);

    }
}
