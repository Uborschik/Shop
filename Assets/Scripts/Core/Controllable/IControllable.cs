namespace Game.Core.Controllable
{
    public interface IControllable
    {
        ControlFlag CurrentFlags { get; }

        void Possess(ControlFlag grantedFlags);
        void Release(ControlFlag flagsToRelease);
    }
}
