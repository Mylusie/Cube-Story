using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManagerMariageFin : MonoBehaviour
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
    public GameObject jeunehommebis;
    public GameObject bagueF;
     public GameObject king;

    private bool jeunehommeActivated = false;
    private bool bagueFActivated = false;

    void Start()
    {
        StartCoroutine(PlayAudioClipsWithDelay());
    }

    IEnumerator PlayAudioClipsWithDelay()
    {
        int lastIndex = -1; // Index du dernier clip audio
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

                    if (audioSources[i].clip.length > 2)
                    {
                        if(i==0)
                        {
                            // Activer l'animator du GameObject public
                            ActivateGameObject(jeunehommebis);
                            // Activer l'animator du GameObject public
                            desactivateGameObject(jeunehomme);
                            
                            

                            // Activer l'animator du GameObject public
                            ActivateAnimator(king);
                            

                            // Changer la position du GameObject bagueF
                            if (!bagueFActivated)
                            {
                                //bagueF.transform.position = new Vector3(-0.15, 0.23, -0.26); // Changer la position selon vos besoins
                                ActivateAnimator(bagueF); // Relancer l'animation du GameObject
                                bagueFActivated = true;
                            }
                        }
                    }

                    yield return null;
                }

                lastIndex = i;
            }
        }

        if (lastIndex == audioSources.Length - 1 && !string.IsNullOrEmpty(sceneToUnload) && !string.IsNullOrEmpty(sceneToLoad))
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

       void desactivateGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
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
    
    void DesactivateAnimator(GameObject gameObject)
    {
        if (gameObject != null)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }
        }
    }
}

