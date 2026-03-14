using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Inventory
{
    public class PlacementGrid : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private Vector3Int cellCount = new(1, 1, 1);
        [SerializeField] private Vector3 cellSize = new(0.2f, 0.2f, 0.2f);
        [SerializeField] private Vector3 gap = Vector3.zero;

        private readonly List<Vector3> slots = new();

        public IReadOnlyList<Vector3> Slots => slots;

        private void Awake()
        {
            GenerateSlots();
        }

        private void GenerateSlots()
        {
            slots.Clear();

            var countX = Mathf.Max(0, cellCount.x);
            var countY = Mathf.Max(0, cellCount.y);
            var countZ = Mathf.Max(0, cellCount.z);

            if (countX <= 0 || countY <= 0 || countZ <= 0)
                return;

            var step = cellSize + gap;

            var totalSize = new Vector3(
                countX * cellSize.x + (countX - 1) * gap.x,
                countY * cellSize.y + (countY - 1) * gap.y,
                countZ * cellSize.z + (countZ - 1) * gap.z
            );

            var startX = -totalSize.x / 2 + cellSize.x / 2;
            var startY = cellSize.y / 2;
            var startZ = -totalSize.z / 2 + cellSize.z / 2;

            var startPos = new Vector3(startX, startY, startZ);

            for (int z = 0; z < countZ; z++)
            {
                for (int y = 0; y < countY; y++)
                {
                    for (int x = 0; x < countX; x++)
                    {
                        var slotPosition = startPos + new Vector3(
                            x * step.x,
                            y * step.y,
                            z * step.z
                        );

                        slots.Add(slotPosition);
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (cellCount.x > 0 && cellCount.y > 0 && cellCount.z > 0)
            {
                GenerateSlots();
            }

            if (slots.Count == 0) return;

            Matrix4x4 gizmoMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.matrix = gizmoMatrix;

            var totalSize = new Vector3(
                cellCount.x * cellSize.x + (cellCount.x - 1) * gap.x,
                cellCount.y * cellSize.y + (cellCount.y - 1) * gap.y,
                cellCount.z * cellSize.z + (cellCount.z - 1) * gap.z
            );

            var boundsCenter = new Vector3(0, totalSize.y / 2, 0);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(boundsCenter, totalSize);

            Gizmos.color = Color.cyan;
            foreach (var slot in slots)
            {
                Gizmos.DrawWireCube(slot, cellSize);
            }

            Gizmos.color = Color.blue;
            foreach (var slot in slots)
            {
                Gizmos.DrawSphere(slot, 0.05f);
            }

            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}