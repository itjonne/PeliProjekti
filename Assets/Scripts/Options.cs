using UnityEngine;
using UnityEngine.UI;
using JSAM;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
 
    public GameObject optionsMenuUI;


    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    private static Options instance;







    // Start is called before the first frame update
    void Start()
    {
        optionsMenuUI.SetActive(false);
   

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
        optionsMenuUI.SetActive(false);
        
        


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


    public GameObject optionsMenuCanvas;

    public void CloseOptionsMenu()
    {
        optionsMenuCanvas.SetActive(false);
        SaveVolumeSettings();
    }


}
