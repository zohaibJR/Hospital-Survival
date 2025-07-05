using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public BloodAnim _bloodanimObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeAttackDamage()
    {
        maxHealth = maxHealth - 10;
        _bloodanimObj.PlayScreenBloodAnim();
        Debug.Log("<color=red>Health :- " + maxHealth + "</color>");
    }


}
