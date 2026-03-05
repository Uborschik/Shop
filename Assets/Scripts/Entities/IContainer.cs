using Game.Entities.Items;

namespace Game.Entities
{
    public interface IContainer
    {
        bool TryPushItem(Item item);
        bool TryPullItem(out Item item);
    }
}
