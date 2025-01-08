using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string itemName; // Name of the item
    public Sprite itemIcon; // Icon representing the item
    public Image iconImage; // UI image to display the item icon

    public bool IsEmpty => string.IsNullOrEmpty(itemName);

    // Method to set the item in the slot
    public void SetItem(string newItemName, Sprite newItemIcon)
    {
        itemName = newItemName;
        itemIcon = newItemIcon;
        iconImage.sprite = itemIcon;
        iconImage.enabled = true; // Make the icon visible
    }

    public void ClearSlot()
    {
        itemName = null;
        itemIcon = null;
        iconImage.sprite = null;
        iconImage.enabled = false; // Hide the icon
    }

    // Method to use the item
    public void UseItem()
    {
        if (itemName == "Food")
        {
            // Call method to decrease hunger, assuming a HungerBarScript exists
            FindObjectOfType<HungerBarScript>().DepleteHunger(-10); // Example: Decrease hunger
        }

        // Remove the item after using it
        ClearSlot();
    }
}
