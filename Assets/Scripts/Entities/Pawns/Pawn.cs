using Game.Entities.Pawns.Player;
using UnityEngine;
using VContainer;

namespace Game.Entities.Pawns
{
    public abstract class Pawn : MonoBehaviour
    {
        [Inject] public Hand Hand { get; protected set; }
    }
}
