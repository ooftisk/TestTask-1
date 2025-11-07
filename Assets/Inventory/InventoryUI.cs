using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Button _openInventoryButton;
    [SerializeField] private GameObject _inventoryPanel;
    
    // Start is called before the first frame update
    public void OpenInventory()
    {
        _openInventoryButton.gameObject.SetActive(false);
        _inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        _inventoryPanel.SetActive(false);
        _openInventoryButton.gameObject.SetActive(true);
    }
   
}
