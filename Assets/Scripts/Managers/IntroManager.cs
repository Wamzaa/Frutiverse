using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    private void Start()
    {
        GameObject mainManager = Instantiate(Resources.Load<GameObject>("MainManager"));
        DontDestroyOnLoad(mainManager);

        StartCoroutine(LaunchMainMenu());
    }

    public IEnumerator LaunchMainMenu()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("MainMenu");
        yield return asyncOp;
    } 
}
