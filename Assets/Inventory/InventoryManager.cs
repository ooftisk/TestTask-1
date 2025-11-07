
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("TestPurpose")] 
    [SerializeField] private PickableItem _bulletOnStart;
    
    
    public List<InventorySlot> inventory = new List<InventorySlot>();
    

    [Header("Inventory Settings")] 
    public int inventorySize;
    private List<GameObject> slotInstances = new List<GameObject>();
    [SerializeField] private GameObject[] slots;
    
    [Header("UI Ref")]
    public GameObject slotHolder;
    public GameObject slotPrefab;
    public Transform gridContainer;
    
    private void Start()
    {
        GenerateSlots();
        slots = new GameObject[slotHolder.transform.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        AddItem(_bulletOnStart, 20);
        RefreshUI();
    }
    
    
    public bool AddItem(PickableItem item, int amount)
    {
        foreach (InventorySlot slot in inventory)
        {
            if (slot != null && slot.GetItem().GetIsStackable() && slot.GetQuantity() < item.GetMaxQuantity() &&
                slot.GetItem().GetItemName() == item.GetItemName()) // STUPID SOLUTION
            {
                slot.AddQuantity(amount);
                RefreshUI();
                return true;
            }
        }
        
        if (inventory.Count < slots.Length)
            inventory.Add(new InventorySlot(item, amount));
        else
        {
            return false;
        }
        RefreshUI();
        return true;
    }

    public bool RemoveItem(PickableItem item)
    { 
        InventorySlot slotToSubQuantity = Contains(item);
        if (slotToSubQuantity != null)
        {
            if (slotToSubQuantity.GetQuantity() > 1)
            {
                slotToSubQuantity.SubQuantity(1);
            }
            else
            {
                InventorySlot slotToRemove = new InventorySlot();
                foreach (InventorySlot slot in inventory)
                {
                    if (slot.GetItem() == item)
                    {
                        slotToRemove = slot;
                        break;
                    }
                }
                inventory.Remove(slotToRemove);
            }
        }
        else return false;
            
        RefreshUI();
        return true;
    }
    
    public void GenerateSlots()
    {
        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }
        slotInstances.Clear();

        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slot = Instantiate(slotPrefab, gridContainer);
            slot.name = $"Slot_{i + 1}";
            slotInstances.Add(slot);
        }
    }
    
    public void RefreshUI()
    {
        for (int i = 0; i < slotInstances.Count; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].GetItem().GetItemIcon();
                
                if (inventory[i].GetItem().GetIsStackable())
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i].GetQuantity() + "";
                else
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    public InventorySlot Contains(PickableItem item)
    {
        foreach (InventorySlot slot in inventory)
        {
            if (slot.GetItem() == (item))
            {
                return slot;
            }
        }
        return null;
    }
    
    
    public void SaveInventory()
    {
        InventoryData data = new InventoryData();

        foreach (InventorySlot slot in inventory)
        {
            if (slot.GetItem() != null)
            {
                ItemData itemData = new ItemData
                {
                    id = slot.GetItem().GetItemID(), 
                    quantity = slot.GetQuantity()
                };
                data.items.Add(itemData);
            }
        }

        SaveSystem.Save(data, "inventory");
        Debug.Log("Inventory saved!");
    }

    public void LoadInventory()
    {
        if (!SaveSystem.Exists("inventory"))
        {
            Debug.Log("⚠️ No inventory save found.");
            return;
        }

        InventoryData data = SaveSystem.Load<InventoryData>("inventory");
        inventory.Clear();

        foreach (var itemData in data.items)
        {
            PickableItem item = Resources.Load<PickableItem>($"Items/{itemData.id}");

            if (item != null)
                inventory.Add(new InventorySlot(item, itemData.quantity));
            else
                Debug.LogWarning($"Item with ID {itemData.id} not found in Resources/Items/");
        }

        RefreshUI();
        Debug.Log("📂 Inventory loaded!");
    }
}
