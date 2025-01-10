using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBarScript : MonoBehaviour
{
    public float maxHunger = 100f;
    private float currentHunger;
    public List<Sprite> hungerBarSprites;
    public Image hungerBarImage;

    void Start()
    {
        currentHunger = maxHunger;
        StartCoroutine(LoseHungerEvery5Sec());
        UpdateHungerBar();
    }

    private IEnumerator LoseHungerEvery5Sec() {
        while (true) {

            DepleteHunger(2f);

            yield return new WaitForSeconds(10f);
        }
    }

    void Update()
    {
        UpdateHungerBar();
    }

    public void DepleteHunger(float amount)
    {
        currentHunger -= amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }

    private void UpdateHungerBar()
    {
        int index = GetHungerBarImageIndex();

        if (index >= 0 && index < hungerBarSprites.Count)
        {
            hungerBarImage.sprite = hungerBarSprites[index];
        }
    }

    private int GetHungerBarImageIndex()
    {
        if (currentHunger > 87.5 && currentHunger <= 100) return 0;
        if (currentHunger > 75 && currentHunger <= 87.5) return 1;
        if (currentHunger > 62.5 && currentHunger <= 75) return 2;
        if (currentHunger > 50 && currentHunger <= 62.5) return 3;
        if (currentHunger > 37.5 && currentHunger <= 50) return 4;
        if (currentHunger > 25 && currentHunger <= 37.5) return 5;
        if (currentHunger > 12.5 && currentHunger <= 25) return 6;
        return 7;
    }

    public void ResetHunger()
    {
        currentHunger = maxHunger;
        UpdateHungerBar();
    }
}
