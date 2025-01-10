using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public float maxHealth = 6f;
    private float currentHealth;
    public List<Sprite> healthSprites;
    public Image HealthBarImage1;
    public Image HealthBarImage2;
    public Image HealthBarImage3;
    public GameObject gameOverPanel;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        UpdateHealthBar();
    }

    public void DepleteHealth(float amount)
    {
        Debug.Log(currentHealth);
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            TriggerGameOver();
        }
    }

    private void UpdateHealthBar()
    {
        int index1 = GetHealthBarImageIndex(1);
        int index2 = GetHealthBarImageIndex(2);
        int index3 = GetHealthBarImageIndex(3);

        HealthBarImage1.sprite = healthSprites[index1];
        HealthBarImage2.sprite = healthSprites[index2];
        HealthBarImage3.sprite = healthSprites[index3];
    }

    private int GetHealthBarImageIndex(int heartNumber)
    {
        float healthPerHeart = maxHealth / 3; 
        float healthThreshold = heartNumber * healthPerHeart;

        if (currentHealth >= healthThreshold)
        {
            return 0;
        }
        else if (currentHealth >= (healthThreshold - 1))
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
        private void TriggerGameOver()
    {
        Time.timeScale = 0; // Pause the game
        gameOverPanel.SetActive(true); // Show the game over panel
    }
        public void ReplayLevel()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }
        public void BackToMainMenu()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }
}
