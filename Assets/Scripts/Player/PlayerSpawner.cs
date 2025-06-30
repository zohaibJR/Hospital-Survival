using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Assign all player prefabs in the correct order")]
    public GameObject[] playerPrefabs;

    [Header("Assign spawn point in Inspector")]
    public Transform spawnPoint;

    void Start()
    {
        // Get selected index from PlayerPrefs
        int selectedIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);

        // Check index validity
        if (selectedIndex < 0 || selectedIndex >= playerPrefabs.Length)
        {
            Debug.LogError("Selected player index is out of range!");
            return;
        }

        // Instantiate selected player at spawn point
        Instantiate(playerPrefabs[selectedIndex], spawnPoint.position, spawnPoint.rotation);
    }
}
