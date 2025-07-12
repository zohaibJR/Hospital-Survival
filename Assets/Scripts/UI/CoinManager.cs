using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public CoinDisplay coinDisplay; // Drag your CoinDisplay GameObject in Inspector

    void Start()
    {
        Debug.Log("Coin Manager Started");

        if (!PlayerPrefs.HasKey("TotalCoin"))
        {
            SetGameStartCoins();
        }
    }

    public void SetGameStartCoins()
    {
        PlayerPrefs.SetInt("TotalCoin", 800);
        PlayerPrefs.Save();
        Debug.Log("Game start coins set to 500");

        Debug.Log("Total Coins: " + PlayerPrefs.GetInt("TotalCoin"));

        coinDisplay?.UpdateCoinText();
    }

    public void AddFiftyCoins()
    {
        int coins = PlayerPrefs.GetInt("TotalCoin");
        coins += 50;
        PlayerPrefs.SetInt("TotalCoin", coins);
        Debug.Log("Added 50 coins");
        PlayerPrefs.Save();

        coinDisplay?.UpdateCoinText(); // ✅ Refresh the UI
    }

    public void RemoveCoins(int P_coins)
    {

        int coins = PlayerPrefs.GetInt("TotalCoin");
        coins = coins - P_coins;
        PlayerPrefs.SetInt("TotalCoin", coins);
        Debug.Log("Removed 500 coins");
        PlayerPrefs.Save();

        coinDisplay?.UpdateCoinText(); // ✅ Refresh the UI
    }


}
