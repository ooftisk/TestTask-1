using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    private BaseItem _item;
    private int _quantity;

    public InventorySlot(BaseItem item, int quantity)
    {
        _item = item;
        _quantity = quantity;
    }
    
    public InventorySlot()
    {
        _item = null;
        _quantity = 0;
    }

    public BaseItem GetItem() { return _item; }
    public int GetQuantity() { return _quantity; }

    public void AddQuantity(int quantity) { _quantity += quantity; }
    public void SubQuantity(int quantity) { _quantity -= quantity; }
}
