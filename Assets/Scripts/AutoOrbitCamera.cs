using UnityEngine;

public class AutoOrbitCamera : MonoBehaviour
{
    public Transform target;        // Player to orbit around
    public float distance = 5.0f;   // Distance from player
    public float height = 2.0f;     // Height above player
    public float orbitSpeed = 20.0f; // Degrees per second

    private float currentAngle = 0;

    void LateUpdate()
    {
        if (target == null) return;

        // Update the current angle based on time
        currentAngle += orbitSpeed * Time.deltaTime;
        if (currentAngle >= 360f) currentAngle -= 360f;

        // Convert angle to radians
        float radians = currentAngle * Mathf.Deg2Rad;

        // Calculate new position around the player
        float x = target.position.x + distance * Mathf.Sin(radians);
        float z = target.position.z + distance * Mathf.Cos(radians);
        float y = target.position.y + height;

        // Apply new position
        transform.position = new Vector3(x, y, z);

        // Always look at the player
        transform.LookAt(target);
    }
}
