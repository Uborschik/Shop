using Game.Entities.Tools;
using UnityEngine;

namespace Game.Entities.Items
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class Item : MonoBehaviour, IInteractable
    {
        private Rigidbody rb;
        private Collider collision;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            collision = GetComponent<Collider>();
        }

        public void SetActivePhysics(bool enable)
        {
            collision.enabled = enable;
            rb.SetEnabled(enable);
        }

        public void Interact(ref TraderTool tool)
        {
        }
    }
}
