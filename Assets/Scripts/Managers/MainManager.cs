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

    [HideInInspector]
    public int currentMoney;

    private bool canMove;

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
        currentMoney = 0;
        canMove = true;
        DontDestroyOnLoad(this);
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool _canMove) 
    {
        canMove = _canMove;
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

    public IEnumerator ChangeSceneToMainMenu()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("MainMenu");
        yield return asyncOp;

        Destroy(player);
        Destroy(uiManager.gameObject);
        Destroy(this.gameObject);
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
        uiManager.GameOver();
        player.SetActive(false);
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        uiManager.UpdateHealthBar();
        player.SetActive(true);
    }

    public void PickUpCoin(int value)
    {
        currentMoney += value;
        uiManager.UpdateMoneyText();
    }
}
