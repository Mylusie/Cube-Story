using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManagerVente : MonoBehaviour
{
    public AudioSource[] audioSources; // Tableau des sources audio
    public AudioClip[] audioClips; // Tableau des clips audio
    public List<int> knightAudioIndices; // Indices des clips audio du chevalier
    public List<int> fatherAudioIndices; // Indices des clips audio du père
    public Material knightMaterial; // Matériau pour le chevalier
    public Material fatherMaterial; // Matériau pour le père
    public Material originalKnightMaterial; // Matériau d'origine pour le chevalier
    public Material originalFatherMaterial; // Matériau d'origine pour le père
    public GameObject bourseObject; // GameObject de la bourse
    public string sceneToLoad; // Nom de la scène à charger
    public string sceneToUnLoad; // Nom de la scène à charger

    private Dictionary<int, bool> knightAudioPlaying; // Dictionnaire pour suivre les clips audio du chevalier en cours de lecture
    private Dictionary<int, bool> fatherAudioPlaying; // Dictionnaire pour suivre les clips audio du père en cours de lecture
    private bool uneFois = true; // Permet d'avoir la bourse qu'une seule fois de fait

    void Start()
    {
        // Initialiser les dictionnaires
        knightAudioPlaying = new Dictionary<int, bool>();
        fatherAudioPlaying = new Dictionary<int, bool>();

        for (int i = 0; i < audioSources.Length && i < audioClips.Length; i++)
        {
            // Ajouter les clips audio du chevalier et du père aux dictionnaires
            if (knightAudioIndices.Contains(i))
            {
                knightAudioPlaying.Add(i, true);
            }

            if (fatherAudioIndices.Contains(i))
            {
                fatherAudioPlaying.Add(i, true);
            }
        }

        // Commence par charger la scène
        StartCoroutine(PlayAudioClipsWithDelay());
    }

    IEnumerator PlayAudioClipsWithDelay()
    {
        // Jouer les clips audio avec un délai de 2 secondes entre chaque
        for (int i = 0; i < audioSources.Length && i < audioClips.Length; i++)
        {
            // Vérifier si la source audio et le clip audio existent
            if (audioSources[i] != null && audioClips[i] != null)
            {
                // Réglez le volume de l'AudioSource à 0.5
                audioSources[i].volume = 0.5f;

                // Assigner le clip audio à la source audio
                audioSources[i].clip = audioClips[i];
                // Jouer le son
                audioSources[i].Play();

                // Attendre que le clip audio en cours soit terminé
                while (audioSources[i].isPlaying)
                {
                    // Mettre à jour les matériaux des personnages
                    UpdateCharacterMaterials(i);

                    // Vérifier si c'est le bon moment pour la bourse
                    if (i == 15 && uneFois)
                    {
                        // Activer l'objet
                        bourseObject.SetActive(true);

                        // Attendre la durée de l'animation
                        yield return new WaitForSeconds(5);

                        // Désactiver l'objet après l'animation
                        bourseObject.SetActive(false);

                        uneFois = false; // La bourse n'est plus nécessaire
                    }
                    yield return null;
                }
                 // Vérifier si c'est le dernier clip audio et qu'il est terminé
                if (i == audioSources.Length - 1 && !audioSources[i].isPlaying)
                    {
                        
                        // Charger une nouvelle scène et décharger la scène actuelle
                        SceneManager.LoadScene(sceneToLoad);
                        SceneManager.UnloadSceneAsync(sceneToUnLoad);
                    }
            }

            // Attendre 2 secondes avant de passer au clip suivant
            yield return new WaitForSeconds(2f);
        }
    }

    void UpdateCharacterMaterials(int index)
    {
        // Mettre à jour les matériaux des personnages en fonction de l'index du clip audio en cours
        if (knightAudioPlaying.ContainsKey(index) && knightAudioPlaying[index])
        {
            // Mettre à jour le matériau du chevalier avec le matériau spécifié
            SetCharacterMaterial(GameObject.FindWithTag("Knight").GetComponent<Renderer>(), knightMaterial);
        }
        else
        {
            // Mettre à jour le matériau du chevalier avec le matériau d'origine
            SetCharacterMaterial(GameObject.FindWithTag("Knight").GetComponent<Renderer>(), originalKnightMaterial);
        }

        if (fatherAudioPlaying.ContainsKey(index) && fatherAudioPlaying[index])
        {
            // Mettre à jour le matériau du père avec le matériau spécifié
            SetCharacterMaterial(GameObject.FindWithTag("Father").GetComponent<Renderer>(), fatherMaterial);
        }
        else
        {
            // Mettre à jour le matériau du père avec le matériau d'origine
            SetCharacterMaterial(GameObject.FindWithTag("Father").GetComponent<Renderer>(), originalFatherMaterial);
        }
    }

    void SetCharacterMaterial(Renderer characterRenderer, Material newMaterial)
    {
        // Mettre à jour le matériau du personnage
        if (characterRenderer != null && newMaterial != null)
        {
            characterRenderer.material = newMaterial;
        }
    }
}