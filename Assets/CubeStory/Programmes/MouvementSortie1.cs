using UnityEngine;

public class DeplacementCameraSortie1 : MonoBehaviour
{
    public GameObject cible; // Position vers laquelle déplacer la caméra
    public float vitesseDeplacement = 5.0f; // Vitesse de déplacement de la caméra
    public GameObject porteSortie; // GameObject dont l'état d'activation contrôle le déplacement de la caméra
    public float seuilDistance = 0.01f;// Seuil de distance pour considérer que la caméra est arrivée à la nouvelle position
    public GameObject MainCamera;
    //private bool enDeplacement = false; // Indique si la caméra est en cours de déplacement

    void Update()
    {
        // Vérifier si le GameObject de condition est actif et si la caméra n'est pas déjà en déplacement
        if (porteSortie != null && porteSortie.activeSelf)//&& !enDeplacement
        {
            // Déplacer progressivement la caméra vers la nouvelle position
            Debug.Log("la porte est détectée");

            //CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
            //capsuleCollider.enabled = false;

            //transform.position = Vector3.MoveTowards(transform.position, cible.transform.position, vitesseDeplacement * Time.deltaTime);
            //Debug.Log("Position : " + transform.position);

            ActivateAnimator(MainCamera);


            // Vérifier si la caméra a atteint la nouvelle position
            if (Vector3.Distance(transform.position, cible.transform.position) < seuilDistance)
            {
                //capsuleCollider.enabled = true;
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
