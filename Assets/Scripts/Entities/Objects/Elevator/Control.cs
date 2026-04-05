using Game.Core.Controllable;
using PrimeTween;
using UnityEngine;

namespace Game.Entities.Objects.Elevator
{
    public class Control : MonoBehaviour
    {
        [SerializeField] private float speed = 1.2f;
        [SerializeField] private float[] floorHeights;
        [SerializeField] private Button btnUp;
        [SerializeField] private Button btnDown;
        [SerializeField] private Display display;

        private int currentFloorIndex;
        private int targetFloorIndex;

        private int currentFloorNumber;
        private int targetFloorNumber;

        private void OnEnable()
        {
            btnUp.Pressed += Btn_Pressed;
            btnDown.Pressed += Btn_Pressed;
        }

        private void Start()
        {
            currentFloorIndex = 0;
            targetFloorIndex = currentFloorIndex;
            currentFloorNumber = currentFloorIndex + 1;
            targetFloorNumber = targetFloorIndex + 1;
            display.SetTarget(targetFloorNumber);
            display.SetCurrent(currentFloorNumber);
        }

        private void OnDisable()
        {
            btnUp.Pressed += Btn_Pressed;
            btnDown.Pressed += Btn_Pressed;
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

        private void Btn_Pressed(int value)
        {
            var next = targetFloorIndex + value;

            if (next < 0 || next > floorHeights.Length - 1) return;

            targetFloorIndex = next;
            targetFloorNumber = targetFloorIndex + 1;

            display.SetTarget(targetFloorIndex + 1);


            Rise();
        }

        private void Rise()
        {
            var duration = Mathf.Abs(transform.position.y - floorHeights[targetFloorIndex]) / speed;
            var floor = new Vector3(transform.position.x, floorHeights[targetFloorIndex], transform.position.z);

            Tween.Position(transform, new TweenSettings<Vector3>(endValue: floor, duration: duration, ease: Ease.Linear, updateType: UpdateType.FixedUpdate))
                .OnComplete(() =>
                {
                    currentFloorIndex = Mathf.Clamp(targetFloorIndex, 0, floorHeights.Length);
                    currentFloorNumber = currentFloorIndex + 1;

                    display.SetCurrent(currentFloorNumber);
                });
        }
    }
}