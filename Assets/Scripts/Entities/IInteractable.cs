using Game.Entities.Pawns;

namespace Game.Entities
{
    public enum InteractionMode : byte
    {
        Primary,   // Основной (например, ЛКМ)
        Secondary  // Дополнительный (например, ПКМ)
    }

    public enum InteractionResult : byte
    {
        Success,
        Failure,
        Running
    }

    public interface IInteractable
    {
        InteractionResult Interact(Pawn pawn, InteractionMode mode);
    }
}
