using UnityEngine;

public class AttackCollsison : MonoBehaviour
{
    private void Start()
    {
        print("AttackCollision script started");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            Debug.Log("Colliding withdd Player");
        }
    }


}
