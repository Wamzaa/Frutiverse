using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("Player"));
        DontDestroyOnLoad(player);
        GameObject mainManager = Instantiate(Resources.Load<GameObject>("MainManager"));
        DontDestroyOnLoad(mainManager);

        MainManager.Instance.player = player;
        MainManager.Instance.ChangeScene("TestScene", 0);
        
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
