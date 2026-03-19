using Game.Core.Controllable;
using Game.Entities.Pawns.Player;
using Game.Services.InputSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Entities.Player
{
    public interface ICameraProvider
    {
        Transform CameraTransform { get; }
    }

    public class PlayerEntity : MonoBehaviour, ICameraProvider
    {
        [SerializeField] private LayerMask interactionMask;

        private IBody currentBody;
        private Camera mainCamera;
        private PlayerInputs inputs;
        private PlayerInteraction playerInteraction;

        public PlayerInputs Inputs => inputs;
        public Transform CameraTransform { get; private set; }

        private void Awake()
        {
            mainCamera = Camera.main;
            inputs = new();
            playerInteraction = new();

            CameraTransform = mainCamera.transform;
        }

        private void OnEnable() => inputs.Enable();

        private void OnDisable()
        {
            inputs.Disable();
            UnsubscribeFromInputs();
        }

        private void FixedUpdate()
        {
            playerInteraction.UpdateRay(mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)));
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