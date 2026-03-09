using Game.Entities.Pawns;
using UnityEngine;

namespace Game.Entities.Items
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Item : Entity, IInteractable
    {
        [field: SerializeField] public ItemData Data { get; private set; }

        private Rigidbody rb;
        private Collider collision;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            collision = GetComponent<Collider>();
        }

        public InteractionResult Interact(Pawn pawn, InteractionMode mode)
        {
            throw new System.NotImplementedException();
        }

        public void PushTo(Transform parent, Vector3 position, Quaternion rotation)
        {
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(position, rotation);
        }

        public void SetActivePhysics(bool enable)
        {
            collision.enabled = enable;
            rb.SetEnabled(enable);
        }
    }
}
