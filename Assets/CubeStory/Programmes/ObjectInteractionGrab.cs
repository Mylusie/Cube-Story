using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private GameObject highlightedObject;
    private GameObject grabbedObject;
    private bool isGrabbing = false;
    private float grabDistance = 5.0f; // Distance à laquelle l'objet est tenu de la caméra

    // Fonction pour mettre en surbrillance un objet
    void HighlightObject(GameObject obj, bool highlight)
    {
        // Modifier l'apparence de l'objet pour le mettre en surbrillance ou le rétablir à son aspect normal
        // Par exemple, vous pouvez modifier la couleur de l'objet ou appliquer un shader spécifique
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
                // Rétablir l'aspect normal
                renderer.material.color = Color.white;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast depuis le centre de l'écran pour détecter l'objet à mettre en surbrillance
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Si l'objet touché fait partie du layer "Interaction"
            if (hitObject.layer == LayerMask.NameToLayer("Interaction"))
            {
                // Si c'est un nouvel objet ou s'il est différent de l'objet déjà mis en surbrillance
                if (hitObject != highlightedObject)
                {
                    // Mettre en surbrillance l'objet touché
                    HighlightObject(hitObject, true);

                    // Désactiver la surbrillance de l'objet précédemment mis en surbrillance
                    if (highlightedObject != null)
                    {
                        HighlightObject(highlightedObject, false);
                    }

                    // Mettre à jour l'objet mis en surbrillance
                    highlightedObject = hitObject;
                }
            }
            else
            {
                // Si l'objet touché n'appartient pas au layer "Interaction",
                // désactiver la surbrillance de l'objet précédemment mis en surbrillance
                if (highlightedObject != null)
                {
                    HighlightObject(highlightedObject, false);
                    highlightedObject = null;
                }
            }
        }

        // Si la touche N est enfoncée
        if (Input.GetKeyDown(KeyCode.N))
        {
            // Vérifie s'il y a un objet mis en surbrillance et s'il n'y a pas déjà un objet en train d'être tenu
            if (highlightedObject != null && !isGrabbing)
            {
                // Mettre en surbrillance l'objet touché
                grabbedObject = highlightedObject;
                isGrabbing = true;
                // Désactiver la surbrillance de l'objet attrapé
                HighlightObject(grabbedObject, false);
                // Désactiver la gravité pour que l'objet ne tombe pas
                grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }

        // Si la touche N est relâchée
        if (Input.GetKeyUp(KeyCode.N))
        {
            // Si un objet est en train d'être tenu, le relâcher
            if (isGrabbing)
            {
                isGrabbing = false;
                // Réactiver la gravité de l'objet pour qu'il retombe
                grabbedObject.GetComponent<Rigidbody>().useGravity = true;
                grabbedObject = null;
            }
        }

        // Si un objet est en train d'être tenu
        if (isGrabbing)
        {
            // Calculer la position de l'objet à la distance désirée devant la caméra
            Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * grabDistance;
            // Définir la position de l'objet
            grabbedObject.transform.position = newPosition;

            // Faire tourner l'objet pour qu'il regarde dans la même direction que la caméra
            grabbedObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}