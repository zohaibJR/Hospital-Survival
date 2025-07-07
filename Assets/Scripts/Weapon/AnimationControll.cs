using UnityEngine;
using System.Collections;

public class AnimationControll : MonoBehaviour
{
    public Animator HammerAnim;
    public Animator ArmAnim;
    void Start()
    {
        if (HammerAnim == null)
        {
            HammerAnim = GetComponent<Animator>();
        }
    }

    public void PlayHammerAttackAnim()
    {
        if (HammerAnim != null)
        {
            HammerAnim.SetBool("isAttackingHammer", true);
            HammerAnim.SetBool("isArmAttacking", true);
            Debug.Log("<color=yellow>Hammer attack animation triggered.</color>");

            // Start coroutine to reset the flag
            StartCoroutine(ResetAttackTrigger());
        }
        else
        {
            Debug.LogWarning("Animator component is not assigned or found on the GameObject.");
        }
    }

    IEnumerator ResetAttackTrigger()
    {
        // Wait a small amount of time so the animation can play
        yield return new WaitForSeconds(0.1f);
        HammerAnim.SetBool("isArmAttacking", false);
        HammerAnim.SetBool("isAttackingHammer", false);
    }
}
