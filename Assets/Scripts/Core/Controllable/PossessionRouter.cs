using Game.Core.Controllable;
using Game.Entities.Player;
using VContainer;
using VContainer.Unity;

namespace Game.Core.Possession
{
    public class PossessionRouter : IStartable
    {
        [Inject] private readonly IBody currentController;
        [Inject] private readonly PlayerEntity playerEntity;

        public void Start()
        {
            playerEntity.PossessBody(currentController);
            currentController.Possess(ControlFlag.Movement | ControlFlag.Look);
        }
    }
}