using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;
    private int selectedSlotIndex = 0;
    private Color originalColor;

    void Start()
    {
        if (slots.Length > 0)
        {
            originalColor = slots[0].iconImage.color;
        }
    }

    void Update()
    {
        HandleSlotSelection();
        UseSelectedItem();
    }

    private void HandleSlotSelection()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedSlotIndex = i;
                HighlightSelectedSlot(i);
            }
        }
    }

    private void UseSelectedItem()
    {
        if (selectedSlotIndex >= 0 && selectedSlotIndex < slots.Length)
        {
            if (Input.GetKeyDown(KeyCode.Return))
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
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].iconImage.color = originalColor;
        }

        slots[index].iconImage.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public bool AddItemToInventory(string itemName, Sprite itemIcon)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].SetItem(itemName, itemIcon);
                return true;
            }
        }

        return false;
    }
}
