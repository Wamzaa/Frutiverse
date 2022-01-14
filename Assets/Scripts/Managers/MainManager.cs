using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public GameObject player;
    public UIManager uiManager;
    public int maxHealth;

    [HideInInspector]
    public int currentHealth;

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
        currentHealth = maxHealth;
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
        Camera.main.GetComponent<FollowCamera>().player = MainManager.Instance.player;

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            GameOver();
        }
        uiManager.UpdateHealthBar();
    }

    public void GameOver()
    {

    }
}