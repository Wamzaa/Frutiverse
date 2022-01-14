using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject healthPanel;
    public GameObject lifePrefab;

    private int currentHealth;
    private List<GameObject> lifes;

    private void Awake()
    {
        lifes = new List<GameObject>();
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
}
