using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject StartPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonClicked()
    {
        Debug.Log("Play button clicked");
        // Here you can add code to start the game, load a scene, etc.  
    }

    public void LoadGameScene()
    {
        Debug.Log("Loading Game Scene");
        // Assuming you have a scene named "GameScene" in your build settings
        SceneManager.LoadScene("GamePlayScene");
    }
}
