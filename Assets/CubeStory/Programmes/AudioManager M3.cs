using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManagerM11: MonoBehaviour
{
    public AudioSource[] audioSources; // Tableau des sources audio
    public AudioClip[] audioClips; // Tableau des clips audio
    public List<int> knightAudioIndices; // Indices des clips audio du chevalier
    public List<int> fatherAudioIndices; // Indices des clips audio du père
    public Material knightMaterial; // Matériau pour le chevalier
    public Material fatherMaterial; // Matériau pour le père
    public Material originalKnightMaterial; // Matériau d'origine pour le chevalier
    public Material originalFatherMaterial; // Matériau d'origine pour le père
    public Animator Anim; // GameObject de la bourse
    public GameObject Cyl;

    private Dictionary<int, bool> knightAudioPlaying; // Dictionnaire pour suivre les clips audio du chevalier en cours de lecture
    private Dictionary<int, bool> fatherAudioPlaying; // Dictionnaire pour suivre les clips audio du père en cours de lecture
    private bool uneFois = true; // Permet d'avoir la bourse qu'une seule fois de fait

    private float timer = 0f;
    private bool timerActive = false;

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
        Debug.Log("Je rentre");
         float totalClipLength = 0f;
        // Jouer les clips audio avec un délai de 2 secondes entre chaque
        for (int i = 0; i < audioSources.Length && i < audioClips.Length; i++)
        {
            Debug.Log(audioSources.Length + "long");
            // Vérifier si la source audio et le clip audio existent
            if (audioSources[i] != null && audioClips[i] != null)
            {
                // Réglez le volume de l'AudioSource à 0.5
                audioSources[i].volume = 0.85f;

                Debug.Log(i);

                // Assigner le clip audio à la source audio
                audioSources[i].clip = audioClips[i];
                // Jouer le son
                audioSources[i].Play();

                totalClipLength = audioClips[i].length;

                Debug.Log("Je suis a l'intérieur");
                // Mettre à jour les matériaux des personnages
                if (knightMaterial != null || fatherMaterial != null)
                {UpdateCharacterMaterials(i);}
                

                Debug.Log("Je suis a l'bru");
                yield return new WaitForSeconds(totalClipLength);

                if(i ==3)
                {yield return new WaitForSeconds(20);}

                

                Debug.Log("finit");
                 // Vérifier si c'est le dernier clip audio et qu'il est terminé
                if (i == audioSources.Length - 1 && !audioSources[i].isPlaying)
                    {
                        
                        // Charger une nouvelle scène et décharger la scène actuelle
                        Anim.SetBool("Finit audio", true);

                        timerActive = true;
                    }

                if (timerActive)
                {
                    timer += Time.deltaTime;

                    if (timer >= 5f)
                    {
                        timerActive = false;

                        // Afficher le GameObject
                        Cyl.SetActive(true);
                    }
                }
            }
            Debug.Log("Attente");
            // Attendre 2 secondes avant de passer au clip suivant
            yield return new WaitForSeconds(0.5f);
        }
    }

    void UpdateCharacterMaterials(int index)
    {

        Debug.Log("Je suis la");
        // Mettre à jour les matériaux des personnages en fonction de l'index du clip audio en cours
        if (knightMaterial != null)
        {
            if (knightAudioPlaying.ContainsKey(index) && knightAudioPlaying[index])
            {
                // Mettre à jour le matériau du chevalier avec le matériau spécifié
                SetCharacterMaterial(GameObject.FindWithTag("Knight").GetComponent<Renderer>(), knightMaterial);
                Debug.Log("Je suis laok");
            }
            else
            {
                // Mettre à jour le matériau du chevalier avec le matériau d'origine
                SetCharacterMaterial(GameObject.FindWithTag("Knight").GetComponent<Renderer>(), originalKnightMaterial);
                Debug.Log("Je suis lak");
            }
        }
        if (fatherMaterial != null)
        {if (fatherAudioPlaying.ContainsKey(index) && fatherAudioPlaying[index])
            {
                // Mettre à jour le matériau du père avec le matériau spécifié
                SetCharacterMaterial(GameObject.FindWithTag("Father").GetComponent<Renderer>(), fatherMaterial);
                Debug.Log("Je suis laof");
            }
            else
            {
                // Mettre à jour le matériau du père avec le matériau d'origine
                SetCharacterMaterial(GameObject.FindWithTag("Father").GetComponent<Renderer>(), originalFatherMaterial);
                Debug.Log("Je suis laF");
            }
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