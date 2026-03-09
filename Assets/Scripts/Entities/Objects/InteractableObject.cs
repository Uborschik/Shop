using Game.Entities.Pawns;
using Game.Services.InputSystem;
using UnityEngine;

namespace Game.Entities.Objects
{
    public abstract class InteractableObject : Entity, IInteractable
    {
        public abstract InteractionResult Interact(Pawn pawn, InteractionMode mode);
    }
}
