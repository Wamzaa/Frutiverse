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

    // Gameplay variables
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int currentMoney;

    // Input values
    private bool canMove;
    private int currentController;

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
        currentController = 0;
    }

    // Getter - Setter

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool _canMove) 
    {
        canMove = _canMove;
    }

    public void SetCurrentController(int newController)
    {
        currentController = newController;
    }

    // Input Callbacks

    public void Jump()
    {
        player.GetComponent<PlayerMovement>().JumpFromInput();
    }

    public void Attack()
    {
        player.GetComponent<PlayerInteraction>().AttackFromInput();
    }

    public void Interact()
    {
        player.GetComponent<PlayerInteraction>().InteractFromInput();
    }

    public float GetMove()
    {
        ControlsManager controlsManager = this.gameObject.GetComponent<ControlsManager>();
        return (controlsManager.GetMove());
    }

    public void Move(float _movementInput)
    {
        float horizontalMovement = 0.0f;
        if (GetCanMove())
        {
            horizontalMovement = _movementInput;
        }
        PlayerMovement.Instance.SetHorizontalMovement(horizontalMovement);
    }

    public void OpenInventory()
    {
        uiManager.OpenWindowFromInputs(1);
    }

    public void OpenMenu()
    {
        uiManager.OpenWindowFromInputs(0);
    }

    public void ResetInput()
    {

    }

    // Scene Management

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
        player = null;
        Destroy(uiManager.gameObject);
        uiManager = null;
    }

    // Gameplay Callbacks

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
