using UnityEngine;

public class HighlightObjects : MonoBehaviour
{
    public GameObject keySortie;    // Référence au gameobject "key_sortie"
    public GameObject carpetSortie; // Référence au gameobject "carpet_sortie"
    public GameObject porteSortie; // Référence au gameobject "porte_sortie"

    public Color highlightColor = Color.blue; // Couleur de surbrillance

    public float blinkInterval = 0.5f; // Intervalle de clignotement en secondes

    private bool isHighlighted = false; // Indique si les objets sont actuellement en surbrillance
    private float timer = 0f;

    void Update()
    {
        // Vérifier si les deux objets sont à moins de 4 mètre l'un de l'autre
        if (keySortie != null && carpetSortie != null && Vector3.Distance(keySortie.transform.position, carpetSortie.transform.position) < 4f)
        {
            // Activer le clignotement des objets
            if (!isHighlighted)
            {
                isHighlighted = true;
                InvokeRepeating("ToggleHighlight", 0f, blinkInterval);
                // Détruire le gameobject "key_sortie"
                Destroy(keySortie);
                porteSortie.SetActive(true);
            }

            else
            {
                // Désactiver le clignotement des objets
                if (isHighlighted)
                {
                    isHighlighted = false;
                    CancelInvoke("ToggleHighlight");
                    SetHighlightState(false); // Assurez-vous que les objets sont définis sur leur état normal
                }
            }
        }
    }

    // Fonction pour basculer entre l'état de surbrillance et l'état normal
    void ToggleHighlight()
    {
        SetHighlightState(!isHighlighted);
    }

    // Fonction pour définir l'état de surbrillance des objets
    void SetHighlightState(bool highlight)
    {
        Color targetColor = highlight ? highlightColor : Color.white; // Couleur cible en fonction de l'état de surbrillance

        // Définir la couleur de surbrillance pour les deux objets
        SetObjectHighlight(keySortie, targetColor);
        SetObjectHighlight(carpetSortie, targetColor);
    }

    // Fonction pour définir la couleur de surbrillance d'un objet
    void SetObjectHighlight(GameObject obj, Color color)
    {
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }
}

