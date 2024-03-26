using UnityEngine;

public class DeplacementCameraSortie1 : MonoBehaviour
{
    public GameObject cible; // Position vers laquelle d�placer la cam�ra
    public float vitesseDeplacement = 5.0f; // Vitesse de d�placement de la cam�ra
    public GameObject porteSortie; // GameObject dont l'�tat d'activation contr�le le d�placement de la cam�ra
    public float seuilDistance = 0.01f;// Seuil de distance pour consid�rer que la cam�ra est arriv�e � la nouvelle position
    public GameObject MainCamera;
    private bool unite = false;

    //private bool enDeplacement = false; // Indique si la cam�ra est en cours de d�placement

    void Update()
    {
        // V�rifier si le GameObject de condition est actif et si la cam�ra n'est pas d�j� en d�placement
        if (porteSortie != null && porteSortie.activeSelf && unite==false)//&& !enDeplacement
        {
            // D�placer progressivement la cam�ra vers la nouvelle position
            Debug.Log("la porte est d�tect�e");
            
            CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
            //capsuleCollider.enabled = false;

            //transform.position = Vector3.MoveTowards(transform.position, cible.transform.position, vitesseDeplacement * Time.deltaTime);
            //Debug.Log("Position : " + transform.position);

            ActivateAnimator(MainCamera);
            unite = true;

            // V�rifier si la cam�ra a atteint la nouvelle position
            Debug.Log(Vector3.Distance(transform.position, cible.transform.position));
            if (Vector3.Distance(transform.position, cible.transform.position) < seuilDistance)
            {
                capsuleCollider.enabled = true;
                Debug.Log("La cam�ra est arriv�e � la nouvelle position.");
                //enDeplacement = true;
                porteSortie.SetActive(false);

            }
        }
    }

    // Fonction pour d�marrer le d�placement de la cam�ra vers la nouvelle position
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
            }
        }
    }
}