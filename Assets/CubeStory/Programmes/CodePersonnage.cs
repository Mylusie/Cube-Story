using UnityEngine;

public class VRMovementController : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Vitesse de déplacement globale
    public float forwardSpeedMultiplier = 1.5f; // Facteur de multiplication pour la vitesse en avant

    void Update()
    {
        // Obtient les entrées de déplacement horizontales et verticales
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcul du vecteur de mouvement en fonction des entrées et de la vitesse de déplacement
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;

        // Vérifie si le mouvement est en avant (positive verticalInput)
        if (verticalInput > 0)
        {
            // Si le mouvement est en avant, multiplie la vitesse par le facteur de multiplication
            movement *= forwardSpeedMultiplier;
        }

        // Applique le mouvement au GameObject
        transform.Translate(movement);
    }
}
