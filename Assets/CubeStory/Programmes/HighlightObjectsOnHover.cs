using UnityEngine;

public class HighlightObjectsOnHover : MonoBehaviour
{
    public LayerMask interactionLayer; // Layer contenant les objets interactifs
    public Material highlightMaterial; // Matériau de surbrillance

    private Material originalMaterial; // Matériau d'origine de l'objet actuellement survolé
    private Renderer lastRenderer; // Dernier Renderer survolé

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, interactionLayer))
        {
            // L'objet est survolé par le pointeur
            Renderer renderer = hit.collider.GetComponent<Renderer>();
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
}
