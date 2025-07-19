using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShopManager : MonoBehaviour
{
    public int currentPlayerIndex = 0;
    public GameObject[] PlayerModels;

    public PlayerBluePrint[] PlayersDataBP;

    public GameObject[] PlayerDetails;

    public GameObject PlayButton;
    public GameObject UnlockAllPlayerButton;
    public GameObject LockedImage;
    public GameObject PlayerPriceBTN;
    public Text PriceInteger;

    public static PlayerShopManager Pinst;

    private void Awake()
    {
        Pinst = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (PlayerBluePrint Player in PlayersDataBP)
        {
            if (Player.price == 0)
            {
                Player.isUnlocked = true;
            }
            else
            {
                Player.isUnlocked = PlayerPrefs.GetInt(Player.name, 0) == 0 ? false : true;
            }
        }

        int rewardedPlayerIndex = PlayerPrefs.GetInt("RewardedPlayer", -1);

        if (rewardedPlayerIndex >= 0 && rewardedPlayerIndex < PlayersDataBP.Length)
        {
            PlayersDataBP[rewardedPlayerIndex].isUnlocked = true;
            PlayerPrefs.SetInt(PlayersDataBP[rewardedPlayerIndex].name, 1);
        }

        PlayerPrefs.SetInt("SelectedPlayer", 0);
        currentPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);


        foreach (GameObject Player in PlayerModels)
        {
            Player.SetActive(false);
        }
        PlayerModels[currentPlayerIndex].SetActive(true);



        if (PlayerPrefs.GetInt("UnlockedAllPlayer") == 1)
        {
            foreach (PlayerBluePrint Players in PlayersDataBP)
            {
                Players.isUnlocked = true;
            }
        }

        Debug.Log("Rewarded Character thing");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        CheckUnlockButton();

    }

    public void ChangeNext()
    {
        PlayerModels[currentPlayerIndex].SetActive(false);

        currentPlayerIndex = currentPlayerIndex + 1;

        if (currentPlayerIndex >= PlayerModels.Length)
        {
            currentPlayerIndex = 0;
        }

        PlayerModels[currentPlayerIndex].SetActive(true);
        PlayerDetails[currentPlayerIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
        Debug.Log("Current Player Index: " + currentPlayerIndex);
    }

    public void ChangePrevious()
    {
        PlayerModels[currentPlayerIndex].SetActive(false);

        currentPlayerIndex = currentPlayerIndex - 1;

        if (currentPlayerIndex == -1)
        {
            currentPlayerIndex = PlayerModels.Length - 1;
        }

        PlayerModels[currentPlayerIndex].SetActive(true);
        PlayerDetails[currentPlayerIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
    }

    public void UpdateUI()
    {
        PlayerBluePrint b = PlayersDataBP[currentPlayerIndex];

        if (b.isUnlocked)
        {
            Debug.Log("Player is Unlocked");
            //remove lock image
            LockedImage.SetActive(false);

            //Activate Player Button
            PlayButton.SetActive(true);

            PlayerPriceBTN.gameObject.SetActive(false);


        }
        else
        {
            Debug.Log("Player is locked");
            //show lock image
            LockedImage.SetActive(true);

            //Deactivate Player Button
            PlayButton.SetActive(false);

            //Activate Price Button
            PlayerPriceBTN.gameObject.SetActive(true);

            //Set Price Text
            PriceInteger.text = " " + b.price;

            if (b.price <= PlayerPrefs.GetInt("TotalCoin", 0))
            {
                PlayerPriceBTN.GetComponent<Button>().interactable = true;
            }
            else
            {
                PlayerPriceBTN.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void UnlockPlayer()
    {
        PlayerBluePrint b = PlayersDataBP[currentPlayerIndex];

        if (b.price <= PlayerPrefs.GetInt("TotalCoin"))
        {
            PlayerPrefs.SetInt(b.name, 1);
            PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
            b.isUnlocked = true;
            PlayerPrefs.SetInt("TotalCoin", PlayerPrefs.GetInt("TotalCoin") - b.price);
        }
    }



    public void UnlockAllPlayer()
    {
        foreach(PlayerBluePrint Players in PlayersDataBP)
        {
            Players.isUnlocked = true;
        }
        PlayerPrefs.SetInt("UnlockedAllPlayer", 1);
    }

    public void CheckUnlockButton()
    {
        if(PlayerPrefs.GetInt("UnlockedAllPlayer") == 1)
        {
            UnlockAllPlayerButton.SetActive(false);
        }
    }
}
