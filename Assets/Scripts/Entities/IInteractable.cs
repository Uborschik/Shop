using Game.Entities.Tools;

namespace Game.Entities
{
    public interface IInteractable
    {
        void Interact(ref TraderTool tool);
    }
}
