using UnityEngine;
using UnityEngine.UI;
using JSAM;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;


    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    private static PauseMenu instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;

        LoadVolumeSettings();

        // Add listeners to the volume sliders
        sfxVolumeSlider.onValueChanged.AddListener(delegate { SaveVolumeSettings(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { SaveVolumeSettings(); });
    
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
      



    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;



    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.SetSoundVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.SetMusicVolume(volume);
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    // Load the volume slider values
    public void LoadVolumeSettings()
    {
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game
    }


    public void ReturnToMainMenu()
    {
        SaveVolumeSettings();
        Resume();
        SceneManager.LoadScene("MainMenu");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()); // Unload the current scene
        
    }

}
