using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyboardMoveProvider : ActionBasedContinuousMoveProvider
{
    public float movementSpeed = 1.0f; // Vitesse de déplacement
    public KeyCode moveForwardKey = KeyCode.W; // Touche pour avancer
    public KeyCode moveBackwardKey = KeyCode.S; // Touche pour reculer
    public KeyCode moveLeftKey = KeyCode.A; // Touche pour aller à gauche
    public KeyCode moveRightKey = KeyCode.D; // Touche pour aller à droite

    private Vector2 inputVector; // Vecteur de direction du déplacement

    protected override void Awake()
    {
        base.Awake();
        forwardSource = transform; // La direction de déplacement est toujours vers l'avant de l'objet
    }

    protected override Vector3 ComputeDesiredMove(Vector2 input)
    {
        // Met à jour le vecteur de déplacement en fonction des touches appuyées
        inputVector = Vector2.zero;
        if (Input.GetKey(moveForwardKey)) // Avancer
        {
            inputVector.y = 1.0f;
        }
        else if (Input.GetKey(moveBackwardKey)) // Reculer
        {
            inputVector.y = -1.0f;
        }

        if (Input.GetKey(moveLeftKey)) // Aller à gauche
        {
            inputVector.x = -1.0f;
        }
        else if (Input.GetKey(moveRightKey)) // Aller à droite
        {
            inputVector.x = 1.0f;
        }

        // Calcule le déplacement en fonction du vecteur de direction et de la vitesse
        Vector3 desiredMove = new Vector3(inputVector.x, 0.0f, inputVector.y);
        return desiredMove.normalized * movementSpeed;
    }
}
