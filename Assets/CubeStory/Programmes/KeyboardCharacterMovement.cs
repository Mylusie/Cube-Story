using UnityEngine;

public class XRCharacterController : MonoBehaviour
{
    public float speed = 3.0f; // Vitesse de déplacement
    public KeyCode moveForwardKey = KeyCode.W; // Touche pour avancer
    public KeyCode moveBackwardKey = KeyCode.S; // Touche pour reculer
    public KeyCode strafeLeftKey = KeyCode.A; // Touche pour aller à gauche
    public KeyCode strafeRightKey = KeyCode.D; // Touche pour aller à droite

    private CharacterController characterController; // Référence au CharacterController
    private Vector3 moveDirection = Vector3.zero; // Direction de déplacement
    private Transform cameraTransform; // Référence à la transform de la caméra

    void Start()
    {
        // Récupérer le CharacterController attaché à l'objet
        characterController = GetComponent<CharacterController>();
        // Récupérer la transform de la caméra
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Réinitialiser la direction de déplacement
        moveDirection = Vector3.zero;

        // Définir la direction de déplacement en fonction des touches enfoncées
        if (Input.GetKey(moveForwardKey))
        {
            moveDirection += cameraTransform.forward;
        }

        if (Input.GetKey(moveBackwardKey))
        {
            moveDirection -= cameraTransform.forward;
        }

        if (Input.GetKey(strafeLeftKey))
        {
            moveDirection -= cameraTransform.right;
        }

        if (Input.GetKey(strafeRightKey))
        {
            moveDirection += cameraTransform.right;
        }

        // Réinitialiser la composante Y de la direction de déplacement à zéro
        moveDirection.y = 0;

        // Déplacer l'objet XR origin en fonction de la direction et de la vitesse
        characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Empêcher le mouvement dans la direction de la collision
        if (Vector3.Dot(moveDirection.normalized, -hit.normal) > 1f)
        {
            moveDirection = Vector3.zero;
        }
    }
}
