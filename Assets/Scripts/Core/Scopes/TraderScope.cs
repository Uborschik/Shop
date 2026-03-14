using Game.Entities.Pawns;
using Game.Entities.Pawns.Player;
using Game.Services.InputSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Core.Scopes
{
    public class TraderScope : LifetimeScope
    {
        [SerializeField] private MovementConfig movementConfig;
        [SerializeField] private TraderCameraConfig cameraConfig;
        [SerializeField] private CharacterController traderCharacterController;
        [SerializeField] private Hand hand;
        [SerializeField] private Pawn trader;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(movementConfig);
            builder.RegisterInstance(cameraConfig);
            builder.RegisterInstance(traderCharacterController);
            builder.RegisterInstance(hand);
            builder.RegisterInstance(trader);

            builder.Register<PlayerInputs>(Lifetime.Scoped);
            builder.Register<TraderMovement>(Lifetime.Scoped);
            builder.Register<TraderLook>(Lifetime.Scoped);
            builder.Register<TraderInteraction>(Lifetime.Scoped);
        }
    }
}