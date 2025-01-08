using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // The UI Image where the item's icon will appear
    private string currentItemName;

    public bool IsEmpty => string.IsNullOrEmpty(currentItemName);

    public void SetItem(string itemName, Sprite itemIcon)
    {
        currentItemName = itemName; // Store the item name
        icon.sprite = itemIcon; // Set the icon
        icon.enabled = true; // Make the icon visible
    }

    public void ClearSlot()
    {
        currentItemName = null; // Clear the item name
        icon.sprite = null; // Remove the icon
        icon.enabled = false; // Hide the icon
    }
}
