using Game.Core.Controllable;
using Game.Core.Possession;
using Game.Entities.Pawns.Player;
using Game.Entities.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Core.Scopes
{
    public partial class GameplayLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerEntity>();
            builder.RegisterComponentInHierarchy<Trader>().AsSelf().As<IBody>();

            builder.RegisterEntryPoint<PossessionRouter>(Lifetime.Singleton);
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