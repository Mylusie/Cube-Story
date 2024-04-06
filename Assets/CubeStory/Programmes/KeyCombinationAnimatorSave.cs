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
        // V�rifie si la combinaison de touches est entr�e
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Debug.Log("Touche enfonc�e : " + keyCode.ToString());
                }
            }
            //Debug.Log(keyCombination[currentKeyIndex]);
            //Debug.Log(currentKeyIndex);
            // V�rifie si la touche enfonc�e correspond � la prochaine touche de la combinaison
            if (Input.GetKeyDown(keyCombination[currentKeyIndex]))
            {
                // Incr�mente l'index de la touche
                currentKeyIndex++;
                
                //Debug.Log(keyCombination[currentKeyIndex]);
                Debug.Log(currentKeyIndex);

                // Affiche la combinaison � l'�cran
                


                // Si toutes les touches de la combinaison ont �t� enfonc�es, active l'animation
                if (currentKeyIndex == keyCombination.Length)
                {
                    if (animator != null)
                    {
                        // Active l'animation en changeant l'�tat de l'Animator
                        animator.enabled = true;
                        float animationDuration = animationClip.length;
                        Invoke(nameof(DisableAnimator), animationDuration);
                        Debug.Log("disableScheduled reussi");
                    }

                    // R�initialise l'indice de la touche pour permettre une nouvelle combinaison
                    currentKeyIndex = 0;
                    UpdateCombinationText();
                }
            }
            else
            {
                // Si la touche enfonc�e n'est pas la prochaine dans la combinaison, r�initialise l'index
                currentKeyIndex = 0;
                ClearCombinationText();
            }
        }
    }


    void DisableAnimator()
    {
        Animator animator = XRorigin.GetComponent<Animator>();
        // D�sactive l'Animator
        animator.enabled = false;
        disableScheduled = true; // Indique que la d�sactivation a �t� programm�e
    }

    // Assurez-vous de d�sactiver l'appel Invoke si l'objet est d�sactiv� avant que le d�lai soit �coul�
    private void OnDisable()
    {
        if (!disableScheduled)
        {
            CancelInvoke(nameof(DisableAnimator));
        }
    }


    // Active le script et affiche le texte de la combinaison � l'�cran
    public void Activate()
    {
        isActive = true;
        UpdateCombinationText();
    }

    // D�sactive le script et efface le texte de la combinaison � l'�cran
    public void Deactivate()
    {
        isActive = false;
        ClearCombinationText();
    }

    // Met � jour le texte de la combinaison � l'�cran
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

    // Efface le texte de la combinaison � l'�cran
    private void ClearCombinationText()
    {
        if (affichageCombinaison.GetComponent<TextMeshProUGUI>().text != null)
        {
            affichageCombinaison.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
