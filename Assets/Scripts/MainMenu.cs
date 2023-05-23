using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        // T‰‰ nyt nollaa pelin jos menee main menuun
        Squad squad = FindObjectOfType<Squad>();

        if (squad != null)
        {
            Destroy(squad.gameObject);

        }
  
    }

    public void LoadGameScene()
    {
        Debug.LogWarning("LADATAAN GAME STATE");
        GameManager.Instance.gameHasEnded = false;
        SceneManager.LoadScene("IntroLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
