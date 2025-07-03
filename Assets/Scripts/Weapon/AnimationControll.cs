using UnityEngine;

public class AnimationControll : MonoBehaviour
{
    public Animator HammerAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HammerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHammerAttackAnim()
    {
        if (HammerAnim != null)
        {
            HammerAnim.SetBool("isAttacking", true);
            Debug.Log("Hammer attack animation triggered.");
            ReturnAnimation();
        }
        else
        {
            Debug.LogWarning("Animator component is not assigned or found on the GameObject.");
        }
    }

    void ReturnAnimation()
    {
        HammerAnim.SetBool("isAttacking", false);
    }


}
