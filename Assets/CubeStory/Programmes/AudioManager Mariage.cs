using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManagerMariage : MonoBehaviour
{
    public AudioSource[] audioSources; // Tableau des sources audio
    public AudioClip[] audioClips; // Tableau des clips audio
    public List<int> knightAudioIndices; // Indices des clips audio du chevalier
    public List<int> fatherAudioIndices; // Indices des clips audio du père
    public Material knightMaterial; // Matériau pour le chevalier
    public Material fatherMaterial; // Matériau pour le père
    public Material originalKnightMaterial; // Matériau d'origine pour le chevalier
    public Material originalFatherMaterial; // Matériau d'origine pour le père
    public string sceneToLoad; // Nom de la scène à charger
    public string sceneToUnload; // Nom de la scène à décharger
    public GameObject jeunehomme;
    public GameObject bagueF;

    private bool jeunehommeActivated = false;
    private bool bagueFActivated = false;

    void Start()
    {
        StartCoroutine(PlayAudioClipsWithDelay());
    }

    IEnumerator PlayAudioClipsWithDelay()
    {
        for (int i = 0; i < audioSources.Length && i < audioClips.Length; i++)
        {
            if (audioSources[i] != null && audioClips[i] != null)
            {
                audioSources[i].volume = 1f;
                audioSources[i].clip = audioClips[i];
                audioSources[i].Play();

                while (audioSources[i].isPlaying)
                {
                    UpdateCharacterMaterials(i);

                    if (i == 10 && !jeunehommeActivated)
                    {
                        ActivateGameObject(jeunehomme);
                        jeunehommeActivated = true;
                    }

                    if (i == 12 && !bagueFActivated)
                    {
                        yield return new WaitForSeconds(10);
                        ActivateAnimator(bagueF);
                        bagueFActivated = true;
                    }

                    yield return null;
                }
            }
        }

        if (!string.IsNullOrEmpty(sceneToUnload) && !string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
            SceneManager.UnloadSceneAsync(sceneToUnload);
        }
    }

    void UpdateCharacterMaterials(int index)
    {
        SetCharacterMaterial(GameObject.FindWithTag("Knight").GetComponent<Renderer>(), knightMaterial, originalKnightMaterial, knightAudioIndices, index);
        SetCharacterMaterial(GameObject.FindWithTag("Father").GetComponent<Renderer>(), fatherMaterial, originalFatherMaterial, fatherAudioIndices, index);
    }

    void SetCharacterMaterial(Renderer characterRenderer, Material newMaterial, Material originalMaterial, List<int> audioIndices, int index)
    {
        if (audioIndices.Contains(index))
        {
            characterRenderer.material = newMaterial;
        }
        else
        {
            characterRenderer.material = originalMaterial;
        }
    }

    void ActivateGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
        }
    }

    void ActivateAnimator(GameObject gameObject)
    {
        if (gameObject != null)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true;
            }
        }
    }
}