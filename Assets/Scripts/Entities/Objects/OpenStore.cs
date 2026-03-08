using DG.Tweening;
using Game.Entities.Pawns;
using Game.Services.InputSystem;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class OpenStore : MonoBehaviour, IInteractable
    {
        [SerializeField] private Discovery open;
        [SerializeField] private Transform button;
        [SerializeField] private float duration;
        [SerializeField] private float up = 0.1f;
        [SerializeField] private float dawn = 0.08f;

        private bool isPressed;

        public InteractionResult Interact(Pawn pawn, InteractionMode mode)
        {
            button.DOKill();
            button.DOLocalMoveY(!isPressed ? dawn : up, duration);
            isPressed = !isPressed;
            open.SendEventMessage();

            return InteractionResult.Success;
        }
    }
}