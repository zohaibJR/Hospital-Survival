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
        int randomDamage = Random.value < 0.5f ? 3 : 6;
        maxHealth -= randomDamage;

        _bloodanimObj.PlayScreenBloodAnim();
        Debug.Log("<color=green>Player Health: " + maxHealth + " (Damage: " + randomDamage + ")</color>");
    }


}
