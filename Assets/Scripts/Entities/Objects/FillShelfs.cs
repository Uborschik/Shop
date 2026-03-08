using DG.Tweening;
using Game.Entities;
using Game.Entities.Items;
using Game.Entities.Objects;
using Game.Entities.Pawns;
using Game.Services.InputSystem;
using UnityEngine;

public class FillShelfs : MonoBehaviour, IInteractable
{
    [SerializeField] private Item[] prefabs;
    [SerializeField] private Shelf[] shelves;
    [SerializeField] private Transform button;
    [SerializeField] private float duration;
    [SerializeField] private float up = 0.1f;
    [SerializeField] private float dawn = 0.08f;

    private bool isPressed;

    public InteractionResult Interact(Pawn pawn, InteractionMode mode)
    {
        button.DOKill();
        button.DOLocalMoveY(!isPressed ? dawn : up, duration);
        isPressed = !isPressed;

        for (int i = 0; i < shelves.Length; i++)
        {
            var storage = shelves[i].Storage;

            while (storage.TryGetPushIndex(out _))
            {
                var item = CreateItem();

                storage.TryAddItem(item);
            }
        }

        return InteractionResult.Success;
    }

    private Item CreateItem()
    {
        if (prefabs == null || prefabs.Length == 0)
        {
            Debug.LogWarning("ItemCreator: Prefabs array is empty!");
            return null;
        }

        var randomIndex = Random.Range(0, prefabs.Length);
        var item = Instantiate(prefabs[randomIndex], transform);
        item.SetActivePhysics(false);

        return item;
    }
}
