using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Sprite itemIcon;
    public string itemName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered by: {other.name}"); // Debug log for collision detection
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected."); // Debug log for player detection
            InventoryManager inventory = other.GetComponent<InventoryManager>();
            if (inventory != null)
            {
                Debug.Log("Inventory Manager found."); // Debug log for inventory detection
                if (inventory.AddItem(itemName, itemIcon))
                {
                    Debug.Log($"Picked up {itemName}"); // Debug log for successful pickup
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventory is full!");
                }
            }
            else
            {
                Debug.Log("Inventory Manager not found on Player.");
            }
        }
    }
}
