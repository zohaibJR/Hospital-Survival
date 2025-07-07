using UnityEngine;
using System.Collections;

public class PlayerHammerController : MonoBehaviour
{
    public Animator HammerAnim;

    void Start()
    {
        // Assign Animator if not set in Inspector
        if (HammerAnim == null)
        {
            HammerAnim = GetComponent<Animator>();
        }

        if (HammerAnim == null)
        {
            Debug.LogWarning("Animator not found on GameObject.");
        }
    }

    public void PlayHammerAttackAnim()
    {
        if (HammerAnim != null)
        {
            HammerAnim.SetBool("isAttackingHammer", true);
            Debug.Log("<color=red>Hammer attack animation triggered.</color>");
            StartCoroutine(ResetHammerTrigger());
        }
        else
        {
            Debug.LogWarning("Animator component is missing!");
        }
    }

    private IEnumerator ResetHammerTrigger()
    {
        yield return new WaitForSeconds(0.28f); // Adjust based on your hammer attack animation
        HammerAnim.SetBool("isAttackingHammer", false);
    }
}
