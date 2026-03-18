using Game.Entities.Pawns.Player;

namespace Game.Entities.Interactables
{
    public interface IInteractable
    {
        InteractionResult Interact(InteractionContext context, InteractionMode mode);
    }
}
