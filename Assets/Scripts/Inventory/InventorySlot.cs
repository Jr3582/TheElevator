using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string itemName; // Name of the item
    public Sprite itemIcon; // Icon representing the item
    public Image iconImage; // UI image to display the item icon

    public bool IsEmpty => string.IsNullOrEmpty(itemName);

    public void SetItem(string newItemName, Sprite newItemIcon)
    {
        itemName = newItemName;
        itemIcon = newItemIcon;
        iconImage.sprite = itemIcon;
        iconImage.enabled = true;
    }

    public void ClearSlot()
    {
        itemName = null;
        itemIcon = null;
        iconImage.sprite = null;
        iconImage.enabled = false;
    }

    // Method to use the item
    public void UseItem()
    {
        if (itemName == "Food")
        {
            FindObjectOfType<HealthScript>().DepleteHealth(-1);
            FindObjectOfType<HungerBarScript>().DepleteHunger(-10);
        }

        ClearSlot();
    }
}
