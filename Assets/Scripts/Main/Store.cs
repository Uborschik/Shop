using Game.Entities.Objects;
using Game.Entities.Pawns.NPCs;
using UnityEngine;

namespace Game.Main
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private OpenStore openStore;
        [SerializeField] private Buyer buyer;

        //public event Action<bool> SetState;

        //private void OnEnable()
        //{
        //    openStore.SetState += OpenStore_SetState;
        //}

        //private void OnDisable()
        //{
        //    openStore.SetState -= OpenStore_SetState;
        //}

        //private void OpenStore_SetState(bool state)
        //{
        //    SetState?.Invoke(state);
        //}
    }
}