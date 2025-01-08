using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName; // The name of the item (e.g., "Food")
    public Sprite itemIcon; // The icon representing the item

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))  // Ensure the player has the "Player" tag
        {

            InventoryManager inventoryManager = other.GetComponent<InventoryManager>();

            if (inventoryManager != null)
            {
                inventoryManager.AddItemToInventory(itemName, itemIcon);
                
                Destroy(gameObject);
                Debug.Log("Food picked up and destroyed.");
            }
            else
            {
                Debug.Log("InventoryManager not found on player.");
            }
        }
    }
}
