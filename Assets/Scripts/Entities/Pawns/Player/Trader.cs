using Game.Services.InputSystem;
using UnityEngine;
using VContainer;

namespace Game.Entities.Pawns.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Trader : Pawn
    {
        [Inject] private readonly PlayerInputs input;
        [Inject] private readonly TraderMovement movement;
        [Inject] private readonly TraderLook look;
        [Inject] private readonly TraderInteraction interaction;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            if (input == null) return;

            input.Jump += OnJump;
            input.Drop += OnDrop;
            input.Interact += OnInteract;
            input.AltInteract += OnInteract;
        }

        private void OnDisable()
        {
            if (input == null) return;

            input.Jump -= OnJump;
            input.Drop -= OnDrop;
            input.Interact -= OnInteract;
            input.AltInteract -= OnInteract;

            input.Dispose();
        }

        private void Update()
        {
            movement.SetMovement(input.MovementDirection);
            movement.Tick();
        }

        private void LateUpdate()
        {
            look.AddLook(input.MouseDelta);
        }

        private void FixedUpdate()
        {
            interaction.RayCast(Hand);
        }

        private void OnJump() => movement?.Jump();
        private void OnDrop() => interaction?.DropItem(Hand);
        private void OnInteract(InteractionMode mode) => interaction?.OnInteract(this, mode);
    }
}