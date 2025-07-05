using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private SphereCollider handCollider;

    // This will be controlled externally (e.g., from an animation or enemy script)
    public bool isAttacking = false;

    private void Start()
    {
        Debug.Log("AttackCollision script started");

        // Get the sphere collider attached to this object
        handCollider = GetComponent<SphereCollider>();

        // Disable it at the start
        if (handCollider != null)
            handCollider.enabled = false;
    }

    private void Update()
    {
        // Toggle collider based on isAttacking flag
        if (handCollider != null)
        {
            handCollider.enabled = isAttacking;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Use "Player" instead of "Capsule" for consistency
        {
            Debug.Log("Colliding with Player");
        }
    }
}
