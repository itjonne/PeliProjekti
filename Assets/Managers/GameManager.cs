using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

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



    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        pauseMenu = GetComponentInChildren<PauseMenu>();


        Debug.LogWarning("PELI ALKAA NY!");
        // Peli alkaa

        // Menu

        // Rakenna Squadi

        // Rakenna Mappi
            // Siisti mappi (p‰‰lleik‰kiset kivet pois)


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
}