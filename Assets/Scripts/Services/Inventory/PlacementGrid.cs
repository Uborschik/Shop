using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Inventory
{
    public class PlacementGrid : MonoBehaviour
    {
        [SerializeField] private Vector3 size = Vector3.one;
        [SerializeField] private Vector3 cellSize = new(0.2f, 0.2f, 0.2f);
        [SerializeField] private Vector3 gap = Vector2.zero;

        private Vector3 center;

        public IReadOnlyList<Vector3> Slots { get; private set; }

        private Bounds Bounds => new(center, size);

        private void Awake()
        {
            GenerateSlots();
        }

        private void GenerateSlots()
        {
            var slots = new List<Vector3>();

            center = transform.position;

            var bounds = Bounds;
            var effectiveSize = bounds.max - bounds.min;

            effectiveSize.x = ClampValue(effectiveSize.x, cellSize.x, size.x);
            effectiveSize.y = ClampValue(effectiveSize.y, cellSize.y, size.y);
            effectiveSize.z = ClampValue(effectiveSize.z, cellSize.z, size.z);

            if (effectiveSize.x <= 0 || effectiveSize.y <= 0 || effectiveSize.z <= 0) return;

            var fixCellSize = cellSize + gap;

            var countX = Mathf.CeilToInt(effectiveSize.x / fixCellSize.x);
            var countY = Mathf.CeilToInt(effectiveSize.y / fixCellSize.y);
            var countZ = Mathf.CeilToInt(effectiveSize.z / fixCellSize.z);

            if (countX <= 0 && effectiveSize.x >= cellSize.x) countX = 1;
            if (countY <= 0 && effectiveSize.y >= cellSize.y) countY = 1;
            if (countZ <= 0 && effectiveSize.z >= cellSize.z) countZ = 1;

            if (countX <= 0 || countY <= 0 || countZ <= 0) return;

            var totalWidth = countX * cellSize.x + (countX - 1) * gap.x;
            var totalHeight = countY * cellSize.y + (countY - 1) * gap.y;
            var totalDepth = countZ * cellSize.z + (countZ - 1) * gap.z;

            var startX = bounds.min.x + (effectiveSize.x - totalWidth) / 2 + cellSize.x / 2;
            var startY = bounds.min.y + (effectiveSize.y - totalHeight) / 2 + cellSize.y / 2;
            var startZ = bounds.min.z + (effectiveSize.z - totalDepth) / 2 + cellSize.z / 2;

            for (int z = 0; z < countZ; z++)
            {
                for (int y = 0; y < countY; y++)
                {
                    for (int x = 0; x < countX; x++)
                    {
                        var slot = new Vector3(
                            startX + x * fixCellSize.x,
                            startY + y * fixCellSize.y,
                            startZ + z * fixCellSize.z
                        );
                        slots.Add(slot - center);
                    }
                }
            }

            Slots = slots;
        }

        private float ClampValue(float value, float min, float max)
        {
            if (min > max) return min;
            return Mathf.Clamp(value, min, max);
        }

        private void OnDrawGizmosSelected()
        {
            var bounds = Bounds;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(bounds.center, bounds.size);

            GenerateSlots();

            Gizmos.color = Color.cyan;
            foreach (var slot in Slots)
            {
                Gizmos.DrawWireCube(slot + center, cellSize);
            }

            Gizmos.color = Color.blue;
            foreach (var slot in Slots)
            {
                Gizmos.DrawSphere(slot + center, 0.05f);
            }
        }
    }
}