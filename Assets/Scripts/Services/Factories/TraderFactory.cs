using Game.Core.Scopes;
using Game.Entities.Pawns;
using System;
using VContainer;
using VContainer.Unity;

namespace Game.Services.Factories
{
    public class TraderFactory
    {
        public event Action TraderCreated;

        [Inject] private readonly LifetimeScope lifetimeScope;
        [Inject] private readonly TraderScope traderScope;

        public Pawn Create()
        {
            var scope = lifetimeScope.CreateChildFromPrefab(traderScope);
            var pawn = scope.GetComponent<Pawn>();

            TraderCreated?.Invoke();
            return pawn;
        }
    }
}
