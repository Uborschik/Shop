using Game.Entities.Pawns.Player;
using System;
using VContainer;
using VContainer.Unity;

namespace Game.Services.Factories
{
    public interface IPlayerFactory
    {
        Trader Create();
    }

    public class TraderFactory : IPlayerFactory
    {
        public event Action TraderCreated;

        [Inject] private readonly IObjectResolver resolver;
        [Inject] private readonly Trader prefab;

        public Trader Create()
        {
            var pawn = resolver.Instantiate(prefab);

            TraderCreated?.Invoke();
            return pawn;
        }
    }
}
