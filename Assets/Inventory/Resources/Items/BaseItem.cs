using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    [SerializeField] protected string id;
    [SerializeField] protected string itemName;
    [SerializeField] protected Sprite itemIcon;
    [SerializeField] protected bool isStackable;
    [SerializeField] protected int maxQuantity;
    
    public abstract BaseItem GetItem();
    public abstract Sprite GetItemIcon();
    public abstract int GetMaxQuantity();
    public abstract bool GetIsStackable();
    public abstract string GetItemName();
    public abstract string GetItemID();
}
