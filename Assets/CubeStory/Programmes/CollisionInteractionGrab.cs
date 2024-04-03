using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionInteractionGrab : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public GameObject collisionIndPrefab;
    private GameObject collisionInd;

    public LayerMask interactionLayer; // Layer contenant les objets interactifs
    public Material highlightMaterial; // Mat�riau de surbrillance
    private Material originalMaterial; // Mat�riau d'origine de l'objet actuellement survol�
    private Renderer lastRenderer; // Dernier Renderer survol�


    private GameObject highlightedObject;
    private GameObject grabbedObject;
    private bool isGrabbing = false;
    private float grabDistance = 5.0f; // Distance � laquelle l'objet est tenu de la cam�ra
    private GameObject GrabableObject;

    void Update()
    {
        RaycastHit hit;
        if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            if (collisionIndPrefab != null)
            {
                if (collisionInd == null)
                {
                    collisionInd = Instantiate(collisionIndPrefab, hit.point, Quaternion.identity);
                }
                else
                {
                    collisionInd.transform.position = hit.point;

                    // Calculate rotation based on the normal of the collided object
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    collisionInd.transform.rotation = rotation;
                }
            }
        }
        else
        {
            if (collisionInd != null)
            {
                Destroy(collisionInd);
                collisionInd = null;
            }
        }

        if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            // L'objet est survol� par le pointeur
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.layer == LayerMask.NameToLayer("Interaction"))
            {
                GrabableObject = hitObject;
                if (renderer != null)
                {
                    if (renderer != lastRenderer)
                    {
                        // R�tablit le mat�riau d'origine du dernier objet survol�
                        if (lastRenderer != null)
                        {
                            lastRenderer.material = originalMaterial;
                        }

                        // Conserve le mat�riau d'origine de l'objet actuellement survol�
                        originalMaterial = renderer.material;
                        lastRenderer = renderer;

                        // Appliquer la surbrillance (par exemple, changer le mat�riau en mat�riau de surbrillance)
                        renderer.material = highlightMaterial;



                    }
                }
                else
                {
                    // Aucun objet n'est survol�, r�tablit le mat�riau d'origine du dernier objet survol�
                    if (lastRenderer != null)
                    {
                        lastRenderer.material = originalMaterial;
                        lastRenderer = null;
                    }
                }
            }
            else
            {
                // Aucun objet n'est survol�, r�tablit le mat�riau d'origine du dernier objet survol�
                if (lastRenderer != null)
                {
                    lastRenderer.material = originalMaterial;
                    lastRenderer = null;
                }
            }

            // Si la touche N est enfonc�e
            if (Input.GetKeyDown(KeyCode.N))
            {
                //Debug.Log("111");
                if (GrabableObject != null && !isGrabbing)
                {
                    isGrabbing = true;
                    grabbedObject = GrabableObject;
                    grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                    //Debug.Log("222");
                }
            }
            // Si la touche N est rel�ch�e
            if (Input.GetKeyUp(KeyCode.N))
            {
                //Debug.Log("333");
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
                //Debug.Log("444");
                // Calculer la position de l'objet � la distance d�sir�e devant la cam�ra
                Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * grabDistance;
                // D�finir la position de l'objet
                grabbedObject.transform.position = newPosition;

                // Faire tourner l'objet pour qu'il regarde dans la m�me direction que la cam�ra
                grabbedObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            }
        }
    }
}

