using Game.Services.Inventory;
using UnityEngine;

namespace Game.Entities.Tools
{
    public class TraderTool : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public LayerMask InteractionMask { get; private set; }

        public void Interact(ref TraderTool tool)
        {
            var parent = tool.transform.parent;
            transform.parent = parent;
            transform.SetPositionAndRotation(parent.position, parent.rotation);
            Destroy(tool.gameObject);

            tool = this;
        }
    }
}
