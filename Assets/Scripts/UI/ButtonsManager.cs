using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public AnimationControll animationcontrollerObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackButtonPressed()
    {
        animationcontrollerObject.PlayHammerAttackAnim();
        Debug.Log("Attack button pressed");
    }

    public void ComboButtonPressed()
    {
        Debug.Log("Combo button pressed");
    }
}
