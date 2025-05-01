using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // The player to follow
    public float smoothSpeed = 0.125f;  // Smoothness of camera movement
    public Vector3 offset = new Vector3(0, 0, -10);  // Keep camera behind

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
