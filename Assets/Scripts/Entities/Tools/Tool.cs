using Game.Entities.Pawns;
using UnityEngine;

namespace Game.Entities.Tools
{
    public abstract class Tool : Entity, IInteractable
    {
        [field: SerializeField] public float RayDistance { get; protected set; }
        [field: SerializeField] public LayerMask InteractionMask { get; protected set; }

        public void Use(IInteractable interactable, Pawn pawn, InteractionMode mode)
        {
            interactable?.Interact(pawn, mode);
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
