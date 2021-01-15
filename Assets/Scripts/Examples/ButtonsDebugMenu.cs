using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class ButtonsDebugMenu : UDM.IDebugMenu
    {
        private int m_counter = 0;

        public void Construct(IContainer container)
        {
            container.LabelValue("Counter", () => m_counter);
            container.Button("+1").OnClick(() => m_counter++);
            container.Separator();
            container.Label("You can also hide the whole menu when clicking the button:");
            container.Button("+1").OnClick(() => m_counter++).HideMenuOnClick();
        }

        public string Name()
        {
            return "Buttons";
        }
    }
}
