using UnityEngine;
using System.Collections;

public class takeDamageScript : MonoBehaviour
{
    public Animator EnemyAnimator;
    public float EnemyHealth = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        Debug.Log("Enemy Health Script Started");
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Debug.Log("Enemy hit by weapon");

            // Generate a random value between 0 and 1
            float rand = Random.value;

            if (rand < 0.5f)
            {
                EnemyTakeDamage(21);
            }
            else
            {
                EnemyTakeDamage(17);
            }

            //Set Damage Trigger here
            
        }
        Debug.Log("Enemy Health: " + EnemyHealth);
    }

    void EnemyTakeDamage(int number)
    {
        EnemyHealth -= number;

        // Start Damage animation
        EnemyAnimator.SetBool("isDamaged", true);

        // Return to Idle after delay (adjust time based on your animation length)
        StartCoroutine(ResetDamageFlag());

        if(EnemyHealth <= 0)
        {
            EnemyAnimator.SetBool("isDead", true);
            StartCoroutine(DeactivatePlayer());
        }
    }

    private IEnumerator ResetDamageFlag()
    {
        yield return new WaitForSeconds(1f);  // Replace with your TakeDamage animation duration
        EnemyAnimator.SetBool("isDamaged", false);
    }

    private IEnumerator DeactivatePlayer()
    {
        yield return new WaitForSeconds(20f);  // Replace with your TakeDamage animation duration
        Debug.Log("Enemy is dead, deactivating GameObject");
        this.gameObject.SetActive(false);  // Deactivate the enemy GameObject
    }
}
