using System;

namespace Game.Entities.Items
{
    [Serializable]
    public struct ItemData
    {
        public string Name;
        public int SupplierPrice;
        public int PurchasePrice;
    }
}