using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WeaponShopManager : MonoBehaviour
{
    public int currentWeaponIndex = 0;
    public GameObject[] WeaponModels;

    public WeaponBluePrint[] WeaponsDataBP;


    public GameObject LockedImage;
    public GameObject CommingSoonImage;

    public GameObject NextButton;
    public GameObject WeaponPriceBTN;
    public Text PriceInteger;

    public static WeaponShopManager Winst;


    private void Awake()
    {
        Winst = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Weapon Shop Manager Started");
        foreach (WeaponBluePrint Weapons in WeaponsDataBP)
        {
            if(Weapons.WeaponPrice == 0)
            {
                Weapons.WeaponIsUnlocked = true;
            }
            else
            {
                Weapons.WeaponIsUnlocked = PlayerPrefs.GetInt(Weapons.WeaponName, 0) == 0 ? false : true;
            }

            //Rewarded Weapon 
            int rewardedWeaponIndex = PlayerPrefs.GetInt("RewardedWeapon", -1);

            if(rewardedWeaponIndex >= 0 && rewardedWeaponIndex < WeaponsDataBP.Length)
            {
                WeaponsDataBP[rewardedWeaponIndex].WeaponIsUnlocked = true;
                PlayerPrefs.SetInt(WeaponsDataBP[rewardedWeaponIndex].WeaponName, 1);
            }

            PlayerPrefs.SetInt("SelectedWeapon", 0);
            currentWeaponIndex = PlayerPrefs.GetInt("SelectedWeapon", 0);

            foreach(GameObject WeaponsM in WeaponModels)
            {
                WeaponsM.SetActive(false);
            }
            WeaponModels[currentWeaponIndex].SetActive(true);



            if(PlayerPrefs.GetInt("UnlockedAllWeapon") == 1)
            {
                foreach (WeaponBluePrint Weapon in WeaponsDataBP)
                {
                    Weapon.WeaponIsUnlocked = true;
                }
            }
            Debug.Log("Rewarded Weapon thing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWeaponUI();
    }

    public void ChangeNextWeapon()
    {
        WeaponModels[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = currentWeaponIndex + 1;

        if (currentWeaponIndex >= WeaponModels.Length)
        {
            currentWeaponIndex = 0;
        }

        WeaponModels[currentWeaponIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedWeapon", currentWeaponIndex);
        Debug.Log("Current Weapon Index: " + currentWeaponIndex);
    }

    public void ChangePreviousWeapon()
    {
        WeaponModels[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = currentWeaponIndex - 1;

        if (currentWeaponIndex == -1)
        {
            currentWeaponIndex = WeaponModels.Length - 1;
        }

        WeaponModels[currentWeaponIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedWeapon", currentWeaponIndex);
        Debug.Log("Current Weapon Index: " + currentWeaponIndex);
    }

    public void UpdateWeaponUI()
    {
        WeaponBluePrint weapon = WeaponsDataBP[currentWeaponIndex];

        if(weapon.WeaponIsUnlocked)
        {
            Debug.Log("Weapon is Unlocked");

            LockedImage.SetActive(false);
            CommingSoonImage.SetActive(false);


            NextButton.SetActive(true); 

            //price here
            WeaponPriceBTN.SetActive(false);
        }

        else
        {
            Debug.Log("Weapon is locked");
            LockedImage.SetActive(true);
            CommingSoonImage.SetActive(true);
            NextButton.SetActive(false);
            //price here
            WeaponPriceBTN.SetActive(true);
            PriceInteger.text = " " + weapon.WeaponPrice;

            if (weapon.WeaponPrice <= PlayerPrefs.GetInt("TotalCoin", 0))
            {
                WeaponPriceBTN.GetComponent<Button>().interactable = true;
            }
            else
            {
                WeaponPriceBTN.GetComponent<Button>().interactable = false;
            }
        }
    }


}
