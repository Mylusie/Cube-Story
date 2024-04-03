using UnityEngine;

public class DeplacementCameraSortie1 : MonoBehaviour
{
    public GameObject cible; // Position vers laquelle déplacer la caméra
    public float vitesseDeplacement = 5.0f; // Vitesse de déplacement de la caméra
    public GameObject porteSortie; // GameObject dont l'état d'activation contrôle le déplacement de la caméra
    public float seuilDistance = 0.01f;// Seuil de distance pour considérer que la caméra est arrivée à la nouvelle position
    public GameObject MainCamera;
    private bool unite = false;
    public AnimationClip animationClip;
    private bool disableScheduled = false;

    //private bool enDeplacement = false; // Indique si la caméra est en cours de déplacement


    void Update()
    {
        // Vérifier si le GameObject de condition est actif et si la caméra n'est pas déjà en déplacement
        if (porteSortie != null && porteSortie.activeSelf && unite==false)//&& !enDeplacement
        {
            // Déplacer progressivement la caméra vers la nouvelle position
            Debug.Log("la porte est détectée");
            
            CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
            //capsuleCollider.enabled = false;

            //transform.position = Vector3.MoveTowards(transform.position, cible.transform.position, vitesseDeplacement * Time.deltaTime);
            //Debug.Log("Position : " + transform.position);

            ActivateAnimator(MainCamera);
            unite = true;

            // Vérifier si la caméra a atteint la nouvelle position
            Debug.Log(Vector3.Distance(transform.position, cible.transform.position));
            if (Vector3.Distance(transform.position, cible.transform.position) < seuilDistance)
            {
                capsuleCollider.enabled = true;
                Debug.Log("La caméra est arrivée à la nouvelle position.");
                //enDeplacement = true;
                porteSortie.SetActive(false);

            }
        }
    }

    // Fonction pour démarrer le déplacement de la caméra vers la nouvelle position
    //public void DeplacerVersNouvellePosition(GameObject targetGameObject)
    //{
        //enDeplacement = false;
        //cible = targetGameObject;
    //}
    void ActivateAnimator(GameObject gameObject)
        
    {
        Debug.Log("3 ");
        if (gameObject != null)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            Debug.Log("1 ");
            if (animator != null)

            {

                Debug.Log("2"); 
                animator.enabled = true;
                float animationDuration = animationClip.length;
                Invoke(nameof(DisableAnimator), animationDuration/2);
                Debug.Log("disableScheduled reussi");
            }
        }
    }
    void DisableAnimator()
    {
        Animator animator = gameObject.GetComponent<Animator>();
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
}
