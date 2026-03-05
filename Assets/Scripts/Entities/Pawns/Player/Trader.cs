using Game.Entities.Tools;
using Game.Services.InputSystem;
using UnityEngine;

namespace Game.Entities.Pawns.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Trader : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private MovementConfig movementConfig;
        [SerializeField] private TraderCameraConfig cameraConfig;
        [SerializeField] private float interactionDistance = 2f;

        private PlayerInputs input;

        private TraderMovement movement;
        private TraderLook look;
        private TraderInteraction interaction;

        private void Awake()
        {
            var initialTool = transform.GetComponentInChildren<TraderTool>();
            var characterController = GetComponent<CharacterController>();

            input = new();

            movement = new(() => cameraConfig.TraderCamera.transform, characterController, movementConfig);
            look = new(transform, cameraConfig.TraderCamera.transform, cameraConfig);
            interaction = new(() => cameraConfig.TraderCamera.transform, interactionDistance, initialTool);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            if (input == null) return;

            input.Jump += OnJump;
            input.Interact += OnInteract;
            input.AltInteract += OnAltInteract;
        }

        private void OnDisable()
        {
            if (input == null) return;

            input.Jump -= OnJump;
            input.Interact -= OnInteract;
            input.AltInteract -= OnAltInteract;

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

        private void OnJump() => movement?.Jump();
        private void OnInteract() => interaction?.OnInteract();
        private void OnAltInteract() => interaction?.OnAltInteract();
    }
}