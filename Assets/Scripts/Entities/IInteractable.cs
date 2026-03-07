using Game.Services.InputSystem;

namespace Game.Entities
{
    public interface IInteractable
    {
        void Interact(IInteractor interactor, InteractionMode mode);
    }
}
