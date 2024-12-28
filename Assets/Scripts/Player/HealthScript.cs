using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float maxHealth = 6f;
    private float currentHealth;
    public List<Sprite> healthSprites;
    public Image HealthBarImage1;
    public Image HealthBarImage2;
    public Image HealthBarImage3;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
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
}
