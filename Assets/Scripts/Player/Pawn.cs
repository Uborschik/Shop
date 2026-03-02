using Game.Items;
using Game.Services.InputSystem;
using Game.Services.Storage;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Pawn : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Item prefab;
        [SerializeField] private Transform bag;

        [Header("Settings")]
        [SerializeField] private MovementConfig movementConfig;
        [SerializeField] private CameraConfig cameraConfig;
        [SerializeField] private float interactionDistance = 2f;

        private PawnMovement movement;
        private PawnLook look;
        private PawnInteraction interaction;
        private Inventory inventory;
        private PlayerInputs input;

        public Inventory Inventory => inventory;

        private void Awake()
        {
            var characterController = GetComponent<CharacterController>();

            movement = new PawnMovement(() => playerCamera.transform, characterController, movementConfig);
            look = new PawnLook(transform, playerCamera.transform, cameraConfig);
            interaction = new PawnInteraction(this, () => playerCamera.transform, interactionDistance);
            inventory = new Inventory(bag);
            input = new();
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            var start = bag.transform.position;
            var end = start + playerCamera.transform.forward * interactionDistance;
            Gizmos.DrawLine(start, end);
        }
    }
}