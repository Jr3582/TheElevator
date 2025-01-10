using UnityEngine;

public class EndLevelDoor : MonoBehaviour
{
    public GameObject endLevelGUI; // Assign the GUI GameObject in the Inspector
    private bool isPlayerNearby = false; // Tracks if the player is near the door

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            isPlayerNearby = true; // Set the flag to true when the player enters the trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            isPlayerNearby = false; // Reset the flag when the player exits the trigger
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // Check if E is pressed and player is nearby
        {
            ShowEndLevelGUI();
        }
    }

    private void ShowEndLevelGUI()
    {
        endLevelGUI.SetActive(true); // Show the GUI
        Time.timeScale = 0; // Pause the game
    }
}
