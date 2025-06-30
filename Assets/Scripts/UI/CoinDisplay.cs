using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinDisplay : MonoBehaviour
{
    public Text coinText;
    private const string CoinKey = "TotalCoin";

    void Start()
    {
        StartCoroutine(DelayedUpdate());
    }

    IEnumerator DelayedUpdate()
    {
        yield return new WaitForEndOfFrame(); // Waits for one frame
        UpdateCoinText();
    }
    public void UpdateCoinText()
    {
        int coins = PlayerPrefs.GetInt("TotalCoin", 0);
        coinText.text = coins.ToString();
    }

}
