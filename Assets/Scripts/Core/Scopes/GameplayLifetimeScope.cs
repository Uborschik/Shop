using Game.Core.Controllable;
using Game.Core.Possession;
using Game.Entities.Pawns;
using Game.Entities.Pawns.Player;
using Game.Entities.Player;
using Game.Entities.Vehicles;
using Game.Services.Factories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Core.Scopes
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private Boat boat;
        [SerializeField] private Trader playerPrefab;
        [SerializeField] private PlayerEntity playerEntity;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IPlayerFactory, TraderFactory>(Lifetime.Singleton);

            builder.RegisterComponentInNewPrefab(playerEntity, Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(playerPrefab, Lifetime.Singleton).AsSelf().As<IControllable>().As<IBody>();

            builder.Register<PhysicsExitValidator>(Lifetime.Singleton).As<IExitValidator>();

            builder.Register<PossessionRouter>(Lifetime.Singleton);

            builder.Register<VehicleEntrySystem>(Lifetime.Singleton);

            builder.RegisterEntryPoint<PossessionTickable>();
        }

        public class PossessionTickable : IStartable, ITickable, IFixedTickable, ILateTickable
        {
            private readonly Trader trader;
            private readonly PossessionRouter possession;

            public PossessionTickable(Trader trader, PossessionRouter possession)
            {
                this.trader = trader;
                this.possession = possession;
            }

            public void Start() => possession.OnStart(trader);
            public void Tick() => possession.OnTick();
            public void FixedTick() => possession.OnFixedTick();
            public void LateTick() => possession.OnLateTick();
        }

        public class PhysicsExitValidator : IExitValidator
        {
            public bool ValidateExitPoint(Vector3 desiredPoint, out Vector3 validPoint)
            {
                validPoint = desiredPoint;
                return true;
            }
        }
    }
}