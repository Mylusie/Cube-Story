using UnityEngine;
using System.Collections.Generic;

public class CameraBoundsss : MonoBehaviour
{
    public Transform cameraTransform; // Référence à la transform de la caméra
    public GameObject cylinder; // Le cylindre qui définit les limites
    
    void Update()
    {
        // Détecter la sortie du cylindre
        if (IsCameraOutsideCylinder())
        {
            // Désactiver le composant XRCharacterController du GameObject XR Origin
            XRCharacterController characterController = GetComponent<XRCharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
            }
            else
            {
                Debug.LogError("XRCharacterController component not found on XR Origin GameObject.");
            }
        }
    }

    bool IsCameraOutsideCylinder()
    {
        // Obtenir les bounds du cylindre
        Bounds cylinderBounds = cylinder.GetComponent<Collider>().bounds;

        // Déterminer si la position de la caméra est dans les bounds
        return !cylinderBounds.Contains(cameraTransform.position);
    }
}
