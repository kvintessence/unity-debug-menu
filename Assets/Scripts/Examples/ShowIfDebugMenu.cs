using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class ShowIfDebugMenu : UDM.IDebugMenu
    {
        private enum Color
        {
            Red,
            Green,
            Blue,
        }

        private Color m_color = Color.Green;

        public void Construct(IContainer container)
        {
            container.Label("For each color selected there's a unique input, so you can customize it depending on previous input:");

            container.Dropdown(() => m_color)
                     .OnValueChanged(value => m_color = value);

            container.ShowIf(() => m_color == Color.Red, ConstructRed);
            container.ShowIf(() => m_color == Color.Green, ConstructGreen);
            container.ShowIf(() => m_color == Color.Blue, ConstructBlue);
        }

        private void ConstructRed(IContainer container)
        {
            container.Label("I'm red!");
        }

        private void ConstructGreen(IContainer container)
        {
            container.Button("No, make it red!")
                     .OnClick(() => m_color = Color.Red);
        }

        private void ConstructBlue(IContainer container)
        {
            container.Label("Alpha value:");
            container.FloatSlider(0.1f)
                     .MinMax(0.0f, 1.0f);
        }

        public string Name()
        {
            return "Show If";
        }
    }
}
