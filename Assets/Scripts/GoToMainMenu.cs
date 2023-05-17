using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    public string MainMenu;
    public string PauseMenu;

    void Start()
    {
        StartCoroutine(LoadScenes());
    }

    IEnumerator LoadScenes()
    {
        AsyncOperation asyncLoadScene1 = SceneManager.LoadSceneAsync(MainMenu, LoadSceneMode.Additive);
        AsyncOperation asyncLoadScene2 = SceneManager.LoadSceneAsync(PauseMenu, LoadSceneMode.Additive);

        while (!asyncLoadScene1.isDone  || !asyncLoadScene2.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(MainMenu));
    }
}
