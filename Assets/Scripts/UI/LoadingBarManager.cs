using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBarManager : MonoBehaviour
{
    public Image fillImage;
    public float fillduration = 5;

    private float timer = 0f;
    private bool isFilled = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        Debug.Log("Loading Bar Manager Called");
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f; // Start from empty
        }
    }

    void Update()
    {
        if (fillImage == null || isFilled) return;

        timer += Time.deltaTime;
        float progress = Mathf.Clamp01(timer / fillduration);
        fillImage.fillAmount = progress;

        if (progress >= 1f)
        {
            LoadMainMenu();
        }
    }

    //public void ShowAppOpenAdd()
    //{
    //    Debug.Log("App Open Function Called");
    //    AppOpenAdManager.Instance.ShowAppOpen(); // 👈 This line shows the ad

    //    LoadMainMenu();

    //}

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Main Menu Scene Loaded");

    }
}
