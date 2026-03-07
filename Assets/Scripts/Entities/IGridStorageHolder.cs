using Game.Services.Inventory;

namespace Game.Entities
{
    public interface IGridStorageHolder
    {
        GridStorage Storage { get; }
    }
}