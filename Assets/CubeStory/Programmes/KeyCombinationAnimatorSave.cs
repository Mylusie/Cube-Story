using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyCombinationAnimatorSave : MonoBehaviour
{
    public GameObject XRorigin;
    public AnimationClip animationClip;
    
    // Les touches de la combinaison
    public KeyCode[] keyCombination = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    public GameObject cube;
    public GameObject affichageCombinaison;
    private int currentKeyIndex = 0;
    private bool disableScheduled = false;
    private bool isActive = false;


    void Update()
    {
        Animator animator = XRorigin.GetComponent<Animator>();
        // Vérifie si la combinaison de touches est entrée
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Debug.Log("Touche enfoncée : " + keyCode.ToString());
                }
            }
            //Debug.Log(keyCombination[currentKeyIndex]);
            //Debug.Log(currentKeyIndex);
            // Vérifie si la touche enfoncée correspond à la prochaine touche de la combinaison
            if (Input.GetKeyDown(keyCombination[currentKeyIndex]))
            {
                // Incrémente l'index de la touche
                currentKeyIndex++;
                
                //Debug.Log(keyCombination[currentKeyIndex]);
                Debug.Log(currentKeyIndex);

                // Affiche la combinaison à l'écran
                


                // Si toutes les touches de la combinaison ont été enfoncées, active l'animation
                if (currentKeyIndex == keyCombination.Length)
                {
                    if (animator != null)
                    {
                        // Active l'animation en changeant l'état de l'Animator
                        animator.enabled = true;
                        float animationDuration = animationClip.length;
                        Invoke(nameof(DisableAnimator), animationDuration);
                        Debug.Log("disableScheduled reussi");
                    }

                    // Réinitialise l'indice de la touche pour permettre une nouvelle combinaison
                    currentKeyIndex = 0;
                    UpdateCombinationText();
                }
            }
            else
            {
                // Si la touche enfoncée n'est pas la prochaine dans la combinaison, réinitialise l'index
                currentKeyIndex = 0;
                ClearCombinationText();
            }
        }
    }


    void DisableAnimator()
    {
        Animator animator = XRorigin.GetComponent<Animator>();
        // Désactive l'Animator
        animator.enabled = false;
        disableScheduled = true; // Indique que la désactivation a été programmée
    }

    // Assurez-vous de désactiver l'appel Invoke si l'objet est désactivé avant que le délai soit écoulé
    private void OnDisable()
    {
        if (!disableScheduled)
        {
            CancelInvoke(nameof(DisableAnimator));
        }
    }


    // Active le script et affiche le texte de la combinaison à l'écran
    public void Activate()
    {
        isActive = true;
        UpdateCombinationText();
    }

    // Désactive le script et efface le texte de la combinaison à l'écran
    public void Deactivate()
    {
        isActive = false;
        ClearCombinationText();
    }

    // Met à jour le texte de la combinaison à l'écran
    private void UpdateCombinationText()
    {
        if (affichageCombinaison.GetComponent<TextMeshProUGUI>().text != null)
        {
            string combinationString = "";
            Debug.Log(combinationString);
            foreach (KeyCode key in keyCombination)
            {
                combinationString += key.ToString() + " ";
            }
            affichageCombinaison.GetComponent<TextMeshProUGUI>().text = combinationString;
        }
    }

    // Efface le texte de la combinaison à l'écran
    private void ClearCombinationText()
    {
        if (affichageCombinaison.GetComponent<TextMeshProUGUI>().text != null)
        {
            affichageCombinaison.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
