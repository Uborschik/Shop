using Game.Entities.Pawns;
using Game.Services.InputSystem;
using UnityEngine;

namespace Game.Entities.Tools
{
    public abstract class Tool : MonoBehaviour, IInteractable
    {
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask interactionMask;

        public void Use(Transform cameraTransform, Pawn pawn, InteractionMode mode)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, rayDistance, interactionMask))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var pushable))
                {
                    pushable?.Interact(pawn, mode);
                }
            }
        }

        public InteractionResult Interact(Pawn pawn, InteractionMode mode)
        {
            var parent = pawn.ToolHolder.Tool.transform.parent;
            transform.parent = parent;
            transform.SetPositionAndRotation(parent.position, parent.rotation);
            Destroy(pawn.ToolHolder.Tool.gameObject);

            pawn.ToolHolder.SetTool(this);

            return InteractionResult.Success;
        }
    }
}
