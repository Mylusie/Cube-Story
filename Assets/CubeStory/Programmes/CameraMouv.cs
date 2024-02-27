using UnityEngine;

public class KeyboardCameraMovement : MonoBehaviour
{
    public float speed = 1.0f; // Vitesse de d�placement de la cam�ra
    public KeyCode moveForwardKey = KeyCode.W; // Touche pour avancer
    public KeyCode moveBackwardKey = KeyCode.S; // Touche pour reculer
    public KeyCode strafeLeftKey = KeyCode.A; // Touche pour aller � gauche
    public KeyCode strafeRightKey = KeyCode.D; // Touche pour aller � droite

    void Update()
    {
        // Stocke la direction du d�placement
        Vector3 moveDirection = Vector3.zero;

        // D�placement vers l'avant
        if (Input.GetKey(moveForwardKey))
        {
            moveDirection += transform.forward;
        }

        // D�placement vers l'arri�re
        if (Input.GetKey(moveBackwardKey))
        {
            moveDirection += -transform.forward;
        }

        // D�placement lat�ral gauche
        if (Input.GetKey(strafeLeftKey))
        {
            moveDirection += -transform.right;
        }

        // D�placement lat�ral droit
        if (Input.GetKey(strafeRightKey))
        {
            moveDirection += transform.right;
        }

        // Normalize la direction pour �viter un mouvement plus rapide en diagonale
        moveDirection.Normalize();

        // V�rifie s'il y a une collision avant de d�placer la cam�ra
        float moveDistance = speed * Time.deltaTime;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, moveDistance))
        {
            // Ajuste la distance de d�placement en fonction de la collision
            moveDistance = hit.distance;
        }

        // D�place la cam�ra en fonction de la direction et de la distance de d�placement
        transform.Translate(moveDirection * moveDistance, Space.World);
    }
}
