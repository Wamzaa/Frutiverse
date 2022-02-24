using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        GameObject controlsDictionaryObject = new GameObject("ControlsDictionary");
        ControlsDictionary controlsDictionary = controlsDictionaryObject.AddComponent<ControlsDictionary>();
        controlsDictionary.Init();
        DontDestroyOnLoad(controlsDictionaryObject);
    }

    public void Play()
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("Player"));
        DontDestroyOnLoad(player);
        GameObject mainManager = Instantiate(Resources.Load<GameObject>("MainManager"));
        DontDestroyOnLoad(mainManager);
        GameObject uiManager = Instantiate(Resources.Load<GameObject>("UIManager"));
        DontDestroyOnLoad(uiManager);

        MainManager.Instance.player = player;
        MainManager.Instance.uiManager = uiManager.GetComponent<UIManager>();
        uiManager.GetComponent<UIManager>().InitHealthBar();

        MainManager.Instance.ChangeScene("TestScene", 0);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
