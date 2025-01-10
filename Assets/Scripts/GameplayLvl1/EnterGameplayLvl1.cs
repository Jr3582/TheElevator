using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGameplayLvl1 : MonoBehaviour {
    public string nextSceneName;
    private bool isNearDoor = false;
    public Vector3 spawnPositionInNextScene;

    private void Update() {
        if (isNearDoor && Input.GetKeyDown(KeyCode.F)) {
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager != null)
            {
                spawnManager.SetSpawnPosition(spawnPositionInNextScene);
            }
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Door")) {
            isNearDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Door")) {
            isNearDoor = false;
        }
    }

    private void LoadNextScene() {
        SceneManager.LoadScene(nextSceneName);
    }
}
