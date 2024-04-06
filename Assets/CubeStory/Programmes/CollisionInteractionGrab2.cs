using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionInteractionGrab2 : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public GameObject XROrigin;
    private GameObject scriptdeplacements;
    public GameObject collisionIndPrefab;
    private GameObject collisionInd;

    public LayerMask interactionLayer; // Layer contenant les objets interactifs
    public Material highlightMaterial; // Matériau de surbrillance
    private Material originalMaterial; // Matériau d'origine de l'objet actuellement survolé
    private Renderer lastRenderer; // Dernier Renderer survolé
    public GameObject cube;
    public GameObject affichageCombinaison;

    private GameObject highlightedObject;
    private GameObject grabbedObject = null;
    private bool isGrabbing = false;
    private float grabDistance = 5.0f; // Distance à laquelle l'objet est tenu de la caméra
    private GameObject GrabableObject = null; 

    public GameObject exitObject;  // Gameobject utilisé pour la 2e sortie du manoir
    public GameObject indiceSortie; // GameObject qui va afficher un text donnant des indices pour sortir

    private void Start()
    {
        affichageCombinaison.SetActive(false);
    }
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
            // L'objet est survolé par le pointeur
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.layer == LayerMask.NameToLayer("Interaction"))
            {
                GrabableObject = hitObject;
                if (renderer != null)
                {
                    if (renderer != lastRenderer)
                    {
                        // Rétablit le matériau d'origine du dernier objet survolé
                        if (lastRenderer != null)
                        {
                            lastRenderer.material = originalMaterial;
                        }

                        // Conserve le matériau d'origine de l'objet actuellement survolé
                        originalMaterial = renderer.material;
                        lastRenderer = renderer;

                        // Appliquer la surbrillance (par exemple, changer le matériau en matériau de surbrillance)
                        renderer.material = highlightMaterial;



                    }
                }
                else
                {
                    // Aucun objet n'est survolé, rétablit le matériau d'origine du dernier objet survolé
                    if (lastRenderer != null)
                    {
                        lastRenderer.material = originalMaterial;
                        lastRenderer = null;
                    }
                }
            }
            else
            {
                // Aucun objet n'est survolé, rétablit le matériau d'origine du dernier objet survolé
                if (lastRenderer != null)
                {
                    lastRenderer.material = originalMaterial;
                    lastRenderer = null;
                }
                GrabableObject = null;
            }
            
            // Si la touche N est enfoncée
            if (Input.GetKeyDown(KeyCode.N))
            {
                if (GrabableObject != null && !isGrabbing && GrabableObject != cube)
                {
                    isGrabbing = true;
                    grabbedObject = GrabableObject;
                    grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                    collisionIndPrefab.SetActive(false);
                }
                Debug.Log(affichageCombinaison.activeSelf);
                if (GrabableObject == cube)
                {
                    if (!affichageCombinaison.activeSelf)
                    {
                        affichageCombinaison.SetActive(true);
                        GameObject xrOriginObject = XROrigin;
                        XRCharacterController xrCharacterControllerComponent = xrOriginObject.GetComponent<XRCharacterController>();
                        xrCharacterControllerComponent.enabled = false;

                    }
                    else
                    {
                        affichageCombinaison.SetActive(false);
                        GameObject xrOriginObject = XROrigin;
                        XRCharacterController xrCharacterControllerComponent = xrOriginObject.GetComponent<XRCharacterController>();
                        xrCharacterControllerComponent.enabled = true;
                    }
                }
            }
            // Si la touche N est relâchée
            if (Input.GetKeyUp(KeyCode.N))
            {
                // Debug.Log("333");
                // Si un objet est en train d'être tenu, le relâcher
                if (isGrabbing)
                {
                    isGrabbing = false;
                    // Réactiver la gravité de l'objet pour qu'il retombe
                    grabbedObject.GetComponent<Rigidbody>().useGravity = true;
                    collisionIndPrefab.SetActive(true);
                    if (grabbedObject == exitObject)
                    {
                        indiceSortie.SetActive(false);
                    }
                }
                grabbedObject = null;
                GrabableObject = null;
            }
            // Si un objet est en train d'être tenu
            if (isGrabbing)
            {
                //Debug.Log("444");
                // Calculer la position de l'objet à la distance désirée devant la caméra
                Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * grabDistance;
                // Définir la position de l'objet
                grabbedObject.transform.position = newPosition;

                // Faire tourner l'objet pour qu'il regarde dans la même direction que la caméra
                grabbedObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
                Debug.Log("111");
                if (grabbedObject == exitObject)
                {
                    indiceSortie.SetActive(true);
                    Debug.Log("indice");
                }
            }
        }
    }
}

