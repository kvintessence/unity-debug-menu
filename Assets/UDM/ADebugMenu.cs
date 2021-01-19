using UDM.UI;

namespace UDM
{
    public abstract class ADebugMenu
    {
        public abstract void Construct(IContainer container);
        public abstract string Name();

        public virtual void Update()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }

        public virtual void OnEnabledChanged(bool enabled)
        {
        }
    }
}
