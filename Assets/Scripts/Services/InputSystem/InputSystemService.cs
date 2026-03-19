namespace Game.Services.InputSystem
{
    public class InputSystemService
    {
        public InputControls Controls { get; }

        public InputSystemService()
        {
            Controls = new InputControls();
        }
    }
}