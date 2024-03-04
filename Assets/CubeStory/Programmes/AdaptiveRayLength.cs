using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;


public class AdaptiveRayLength : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public float maxRayLength = 10f;

    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * maxRayLength  , Color.red);

        if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            float distance = Mathf.Min(maxRayLength, hit.distance);
            Vector3 origin = hit.point - rayInteractor.attachTransform.forward * distance;
            Vector3 direction = rayInteractor.attachTransform.forward;
            // Utilisez maintenant origin et direction pour afficher le rayon avec la longueur adaptée.
        }
        else
        {
            Vector3 origin = rayInteractor.attachTransform.position;
            Vector3 direction = rayInteractor.attachTransform.forward;
            // Utilisez maintenant origin et direction pour afficher le rayon avec la longueur maximale.
        }
    }
}