using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Pickable Item")]
public class PickableItem : BaseItem
{
    public override BaseItem GetItem()
    {
        return this;
    }

    public override Sprite GetItemIcon()
    {
        return itemIcon;
    }

    public override int GetMaxQuantity()
    {
        return maxQuantity;
    }

    public override bool GetIsStackable()
    {
        return isStackable;
    }

    public override string GetItemName()
    {
        return itemName;
    }

    public override string GetItemID()
    {
        return id;
    }
}
