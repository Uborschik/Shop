using Game.Player;

namespace Game.Objects
{
    public interface IInteractable
    {
        void Interact(Pawn pawn);
        void AltInteract(Pawn pawn);
    }
}
