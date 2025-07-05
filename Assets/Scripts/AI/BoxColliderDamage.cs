using UnityEngine;

public class BoxColliderDamage : MonoBehaviour
{

    public PlayerHealth playerHealthObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            Debug.Log("<color=green>Player has entered the box collider area.</color>");
            playerHealthObj.TakeAttackDamage();
        }
    }
}
