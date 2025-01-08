using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots; // Array to hold all the inventory slots (6 slots for hotbar)
    private int selectedSlotIndex = 0; // Default to the first slot (index 0)
    private Color originalColor; // Store the original color of the slot image

    void Start()
    {
        // Save the original color of the first slot (assumes all slots have the same initial color)
        if (slots.Length > 0)
        {
            originalColor = slots[0].iconImage.color;
        }

        // Set the first slot to be highlighted by default
        HighlightSelectedSlot(selectedSlotIndex);
    }

    void Update()
    {
        HandleSlotSelection();
        UseSelectedItem();
    }

    private void HandleSlotSelection()
    {
        // Listen for number key inputs (1-6)
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Keys 1 to 6
            {
                selectedSlotIndex = i;
                HighlightSelectedSlot(i); // Highlight the selected slot
            }
        }
    }

    private void UseSelectedItem()
    {
        // Use the item from the selected slot when pressing Enter
        if (selectedSlotIndex >= 0 && selectedSlotIndex < slots.Length)
        {
            if (Input.GetKeyDown(KeyCode.Return)) // Press Enter to use the selected item
            {
                if (!slots[selectedSlotIndex].IsEmpty)
                {
                    slots[selectedSlotIndex].UseItem();
                }
            }
        }
    }

    private void HighlightSelectedSlot(int index)
    {
        // Reset all slots to their original color
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].iconImage.color = originalColor; // Revert to the saved color
        }

        // Highlight the selected slot
        slots[index].iconImage.color = new Color(1f, 1f, 1f, 0.5f); // White with half opacity
    }

    // Method to add the item to the first available slot in the inventory
    public bool AddItemToInventory(string itemName, Sprite itemIcon)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty) // Check for the first empty slot
            {
                slots[i].SetItem(itemName, itemIcon); // Assign the item to the slot
                return true; // Successfully added
            }
        }

        return false; // No empty slots
    }
}
