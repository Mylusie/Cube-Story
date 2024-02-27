using UnityEngine;

public class XRCharacterController : MonoBehaviour
{
    public float speed = 3.0f; // Vitesse de d�placement
    public KeyCode moveForwardKey = KeyCode.W; // Touche pour avancer
    public KeyCode moveBackwardKey = KeyCode.S; // Touche pour reculer
    public KeyCode strafeLeftKey = KeyCode.A; // Touche pour aller � gauche
    public KeyCode strafeRightKey = KeyCode.D; // Touche pour aller � droite

    private CharacterController characterController; // R�f�rence au CharacterController
    private Vector3 moveDirection = Vector3.zero; // Direction de d�placement
    private Transform cameraTransform; // R�f�rence � la transform de la cam�ra

    void Start()
    {
        // R�cup�rer le CharacterController attach� � l'objet
        characterController = GetComponent<CharacterController>();
        // R�cup�rer la transform de la cam�ra
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // R�initialiser la direction de d�placement
        moveDirection = Vector3.zero;

        // D�finir la direction de d�placement en fonction des touches enfonc�es
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

        // R�initialiser la composante Y de la direction de d�placement � z�ro
        moveDirection.y = 0;

        // D�placer l'objet XR origin en fonction de la direction et de la vitesse
        characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Emp�cher le mouvement dans la direction de la collision
        if (Vector3.Dot(moveDirection.normalized, -hit.normal) > 1f)
        {
            moveDirection = Vector3.zero;
        }
    }
}
