using Game.Entities.Pawns.Player;
using UnityEngine;

namespace Game.Entities.Pawns
{
    public abstract class Pawn : MonoBehaviour
    {
        [field: SerializeField] public ToolHolder ToolHolder { get; protected set; }
    }
}
