using Game.Entities.Interactables;
using Game.Entities.Pawns.Player;
using UnityEngine;

namespace Game.Entities.Objects
{
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        public abstract InteractionResult Interact(InteractionContext context, InteractionMode mode);
    }
}
