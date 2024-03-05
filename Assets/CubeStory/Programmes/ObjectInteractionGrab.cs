using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private GameObject highlightedObject;
    private GameObject grabbedObject;
    private bool isGrabbing = false;
    private float grabDistance = 5.0f; // Distance � laquelle l'objet est tenu de la cam�ra

    // Fonction pour mettre en surbrillance un objet
    void HighlightObject(GameObject obj, bool highlight)
    {
        // Modifier l'apparence de l'objet pour le mettre en surbrillance ou le r�tablir � son aspect normal
        // Par exemple, vous pouvez modifier la couleur de l'objet ou appliquer un shader sp�cifique
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (highlight)
            {
                // Appliquer la surbrillance
                renderer.material.color = Color.yellow;
            }
            else
            {
                // R�tablir l'aspect normal
                renderer.material.color = Color.white;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast depuis le centre de l'�cran pour d�tecter l'objet � mettre en surbrillance
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Si l'objet touch� fait partie du layer "Interaction"
            if (hitObject.layer == LayerMask.NameToLayer("Interaction"))
            {
                // Si c'est un nouvel objet ou s'il est diff�rent de l'objet d�j� mis en surbrillance
                if (hitObject != highlightedObject)
                {
                    // Mettre en surbrillance l'objet touch�
                    HighlightObject(hitObject, true);

                    // D�sactiver la surbrillance de l'objet pr�c�demment mis en surbrillance
                    if (highlightedObject != null)
                    {
                        HighlightObject(highlightedObject, false);
                    }

                    // Mettre � jour l'objet mis en surbrillance
                    highlightedObject = hitObject;
                }
            }
            else
            {
                // Si l'objet touch� n'appartient pas au layer "Interaction",
                // d�sactiver la surbrillance de l'objet pr�c�demment mis en surbrillance
                if (highlightedObject != null)
                {
                    HighlightObject(highlightedObject, false);
                    highlightedObject = null;
                }
            }
        }

        // Si la touche N est enfonc�e
        if (Input.GetKeyDown(KeyCode.N))
        {
            // V�rifie s'il y a un objet mis en surbrillance et s'il n'y a pas d�j� un objet en train d'�tre tenu
            if (highlightedObject != null && !isGrabbing)
            {
                // Mettre en surbrillance l'objet touch�
                grabbedObject = highlightedObject;
                isGrabbing = true;
                // D�sactiver la surbrillance de l'objet attrap�
                HighlightObject(grabbedObject, false);
                // D�sactiver la gravit� pour que l'objet ne tombe pas
                grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }

        // Si la touche N est rel�ch�e
        if (Input.GetKeyUp(KeyCode.N))
        {
            // Si un objet est en train d'�tre tenu, le rel�cher
            if (isGrabbing)
            {
                isGrabbing = false;
                // R�activer la gravit� de l'objet pour qu'il retombe
                grabbedObject.GetComponent<Rigidbody>().useGravity = true;
                grabbedObject = null;
            }
        }

        // Si un objet est en train d'�tre tenu
        if (isGrabbing)
        {
            // Calculer la position de l'objet � la distance d�sir�e devant la cam�ra
            Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * grabDistance;
            // D�finir la position de l'objet
            grabbedObject.transform.position = newPosition;

            // Faire tourner l'objet pour qu'il regarde dans la m�me direction que la cam�ra
            grabbedObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}