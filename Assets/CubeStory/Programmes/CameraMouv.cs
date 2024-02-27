using UnityEngine;

public class KeyboardCameraMovement : MonoBehaviour
{
    public float speed = 1.0f; // Vitesse de déplacement de la caméra
    public KeyCode moveForwardKey = KeyCode.W; // Touche pour avancer
    public KeyCode moveBackwardKey = KeyCode.S; // Touche pour reculer
    public KeyCode strafeLeftKey = KeyCode.A; // Touche pour aller à gauche
    public KeyCode strafeRightKey = KeyCode.D; // Touche pour aller à droite

    void Update()
    {
        // Stocke la direction du déplacement
        Vector3 moveDirection = Vector3.zero;

        // Déplacement vers l'avant
        if (Input.GetKey(moveForwardKey))
        {
            moveDirection += transform.forward;
        }

        // Déplacement vers l'arrière
        if (Input.GetKey(moveBackwardKey))
        {
            moveDirection += -transform.forward;
        }

        // Déplacement latéral gauche
        if (Input.GetKey(strafeLeftKey))
        {
            moveDirection += -transform.right;
        }

        // Déplacement latéral droit
        if (Input.GetKey(strafeRightKey))
        {
            moveDirection += transform.right;
        }

        // Normalize la direction pour éviter un mouvement plus rapide en diagonale
        moveDirection.Normalize();

        // Vérifie s'il y a une collision avant de déplacer la caméra
        float moveDistance = speed * Time.deltaTime;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, moveDistance))
        {
            // Ajuste la distance de déplacement en fonction de la collision
            moveDistance = hit.distance;
        }

        // Déplace la caméra en fonction de la direction et de la distance de déplacement
        transform.Translate(moveDirection * moveDistance, Space.World);
    }
}
