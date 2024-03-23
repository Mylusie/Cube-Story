using UnityEngine;

public class InteractionBag : MonoBehaviour
{
    public GameObject objectToInteractWith;
    public KeyCode AcceptButton = KeyCode.M;
    public KeyCode throwButton = KeyCode.T;
    public Material highlightMaterial;
    public GameObject Affichage;
    public GameObject AudiomanActu;
    public GameObject audioJet;
    public GameObject AudiomanAccept;
    public GameObject ObjetBagueabouger;
    public GameObject baguePrefab;
    public AudioClip[] audioClips; // Tableau des clips audio

    private bool hasThrowing = true;

    private void Update()
    {
        if (IsObjectHighlighted(objectToInteractWith) && IsAnyClipPlaying(audioClips))
        {
            if (Input.GetKeyDown(throwButton) && hasThrowing)
            {
                Debug.Log("Throw");
                ThrowRing();
            }

            if (Input.GetKeyDown(AcceptButton))
            {
                Debug.Log("Accept");
                baguePrefab.SetActive(false);
                Affichage.SetActive(false);
                DisableAudioManager();
                
            }

            if (Affichage != null)
            {
                Affichage.SetActive(true);
            }
        }
        else
        {
            if (Affichage != null)
            {
                Affichage.SetActive(false);
            }
        }
    }

    private bool IsObjectHighlighted(GameObject obj)
    {
        if (obj == null) return false;

        Renderer renderer = obj.GetComponent<Renderer>();
        return renderer != null && renderer.sharedMaterial == highlightMaterial;
    }

    private void ThrowRing()
    {
        if (ObjetBagueabouger != null && baguePrefab != null && hasThrowing)
        {
            ObjetBagueabouger.SetActive(true);
            baguePrefab.SetActive(false);
            ObjetBagueabouger.GetComponent<Animator>().enabled = true;

            AudiomanActu.SetActive(false);
            if (audioJet != null)
            {
                audioJet.SetActive(true);
            }

            hasThrowing = false;
        }
        else
        {
            Debug.LogError("L'objet vers lequel lancer la bague ou le prefab de la bague n'est pas défini !");
        }
    }

    private void DisableAudioManager()
    {
        AudiomanActu.SetActive(false);
        Debug.Log("Audio Manager désactivé.");

        AudiomanAccept.SetActive(true);
        Debug.Log("Audio Manager accept activé.");
    }

    private bool IsAnyClipPlaying(AudioClip[] clips)
    {
        if (AudiomanActu != null && AudiomanActu.GetComponent<AudioSource>().isPlaying)
        {
            foreach (var clip in clips)
            {
                if (AudiomanActu.GetComponent<AudioSource>().clip == clip)
                {
                    return false;
                }
            }
        }
        return true;
    }

}