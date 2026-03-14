using Game.Entities.Pawns;
using UnityEngine;

namespace Game.Entities.Objects
{
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        public abstract InteractionResult Interact(Pawn pawn, InteractionMode mode);
    }
}
