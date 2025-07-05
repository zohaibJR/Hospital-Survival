using UnityEngine;

public class BloodAnim : MonoBehaviour
{
    public Animator bloodAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayScreenBloodAnim()
    {
        bloodAnimator.SetTrigger("PlayBlood");
        Debug.Log("<color=red>Blood animation triggered.</color>");

    }
}
