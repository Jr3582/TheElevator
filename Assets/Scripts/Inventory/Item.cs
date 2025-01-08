using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // Name of the item
    public Sprite itemIcon; // Icon representing the item

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure this is the player
        {

            InventoryManager inventoryManager = other.GetComponent<InventoryManager>();
            if (inventoryManager != null)
            {
                if (inventoryManager.AddItemToInventory(itemName, itemIcon))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
