using System.Collections.Generic;
using System;
    [Serializable]
    public class InventoryData
    {
        public List<ItemData> items = new();
    }

    [Serializable]
    public class ItemData
    {
        public string id;
        public int quantity;
    }

