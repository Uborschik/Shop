using Game.Entities.Interactables;
using Game.Entities.Pawns.Player;
using Game.Services.Inventory;
using UnityEngine;

namespace Game.Entities.Items
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class Item : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public ItemData Data { get; private set; }

        private Rigidbody rb;
        private Collider collision;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            collision = GetComponent<Collider>();
        }

        public InteractionResult Interact(InteractionContext context, InteractionMode mode)
        {
            switch (mode)
            {
                case InteractionMode.Primary:
                    context.Hand.Pickup(this);
                    return InteractionResult.Success;
                case InteractionMode.Secondary:

                    if (context.Hand.Item is IGridStorageHolder gridStorage)
                    {
                        gridStorage.Storage.TryAddItem(this);
                    }

                    return InteractionResult.Success;
                default:
                    return InteractionResult.Failure;
            }
        }

        public void OnPickup(Transform parent)
        {
            PushTo(parent, parent.position, parent.rotation);
            SetActivePhysics(false);
        }

        public void OnPickup(Transform parent, Vector3 position)
        {
            PushTo(parent, position, parent.rotation);
            SetActivePhysics(false);
        }

        public void OnDrop()
        {
            PushTo(null, transform.position, transform.rotation);
            SetActivePhysics(true);
        }

        private void PushTo(Transform parent, Vector3 position, Quaternion rotation)
        {
            transform.SetParent(parent);
            transform.SetPositionAndRotation(position, rotation);
        }

        private void SetActivePhysics(bool enable)
        {
            collision.enabled = enable;
            rb.SetEnabled(enable);
        }
    }
}
