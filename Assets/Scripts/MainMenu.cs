using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void LoadGameScene()
    {
        SceneManager.LoadScene("Ossi_Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
