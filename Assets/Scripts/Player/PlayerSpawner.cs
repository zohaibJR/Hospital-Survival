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

        // Instantiate selected player
        GameObject selectedPlayer = Instantiate(playerSelector.players[index], spawnPoint.transform.position, spawnPoint.transform.rotation);
        selectedPlayer.name = "Player_" + index;
    }
}
