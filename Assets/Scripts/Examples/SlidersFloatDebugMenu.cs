using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class SlidersFloatDebugMenu : UDM.IDebugMenu
    {
        private float m_floatValue1 = 0.0f;
        private float m_floatValue2 = 0.0f;

        public void Construct(IContainer container)
        {
            container.Label("Some simple example of value slider (without value text) between 0 and 1:");
            container.FloatSlider(m_floatValue1)
                     .MinMax(0.0f, 1.0f)
                     .ShowValue(false)
                     .OnValueChanged(value => m_floatValue1 = value);
            container.LabelValue("Value", () => m_floatValue1);

            container.Separator();

            container.Label("Next two sliders share the same value, but have different min/max constraints:");
            container.FloatSlider(() => m_floatValue2)
                     .ShowValue()
                     .MinMax(-100.0f, 100.0f)
                     .OnValueChanged(value => m_floatValue2 = value);
            container.FloatSlider(() => m_floatValue2)
                     .MinMax(-200.0f, 200.0f)
                     .OnValueChanged(value => m_floatValue2 = value);
            container.LabelValue("Value", () => m_floatValue2);
        }

        public string Name()
        {
            return "Sliders - Float";
        }
    }
}
