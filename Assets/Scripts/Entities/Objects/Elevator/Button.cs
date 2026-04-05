using Game.Entities.Interactables;
using Game.Entities.Pawns.Player;
using System;
using UnityEngine;

namespace Game.Entities.Objects.Elevator
{
    public class Button : InteractableObject
    {
        public event Action<int> Pressed;

        [SerializeField] private int value;

        public override InteractionResult Interact(InteractionContext context, InteractionMode mode)
        {
            Pressed?.Invoke(value);

            return InteractionResult.Success;
        }
    }
}