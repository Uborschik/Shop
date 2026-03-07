using Game.Entities.Items;
using Game.Services.Inventory;
using System;
using UnityEngine;

namespace Game.Entities.Tools
{
    public class ItemTray : TraderTool, IGridStorageHolder
    {
        [SerializeField] private PlacementGrid placement;

        public GridStorage Storage { get; private set; }

        private void Start()
        {
            Storage = new(placement);
        }
    }
}
