using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;

    void Start()
    {
        Debug.Log("Player health Script Started");
        int selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer", 0);

        switch (selectedPlayer)
        {
            case 0:
                maxHealth = 150f;
                break;
            case 1:
                maxHealth = 200f;
                break;
            case 2:
                maxHealth = 250f;
                break;
            default:
                maxHealth = 150f;
                break;
        }

        Debug.Log("<color=cyan>Selected Player: " + selectedPlayer + " | Max Health: " + maxHealth + "</color>");
    }

    public void TakeAttackDamage()
    {
        int randomDamage = Random.value < 0.5f ? 3 : 6;
        maxHealth -= randomDamage;

        //_bloodanimObj.PlayScreenBloodAnim();
        Debug.Log("<color=green>Player Health: " + maxHealth + " (Damage: " + randomDamage + ")</color>");
    }
}
