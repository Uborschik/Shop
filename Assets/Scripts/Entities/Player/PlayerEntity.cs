using Game.Core.Controllable;
using Game.Core.Possession;
using Game.Entities.Pawns.Player;
using Game.Services.InputSystem;
using UnityEngine;
using VContainer;

namespace Game.Entities.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private PlayerCameraConfig cameraConfig;
        [SerializeField] private LayerMask interactionMask;

        private PlayerInputs inputs;
        private PlayerCamera playerCamera;
        private PlayerInteraction playerInteraction;
        private IBody currentBody;

        public PlayerCamera PlayerCamera => playerCamera;
        public IBody CurrentBody => currentBody;
        public Vector3 LookDirection => playerCamera.Transform.forward;
        public Ray LookRay => new(playerCamera.Transform.position, playerCamera.Transform.forward);

        private void Awake()
        {
            inputs = new();
            playerCamera = new(cameraConfig);
            playerInteraction = new();
        }

        private void OnEnable()
        {
            inputs.Enable();
        }

        private void OnDisable()
        {
            inputs.Disable();

            inputs.Drop -= playerInteraction.DropItem;
            inputs.Interact -= playerInteraction.OnInteract;
            inputs.AltInteract -= playerInteraction.OnInteract;
        }

        public void OnFixedUpdate()
        {
            playerInteraction.UpdateRay(LookRay);
        }

        public void OnLateUpdate()
        {
            playerCamera.LateTick(inputs.MouseDelta);
        }

        public void PossessBody(IBody body)
        {
            if (currentBody != null)
                UnsubscribeFromInputs();

            currentBody = body;
            playerCamera.AttachTo(body);

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
    }
}