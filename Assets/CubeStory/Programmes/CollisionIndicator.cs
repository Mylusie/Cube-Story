using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionIndicator : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public GameObject collisionIndicatorPrefab;
    private GameObject collisionIndicator;

    void Update()
    {
        RaycastHit hit;
        if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            if (collisionIndicatorPrefab != null)
            {
                if (collisionIndicator == null)
                {
                    collisionIndicator = Instantiate(collisionIndicatorPrefab, hit.point, Quaternion.identity);
                }
                else
                {
                    collisionIndicator.transform.position = hit.point;

                    // Calculate rotation based on the normal of the collided object
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    collisionIndicator.transform.rotation = rotation;
                }
            }
        }
        else
        {
            if (collisionIndicator != null)
            {
                Destroy(collisionIndicator);
                collisionIndicator = null;
            }
        }
    }
}
