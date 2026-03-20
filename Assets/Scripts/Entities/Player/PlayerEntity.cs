using Game.Core.Controllable;
using Game.Entities.Pawns.Player;
using Game.Services.InputSystem;
using UnityEngine;

namespace Game.Entities.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private PlayerCameraConfig cameraConfig;

        private IBody currentBody;
        private PlayerInputs inputs;
        private PlayerCamera playerCamera;
        private PlayerInteraction playerInteraction;

        public PlayerInputs Inputs => inputs;

        private void Awake()
        {
            inputs = new();
            playerCamera = new(cameraConfig);
            playerInteraction = new();
        }

        private void OnEnable() => inputs.Enable();

        private void OnDisable()
        {
            inputs.Disable();
            UnsubscribeFromInputs();
        }

        private void FixedUpdate()
        {
            playerInteraction.UpdateRay(playerCamera.Ray);
        }

        private void LateUpdate()
        {
            playerCamera.LateTick(inputs.MouseDelta);
        }

        private void SubscribeToInputs()
        {
            inputs.Drop += playerInteraction.DropItem;
            inputs.Interact += playerInteraction.OnInteract;
            inputs.AltInteract += playerInteraction.OnInteract;
        }

        private void UnsubscribeFromInputs()
        {
            inputs.Drop -= playerInteraction.DropItem;
            inputs.Interact -= playerInteraction.OnInteract;
            inputs.AltInteract -= playerInteraction.OnInteract;
        }

        public void PossessBody(IBody body)
        {
            if (currentBody != null)
                UnsubscribeFromInputs();

            currentBody = body;
            playerCamera.AttachTo(currentBody);

            if (body?.Transform.TryGetComponent(out Hand hand) == true)
            {
                playerInteraction.AttachTo(hand, body);
                SubscribeToInputs();
            }
            else
            {
                Debug.LogWarning($"No Hand found in {body?.Transform.name}");
                playerInteraction.AttachTo(null, body);
            }
        }
    }
}