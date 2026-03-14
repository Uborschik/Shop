using UnityEngine;

namespace Game.Entities.Items.Tools
{
    public abstract class Tool : Item
    {
        [field: SerializeField] public LayerMask InteractionMask { get; private set; }
    }
}
