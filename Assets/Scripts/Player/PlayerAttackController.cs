using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public Animator AttackAnimator;

    void Start()
    {
        // Ensure Animator is assigned (either via Inspector or by GetComponent)
        if (AttackAnimator == null)
        {
            AttackAnimator = GetComponent<Animator>();
        }

        if (AttackAnimator == null)
        {
            Debug.LogWarning("Animator not found on GameObject.");
        }
    }

    public void PlayAttackAnim()
    {
        if (AttackAnimator != null)
        {
            AttackAnimator.SetBool("isArmAttacking", true);
            Debug.Log("<color=yellow>Player attack animation triggered.</color>");
            StartCoroutine(ResetAttackTrigger());
        }
        else
        {
            Debug.LogWarning("Animator component is missing!");
        }
    }

    private IEnumerator ResetAttackTrigger()
    {
        yield return new WaitForSeconds(0.28f); // Wait for animation (28 frames ≈ 0.28s at 60fps)
        AttackAnimator.SetBool("isArmAttacking", false);
    }
}
