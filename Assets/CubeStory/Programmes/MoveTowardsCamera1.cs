using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveAndRotateTowardsCamera1 : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera's transform
    public float speed = 1.0f; // Movement speed
    public float stopDistance = 0.1f; // Minimum distance to consider reaching the camera
    
    void Update()
    {
        // Calculate target position with only X and Z from camera
        Vector3 targetPosition = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);

        // Move towards target position with a limit on distance
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }




         // Get direction towards camera (ignoring Y)
        Vector3 direction = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z) - transform.position;

        // Calculate target rotation on Y axis only
        float targetRotationY = Mathf.Atan2(-direction.z, -direction.x) * Mathf.Rad2Deg;

        // Set object rotation with forced X and Z rotations
        transform.rotation = Quaternion.Euler(0f, targetRotationY, 0f);
    
        }
}
