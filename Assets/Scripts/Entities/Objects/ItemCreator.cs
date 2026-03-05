using Game.Entities.Items;
using Game.Entities.Pawns.Player;
using Game.Entities.Tools;
using Game.Services.Inventory;
using System.Collections;
using UnityEngine;

namespace Game.Entities.Objects
{
    public class ItemCreator : MonoBehaviour, IInteractable
    {
        [SerializeField] private Item[] prefabs;
        [SerializeField] private PlacementGrid placement;
        [SerializeField] private float interval;

        private Item createdItem;
        private Coroutine spawnCoroutine;

        private void Start()
        {
            CreateItem();
        }

        public void Interact(ref TraderTool tool)
        {
            if (createdItem == null) return;

            if (tool is IContainer container)
            {
                if (container.TryPushItem(createdItem))
                {
                    createdItem = null;

                    spawnCoroutine ??= StartCoroutine(SpawnAfterDelay());
                }
            }
        }

        private void CreateItem()
        {
            if (prefabs == null || prefabs.Length == 0)
            {
                Debug.LogWarning("ItemCreator: Prefabs array is empty!");
                return;
            }

            var randomIndex = Random.Range(0, prefabs.Length);
            createdItem = Instantiate(prefabs[randomIndex], placement.transform);
            createdItem.transform.position += placement.Slots[0];
            createdItem.SetActivePhysics(false);
        }

        private IEnumerator SpawnAfterDelay()
        {
            yield return new WaitForSeconds(interval);

            spawnCoroutine = null;
            CreateItem();
        }
    }
}