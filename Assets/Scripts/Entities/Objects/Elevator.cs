using Game.Core.Controllable;
using Game.Entities.Interactables;
using Game.Entities.Pawns.Player;
using PrimeTween;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class Elevator : InteractableObject
    {
        [SerializeField] private TweenSettings tweenSettings;
        [SerializeField] private float[] floorHeights;
        [SerializeField] private Transform lever;

        private int currentHeight;

        private void Start()
        {
            if (transform.position.y != floorHeights[0])
            {
                var target = new Vector3(transform.position.x, floorHeights[0], transform.position.z);
                Rise(target);
            }

            currentHeight = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<IBody>(out var body))
            {
                body.Transform.SetParent(transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.TryGetComponent<IBody>(out var body))
            {
                body.Transform.SetParent(null);
            }
        }

        public override InteractionResult Interact(InteractionContext context, InteractionMode mode)
        {
            var target = new Vector3(transform.position.x, floorHeights[++currentHeight], transform.position.z);
            Rise(target);

            return InteractionResult.Success;
        }

        private void Rise(Vector3 target)
        {
            Tween.Position(transform, new TweenSettings<Vector3>(target, tweenSettings));
        }
    }
}