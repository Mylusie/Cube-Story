using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource introAudioSource; // AudioSource pour l'audio d'introduction
    public AudioClip introClip; // Clip audio pour l'introduction
    public AudioSource loopAudioSource; // AudioSource pour l'audio en boucle
    public AudioClip loopClip; // Clip audio pour la boucle

    void Start()
    {
        // Assurez-vous que les AudioSource et les clips audio sont correctement configurés dans l'éditeur Unity

        // Lancer l'audio d'introduction
        introAudioSource.clip = introClip;
        introAudioSource.Play();

        // Lancer la coroutine pour jouer l'audio en boucle après la fin de l'audio d'introduction
        StartCoroutine(PlayLoopAudioAfterIntro());
    }

    IEnumerator PlayLoopAudioAfterIntro()
    {
        // Attendre que l'audio d'introduction soit terminé
        yield return new WaitForSeconds(introClip.length);

        // Configurer et jouer l'audio en boucle
        loopAudioSource.clip = loopClip;
        loopAudioSource.loop = true;
        loopAudioSource.Play();
    }
}
