using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots; // Array to hold all the inventory slots

    public bool AddItem(string itemName, Sprite itemIcon)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty) // Check if the slot is empty
            {
                slots[i].SetItem(itemName, itemIcon); // Assign the item to the slot
                return true; // Successfully added
            }
        }

        Debug.Log("Inventory is full!");
        return false; // No empty slots
    }
}
