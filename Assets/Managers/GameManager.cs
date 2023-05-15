using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
        public string MainMenu;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    // Pelin UI palikat
    PauseMenu pauseMenu;

    // Pelin ominaisuudet
    public bool gameIsPaused = false;

    /* ANTIN SYSTEEMI REFERENSSINÄ! -OSSI
     void Awake()

    if (Instance == null)
    {
    DontDestroyOnLoad(gameObject);
    Instance = this;
    }

    else
    {
    Destroy(gameObject);
    }
   
     */

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
      // StartCoroutine( GoToMenu());
        pauseMenu = GetComponentInChildren<PauseMenu>();


        Debug.LogWarning("PELI ALKAA NY!");
        // Peli alkaa

        // Menu

        // Rakenna Squadi

        // Rakenna Mappi
            // Siisti mappi (päälleikäkiset kivet pois)


        // OpenMenu/CloseMenu/EndGame/ChangeScene
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
            
        }
    }

    private void TogglePauseMenu()
    {
        if (gameIsPaused)
        {
            gameIsPaused = false;
            Debug.LogWarning("Resuming game");
            pauseMenu.Resume();
        }
        else
        {
            gameIsPaused = true;
            Debug.LogWarning("Pausing game");
            pauseMenu.Pause();
        }
    }

    IEnumerator GoToMenu()
    {
        AsyncOperation asyncLoadScene1 = SceneManager.LoadSceneAsync(MainMenu, LoadSceneMode.Additive);
      //  SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
      while (!asyncLoadScene1.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(MainMenu));
    }

}