using Game.Services.Factories;
using VContainer;
using VContainer.Unity;

namespace Game.Core
{
    public class GameplayEntryPoint : IStartable
    {
        [Inject] private readonly TraderFactory traderFactory;

        public void Start()
        {
            traderFactory.Create();
        }
    }
}
