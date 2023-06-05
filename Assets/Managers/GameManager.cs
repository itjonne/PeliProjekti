using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private string MainMenu = "MainMenu";

    // Pelin ominaisuudet
    public bool gameIsPaused = false;
    public int enemiesKilled = 0;
    public bool captainKilled = false;

    public bool gameHasEnded = false;
    public bool levelFinished = false;

    public GameObject gameOverScreen;
    public GameObject levelEndScreen;

    public LevelEnd levelEnd;

    public bool debug = false;

    private AudioSource[] allAudioSources;

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

    /*
    public void LevelEnd()
    {
        PlayableCharacter character = FindObjectOfType<PlayableCharacter>(); 
         {
            
            Time.timeScale = 0;   
         }      
    }
    */

    // Tekee levelendin käytettäväks kun kapteeni kuolee
    public void KillCaptain()
    {
        captainKilled = true;

        Debug.LogWarning("CAPTAIN KILLED");

        // Joutu tekemään tälläsen mutkan jos haluaa endin olevan piilotettu kentän alussa, tätä itää säätää
        //GameObject levelEnd = GameObject.FindGameObjectWithTag("EndObject");
        //levelEnd.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
        // TÄHÄN VOI TYÖNTÄÄ MITÄ TAHANSA
        // Peli Loppuu / loppupalikkaa rakentuu tms
    }
    public void KillEnemy(int amount)
    {
        enemiesKilled += amount;
        //  Debug.LogWarning("KILLED ENEMY");
        // Debug.LogWarning(enemiesKilled);
        SpawnEndKills spawner = FindObjectOfType<SpawnEndKills>();
        if (spawner != null)
        {
            spawner.enemiesKilled += 1;
        }

    }
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // StartCoroutine( GoToMenu());
        pauseMenu = GetComponentInChildren<PauseMenu>();

        levelEndScreen = GameObject.Find("LevelFinishScreen");
        levelEndScreen.SetActive(false);

        gameOverScreen = GameObject.Find("GameOverScreen");
        gameOverScreen.SetActive(false);

        levelEnd = GetComponent<LevelEnd>();
        debug = false;

        

        Debug.LogWarning("PELI ALKAA NY!");
        // Peli alkaa

        if (SceneManager.GetActiveScene().name == "Loading")
        {
            StartCoroutine(GoToMenu());
        }

        // Menu

        // Rakenna Squadi

        // Rakenna Mappi
        // Siisti mappi (päälleikäkiset kivet pois)


        // OpenMenu/CloseMenu/EndGame/ChangeScene
    }

    private void Update()
    {

        //DEBUGGAUS-KOMENTOJA TESTAUSTA VARTEN NUMEROILLA PÄÄSEE HALUAMAAN KENTTÄÄN
        if (Input.GetKeyDown(KeyCode.P))
        {
            debug = true;
        }


            if (Input.GetKeyDown(KeyCode.Alpha1) && debug == true)
            {
                // SceneManager.LoadScene(SceneManager.GetSceneByName("Level1_ALT"));
                SceneManager.LoadScene("Level1_ALT");

            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && debug == true)
            {
                // SceneManager.LoadScene(SceneManager.GetSceneByName("Level1_ALT"));
                SceneManager.LoadScene("Ossi_Level2");

            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && debug == true)
            {
                // SceneManager.LoadScene(SceneManager.GetSceneByName("Level1_ALT"));
                SceneManager.LoadScene("Ossi_Level3");

            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && debug == true)
            {
                // SceneManager.LoadScene(SceneManager.GetSceneByName("Level1_ALT"));
                SceneManager.LoadScene("Seppo_Level4");

            }

            if (Input.GetKeyDown(KeyCode.Alpha5) && debug == true)
            {
                // SceneManager.LoadScene(SceneManager.GetSceneByName("Level1_ALT"));
                SceneManager.LoadScene("Ossi_FIN");

            }


            if (Input.GetKeyDown(KeyCode.Alpha6) && debug == true)
            {
                // SceneManager.LoadScene(SceneManager.GetSceneByName("Level1_ALT"));
                SceneManager.LoadScene("TESTLEVEL");

            }
            


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();

        }

        
        if (gameHasEnded == true && Input.GetKeyDown(KeyCode.Space))
        {
            //Time.timeScale = 1;
            
            gameOverScreen.SetActive(false);
            StartCoroutine(GoToMenu());
        }


        if (levelFinished == true) 
        {
            levelEndScreen.SetActive(true);
            Time.timeScale = 0;

   

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllAudio();
                var levelEnd = FindObjectOfType<LevelEnd>();
                SceneManager.LoadScene(levelEnd.GetComponent<LevelEnd>().nextLevel);

                levelFinished = false;               
                Time.timeScale = 1;
                levelEndScreen.SetActive(false);
            }

        }

        

    }

    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
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

    public void GameOver() //KOITETAAN KYHÄTÄ VÄHÄN HIENOMPI GAMEOVER - OSSI
    {
        //Time.timeScale = 0;
        gameHasEnded = true;
        gameOverScreen.SetActive(true);

    }

    IEnumerator GoToMenu()
    {
        Debug.LogWarning("MAINMENU , "+ MainMenu);
      AsyncOperation asyncLoadScene1 = SceneManager.LoadSceneAsync(MainMenu, LoadSceneMode.Additive);
      //  SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
      while (!asyncLoadScene1.isDone)
        {
            yield return null;
        }

        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(MainMenu));
    }

}