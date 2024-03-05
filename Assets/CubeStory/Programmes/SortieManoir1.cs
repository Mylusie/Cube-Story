using UnityEngine;

public class HighlightObjects : MonoBehaviour
{
    public GameObject keySortie;    // R�f�rence au gameobject "key_sortie"
    public GameObject carpetSortie; // R�f�rence au gameobject "carpet_sortie"
    public GameObject porteSortie; // R�f�rence au gameobject "porte_sortie"

    public Color highlightColor = Color.blue; // Couleur de surbrillance

    public float blinkInterval = 0.5f; // Intervalle de clignotement en secondes

    private bool isHighlighted = false; // Indique si les objets sont actuellement en surbrillance
    private float timer = 0f;

    void Update()
    {
        // V�rifier si les deux objets sont � moins de 4 m�tre l'un de l'autre
        if (keySortie != null && carpetSortie != null && Vector3.Distance(keySortie.transform.position, carpetSortie.transform.position) < 4f)
        {
            // Activer le clignotement des objets
            if (!isHighlighted)
            {
                isHighlighted = true;
                InvokeRepeating("ToggleHighlight", 0f, blinkInterval);
                // D�truire le gameobject "key_sortie"
                Destroy(keySortie);
                porteSortie.SetActive(true);
            }

            else
            {
                // D�sactiver le clignotement des objets
                if (isHighlighted)
                {
                    isHighlighted = false;
                    CancelInvoke("ToggleHighlight");
                    SetHighlightState(false); // Assurez-vous que les objets sont d�finis sur leur �tat normal
                }
            }
        }
    }

    // Fonction pour basculer entre l'�tat de surbrillance et l'�tat normal
    void ToggleHighlight()
    {
        SetHighlightState(!isHighlighted);
    }

    // Fonction pour d�finir l'�tat de surbrillance des objets
    void SetHighlightState(bool highlight)
    {
        Color targetColor = highlight ? highlightColor : Color.white; // Couleur cible en fonction de l'�tat de surbrillance

        // D�finir la couleur de surbrillance pour les deux objets
        SetObjectHighlight(keySortie, targetColor);
        SetObjectHighlight(carpetSortie, targetColor);
    }

    // Fonction pour d�finir la couleur de surbrillance d'un objet
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

