using Game.Services.InputSystem;
using UnityEngine;

namespace Game.Entities.Tools
{
    public abstract class TraderTool : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public LayerMask InteractionMask { get; private set; }

        public void Interact(IInteractor interactor, InteractionMode mode)
        {
            var parent = interactor.ToolHolder.Tool.transform.parent;
            transform.parent = parent;
            transform.SetPositionAndRotation(parent.position, parent.rotation);
            Destroy(interactor.ToolHolder.Tool.gameObject);

            interactor.ToolHolder.SetTool(this);
        }
    }
}
