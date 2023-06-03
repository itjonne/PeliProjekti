using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JSAM;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsMenu;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public AudioClip buttonClickSound;

    //Stop all sounds
    private AudioSource[] allAudioSources;

    public void Start()
    {


        StopAllAudio();
 

    // T‰‰ nyt nollaa pelin jos menee main menuun
    Squad squad = FindObjectOfType<Squad>();

        if (squad != null)
        {
            Destroy(squad.gameObject);

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

    public void PlayButtonClickSound()
    {
        JSAM.AudioManager.PlaySound(AudioLibSounds.sfx_Click);
    }

    public void LoadGameScene()
    {
        StopAllAudio();
        Debug.LogWarning("LADATAAN GAME STATE");
        GameManager.Instance.gameHasEnded = false;
        SceneManager.LoadScene("IntroLevel");
    }



    public GameObject optionsMenuCanvas;

    private bool optionsMenuOpen = false;

    public void ToggleOptionsMenu()
    {
        optionsMenuOpen = !optionsMenuOpen;
        optionsMenuCanvas.SetActive(optionsMenuOpen);
    }

    


    public void QuitGame()
    {
        Application.Quit();
    }


}
