using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public int currentPlayerIndex;
    GameObject playerSpawnPoint;
    public GameObject[] players;

    private void OnEnable()
    {
        playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
        if (playerSpawnPoint == null)
        {
            Debug.LogError("PlayerSpawnPoint not found in scene.");
            return;
        }

        // Get selected player index from PlayerPrefs
        currentPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);
        Debug.Log("Selected Player Index: " + currentPlayerIndex);

        // Deactivate all players first
        foreach (GameObject player in players)
        {
            if (player != null)
                player.SetActive(false);
        }

        // Only activate and place players[0]
        if (players.Length > 0 && players[0] != null)
        {
            players[0].SetActive(true);
            players[0].transform.position = playerSpawnPoint.transform.position;
            players[0].transform.rotation = playerSpawnPoint.transform.rotation;
        }
        else
        {
            Debug.LogError("players[0] is not assigned.");
        }
    }
}
