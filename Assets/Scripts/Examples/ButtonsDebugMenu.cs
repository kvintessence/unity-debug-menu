using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class ButtonsDebugMenu : UDM.ADebugMenu
    {
        private int m_counter = 0;

        public override void Construct(IContainer container)
        {
            container.LabelValue("Counter", () => m_counter);
            container.Button("+1").OnClick(() => m_counter++);
            container.Separator();
            container.Label("You can also hide the whole menu when clicking the button:");
            container.Button("+1").OnClick(() => m_counter++).HideMenuOnClick();
            container.Separator();
            container.Label("You can have several buttons in a row:");
            container.Horizontal(ExampleHorizontalButtons);
        }

        private void ExampleHorizontalButtons(IContainer container)
        {
            container.Button("-10").OnClick(() => m_counter -= 10);
            container.Button("-1").OnClick(() => m_counter -= 1);
            container.Button("+1").OnClick(() => m_counter += 1);
            container.Button("+10").OnClick(() => m_counter += 10);
        }

        public override string Name()
        {
            return "Buttons";
        }
    }
}
