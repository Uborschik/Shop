using Game.Services.Inventory;
using UnityEngine;

namespace Game.Entities.Tools
{
    public class ItemTray : Tool, IGridStorageHolder
    {
        [SerializeField] private PlacementGrid placement;

        public GridStorage Storage { get; private set; }

        private void Start()
        {
            Storage = new(placement);
        }
    }
}
