using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject healthPanel;
    public GameObject lifePrefab;
    public GameObject menuUI;
    public GameObject inventoryUI;
    public GameObject gameOverUI;

    private int currentHealth;
    private List<GameObject> lifes;

    private bool menuVisible;
    private bool inventoryVisible;
    private bool gameOverVisible;

    private void Awake()
    {
        lifes = new List<GameObject>();
        menuVisible = false;
        inventoryVisible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(ControlsDictionary.Instance.inventoryButtonKey))
        {
            OpenInventory(!inventoryVisible);
        }
        if (Input.GetKeyDown(ControlsDictionary.Instance.menuButtonKey))
        {
            OpenMenu(!menuVisible);
        }
    }

    public void InitHealthBar()
    {
        for(int i=0; i<MainManager.Instance.maxHealth; i++)
        {
            GameObject life = Instantiate(lifePrefab, healthPanel.transform) ;
            lifes.Add(life);
        }
    }

    public void UpdateHealthBar()
    {
        currentHealth = MainManager.Instance.currentHealth;
        for(int i = 0; i<currentHealth; i++)
        {
            lifes[i].transform.GetChild(1).gameObject.SetActive(true);
        }
        for (int j = currentHealth; j < MainManager.Instance.maxHealth; j++)
        {
            lifes[j].transform.GetChild(1).gameObject.SetActive(false);
        }

    }

    public void OpenInventory(bool open)
    {
        if (open && !gameOverVisible)
        {
            menuVisible = false;
            menuUI.SetActive(false);
        }
        
        inventoryVisible = open;
        inventoryUI.SetActive(open);
    }

    public void OpenMenu(bool open)
    {
        if (open && !gameOverVisible)
        {
            inventoryVisible = false;
            inventoryUI.SetActive(false);
        }

        menuVisible = open;
        menuUI.SetActive(open);
    }

    public void GameOver()
    {
        inventoryVisible = false;
        menuVisible = false;
        inventoryUI.SetActive(false);
        menuUI.SetActive(false);
        gameOverVisible = true;
        gameOverUI.SetActive(true);
    }

    public void Respawn()
    {
        gameOverVisible = false;
        gameOverUI.SetActive(false);
        MainManager.Instance.Respawn();
    }

    public void GoBackToMainMenu()
    {
        StartCoroutine(MainManager.Instance.ChangeSceneToMainMenu());
    }
}
