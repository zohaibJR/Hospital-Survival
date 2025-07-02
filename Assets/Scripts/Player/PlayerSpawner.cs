using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private PlayerSelector playerSelector;

    void Start()
    {
        // Get PlayerSelector in the scene
        playerSelector = FindObjectOfType<PlayerSelector>();
        if (playerSelector == null)
        {
            Debug.LogError("PlayerSelector not found in scene.");
            return;
        }

        // Get spawn point by tag
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
        if (spawnPoint == null)
        {
            Debug.LogError("No object found with tag 'PlayerSpawnPoint'");
            return;
        }

        int index = PlayerPrefs.GetInt("SelectedPlayer", 0);

        // Safety check
        if (index < 0 || index >= playerSelector.players.Length)
        {
            Debug.LogError("Selected player index out of range.");
            return;
        }

        // Activate and move selected player
        GameObject selectedPlayer = playerSelector.players[index];
        selectedPlayer.SetActive(true);
        selectedPlayer.transform.position = spawnPoint.transform.position;
        selectedPlayer.transform.rotation = spawnPoint.transform.rotation;

        Debug.Log("Activated existing player: " + selectedPlayer.name);
    }
}
