using Game.Services.Factories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Core.Scopes
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private TraderScope traderScope;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(traderScope);

            builder.Register<TraderFactory>(Lifetime.Scoped);
            builder.RegisterEntryPoint<GameplayEntryPoint>(Lifetime.Singleton);
        }
    }
}