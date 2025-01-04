using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Start()
    {
        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager != null)
        {
            Vector3 spawnPosition = spawnManager.GetSpawnPosition();

            GameObject player = GameObject.FindWithTag("Player");

            if (player == null && playerPrefab != null)
            {
                player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            }

            if (player != null)
            {
                player.transform.position = spawnPosition;
            }
        }
    }
}
