using UnityEngine;

public class SetAudioSourcesTo2D : MonoBehaviour
{
    private void Start()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.spatialBlend = 0f;
        }
    }
}
