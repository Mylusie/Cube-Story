using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class KeyCombinationAnimator : MonoBehaviour
{
    public GameObject XROrigin;
    public AnimationClip animationClip;
    public GameObject disparitionIndice;

    // Les touches de la combinaison
    public KeyCode[] keyCombination = { KeyCode.S, KeyCode.D, KeyCode.A, KeyCode.W };
    public GameObject cube;
    public GameObject affichageCombinaison;
    public GameObject affichageCombinaisonCode;
    public GameObject affichageRequeteParchemin;
    private int currentKeyInd = 0;
    private bool disableScheduled = false;
    private bool isActive = false;
    private string currentCombination = "";
    private float count = 0;
    private string Addtext = "";
    private string currentPrint = "";
    private TextMeshPro textMeshProComponent;
    private bool sortieReussie = false;

    private void Start()
    {
        affichageCombinaison.SetActive(false);
        affichageRequeteParchemin.SetActive(false);
    }
    void Update()
    {
        if (!affichageCombinaison.activeSelf)
        {
            return;
        }
            
        if (disparitionIndice.activeSelf)
        {
            affichageRequeteParchemin.SetActive(true);
            return;
        }    
        affichageRequeteParchemin.SetActive(false);

        if (Input.anyKeyDown)
        {
            KeyCode keyPressed = GetKeyPressed();
            if (keyPressed != KeyCode.None)
            {
                if (currentCombination.Length/2 < keyCombination.Length)
                {
                    Addtext = keyPressed.ToString();
                    currentCombination += Addtext + " ";
                }
                

                if (GetKeyPressed() == KeyCode.W && currentPrint.Length < 4) 
                {
                    currentPrint += "2";
                }
                else if (GetKeyPressed() == KeyCode.A && currentPrint.Length < 4) 
                {
                    currentPrint += "3";
                }
                else if (GetKeyPressed() == KeyCode.S && currentPrint.Length < 4) 
                {
                    currentPrint += "4";
                }
                else if (GetKeyPressed() == KeyCode.D && currentPrint.Length < 4) 
                {
                    currentPrint += "1";
                }
                count++;

                textMeshProComponent = affichageCombinaisonCode.GetComponent<TextMeshPro>();
                if (textMeshProComponent != null)
                {
                    textMeshProComponent.text = currentPrint;
                }
                else
                {
                    Debug.LogError("Le GameObject ne contient pas de composant TextMeshPro !");
                }


                if (currentPrint.Length >= keyCombination.Length)
                {
                    if (CheckCombination())
                    {
                        if (!sortieReussie)
                        {
                            // Active l'animation en changeant l'état de l'Animator
                            textMeshProComponent.color = Color.green;
                            ChangeCodeColorGreen();
                            Invoke(nameof(LetsGo), 2f);

                            float animationDuration = animationClip.length;
                            Debug.Log(animationDuration);
                            Invoke(nameof(DisableAnimator), animationDuration);
                            sortieReussie = true;
                        }
                    }
                    else
                    {
                        textMeshProComponent.color = Color.red;
                        count = 0;
                        Invoke(nameof(ChangeCodeColorRed),2f);
                        
                    }
                }
            }
        }
    }
    void DisableAnimator()
    {
        Animator animator = XROrigin.GetComponent<Animator>();

        // Désactive l'Animator
        animator.enabled = false;
        disableScheduled = true; // Indique que la désactivation a été programmée
        GameObject xrOriginObject = XROrigin;
        XRCharacterController xrCharacterControllerComponent = xrOriginObject.GetComponent<XRCharacterController>();
        xrCharacterControllerComponent.enabled = true;
    }

    void LetsGo()
    {
        Animator animator = XROrigin.GetComponent<Animator>();
        animator.enabled = true;
        affichageCombinaison.SetActive(false);
        cube.SetActive(false);
    }
    void ChangeCodeColorGreen()
    {
        textMeshProComponent.color = Color.green;
    }

    void ChangeCodeColorRed()
    {
        currentPrint = "";
        currentCombination = "";
        textMeshProComponent.text = currentPrint;
        textMeshProComponent.color = Color.white; 
    }


    private void OnDisable()
    {
        if (!disableScheduled)
        {
            CancelInvoke(nameof(DisableAnimator));
        }
    }

    KeyCode GetKeyPressed()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                return keyCode;
            }
        }
        return KeyCode.None;
    }

    bool CheckCombination()
    {
        for (int i = 0; i < keyCombination.Length; i++)
        {
            if (currentCombination[i * 2] != keyCombination[i].ToString()[0])
            {
                return false;
            }
        }
        return true;
    }

    string GetKeyCombinationAsString()
    {
        string combinationString = "";
        // Parcourir le tableau et concaténer chaque élément dans une chaîne
        for (int i = 0; i < keyCombination.Length; i++)
        {
            combinationString += keyCombination[i].ToString();
            // Ajouter un espace après chaque touche, sauf pour la dernière
            if (i < keyCombination.Length - 1)
            {
                combinationString += " ";
            }
        }
        return combinationString;
    }

}