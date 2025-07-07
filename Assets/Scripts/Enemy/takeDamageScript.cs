using UnityEngine;
using System.Collections;

public class takeDamageScript : MonoBehaviour
{
    public Animator EnemyAnimator;
    public float EnemyHealth = 50f;
    private bool isDead = false;
    private AIController aiController;

    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        aiController = GetComponent<AIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && !isDead)
        {
            float rand = Random.value;
            int damage = (rand < 0.5f) ? 21 : 17;

            Debug.Log("Weapon hit detected. Random value: " + rand + ", Damage: " + damage);

            EnemyTakeDamage(damage);
        }
    }

    void EnemyTakeDamage(int amount)
    {
        EnemyHealth -= amount;
        Debug.Log("Enemy took damage: " + amount + ", Remaining Health: " + EnemyHealth);

        if (EnemyHealth <= 0)
        {
            isDead = true;
            EnemyAnimator.SetBool("isDead", true);
            StartCoroutine(DeactivateAfterDeath());
        }
        else
        {
            StartCoroutine(PlayDamageAnimation());
        }
    }

    IEnumerator PlayDamageAnimation()
    {
        // Stop conflicting animations
        if (aiController != null)
        {
            aiController.CancelAttack();
        }

        EnemyAnimator.SetBool("isWalking", false);
        EnemyAnimator.SetBool("isRunning", false);
        EnemyAnimator.SetBool("isAttacking", false);
        EnemyAnimator.SetBool("isDamaged", true);

        yield return new WaitForSeconds(0.7f); // Adjust to damage animation length

        EnemyAnimator.SetBool("isDamaged", false);
    }

    IEnumerator DeactivateAfterDeath()
    {
        yield return new WaitForSeconds(2f); // Adjust to death animation length
        gameObject.SetActive(false);
    }
}
