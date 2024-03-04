using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class AudioManager : MonoBehaviour
{
    public AudioSource introAudioSource; // Source audio pour l'audio d'introduction
    public AudioClip introClip; // Clip audio d'introduction

    void Start()
    {
        // Commence par charger la scène
        StartCoroutine(LoadSceneWithDelay());
    }

    IEnumerator LoadSceneWithDelay()
    {
        // Attendre 10 secondes
        yield return new WaitForSeconds(3);

        // Jouer l'audio d'introduction
        introAudioSource.clip = introClip;
        introAudioSource.Play();

    }
}
