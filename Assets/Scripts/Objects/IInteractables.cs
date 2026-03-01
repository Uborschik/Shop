using Game.Player;

namespace Game.Objects
{
    public interface IInteractable
    {
        void Push(Pawn pawn);
        void Pull(Pawn pawn);
    }
}
