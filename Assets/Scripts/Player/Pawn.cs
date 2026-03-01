using Game.Items;
using Game.Services.InputSystem;
using Game.Services.Storage;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Pawn : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Inventory inventory;
        [SerializeField] private Item prefab;
        [SerializeField] private float speed = 5;
        [SerializeField] private float mouseSensitivity = 0.15f;
        [SerializeField] private float jumpForce = 20f;
        [SerializeField] private float gravityForce = Physics.gravity.y;

        private CharacterController characterController;
        private PlayerInput playerInput;
        private float verticalVelocity;
        private float verticalAngle;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();

            playerInput = new();
        }

        private void Start()
        {
            if (inventory.Count <= 0) return;

            for (int i = 0; i < inventory.Count; i++)
            {
                var obj = Instantiate(prefab);

                if (inventory.CanPush()) inventory.PushItem(obj);
            }
        }

        private void OnEnable()
        {
            playerInput.Jump += PlayerInput_Jump;
        }

        private void OnDisable()
        {
            playerInput.Jump -= PlayerInput_Jump;
        }

        private void PlayerInput_Jump()
        {
            if (characterController.isGrounded)
                verticalVelocity = jumpForce;
        }

        private void Move()
        {
            var input = playerInput.MovementDirection.normalized;

            var forward = mainCamera.transform.forward;
            forward.y = 0;
            forward.Normalize();

            var right = mainCamera.transform.right;
            right.y = 0;
            right.Normalize();

            var horizontalMove = (right * input.x) + (forward * input.y);

            if (characterController.isGrounded && verticalVelocity < 0) verticalVelocity = -1f;
            else verticalVelocity += gravityForce * Time.deltaTime;

            var move = horizontalMove + Vector3.up * verticalVelocity;

            characterController.Move(speed * Time.deltaTime * move);
        }

        private void Look()
        {
            var delta = playerInput.MouseDelta;

            var mouseX = delta.x * mouseSensitivity;
            var mouseY = delta.y * mouseSensitivity;

            transform.Rotate(Vector3.up, mouseX);

            verticalAngle -= mouseY;
            verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);
            mainCamera.transform.localRotation = Quaternion.Euler(verticalAngle, 0f, 0f);
        }

        private void Update()
        {
            Move();
        }

        private void LateUpdate()
        {
            Look();
        }
    }
}
