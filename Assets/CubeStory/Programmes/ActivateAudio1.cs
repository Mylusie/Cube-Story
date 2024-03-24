using UnityEngine;

public class LancementAudioF : MonoBehaviour
{
    public Transform cameraTransform; // Référence à la transform de la caméra
    public GameObject cylinder; // Le cylindre qui définit les limites
    public GameObject gameObjectToActivate; // Référence au GameObject à activer
    
    void Update()
    {
        Debug.Log(!IsCameraOutsideCylinder());
        // Détecter la sortie du cylindre
        if (!IsCameraOutsideCylinder())
        {
            
           
            gameObjectToActivate.SetActive(true);
            cylinder.SetActive(false);
        }
    }

    bool IsCameraOutsideCylinder()
    {
        // Obtenir le rayon du cylindre (supposant qu'il est aligné avec l'axe Y)
        float cylinderRadius = 15;

        // Obtenir la position de la caméra sur le plan XZ (ignorer la hauteur Y)
        Vector3 cameraPositionXZ = new Vector3(cameraTransform.position.x, 0, cameraTransform.position.z);

        // Obtenir la position du centre du cylindre sur le plan XZ (ignorer la hauteur Y)
        Vector3 cylinderCenterXZ = new Vector3(cylinder.transform.position.x, 0, cylinder.transform.position.z);

        // Calculer la distance entre la caméra et le centre du cylindre sur le plan XZ
        float distanceToCylinderCenter = Vector3.Distance(cameraPositionXZ, cylinderCenterXZ);

        // La caméra est à l'extérieur du cylindre si la distance à son centre est supérieure à son rayon
        bool isOutside = distanceToCylinderCenter > cylinderRadius;

        Debug.Log(distanceToCylinderCenter + "Distance avec le centre du cyclindre");

        Debug.Log(cylinderRadius + "rayon");
        
        
        return isOutside;
    }
}

