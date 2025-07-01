using Unity.VisualScripting;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public int currentPlayerIndex;
    GameObject playerSpawnPoint;
    public GameObject[] players;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
        Debug.Log("Player Spawn Point :- "+ playerSpawnPoint);

        currentPlayerIndex= PlayerPrefs.GetInt("SelectedPlayer", 0);
        foreach(GameObject player in players)
        {
            player.SetActive(false);
        }

        players[currentPlayerIndex].SetActive(true);
        players[currentPlayerIndex].gameObject.transform.position = playerSpawnPoint.gameObject.transform.position;
        players[currentPlayerIndex].gameObject.transform.rotation = playerSpawnPoint.gameObject.transform.rotation;
    }
}
