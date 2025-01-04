using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3 playerSpawnPosition;

    private static SpawnManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawnPosition(Vector3 position)
    {
        playerSpawnPosition = position;
    }

    public Vector3 GetSpawnPosition()
    {
        return playerSpawnPosition;
    }
}
