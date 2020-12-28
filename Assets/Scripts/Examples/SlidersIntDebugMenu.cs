using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class SlidersIntDebugMenu : UDM.IDebugMenu
    {
        private int m_intValue1 = 0;
        private int m_intValue2 = 0;

        public void Construct(IContainer container)
        {
            container.Label("Some simple example of value slider (without value text) that can be either 0 or 1:");
            container.IntSlider(m_intValue1)
                     .MinMax(0, 1)
                     .ShowValue(false)
                     .OnValueChanged(value => m_intValue1 = value);
            container.LabelValue("Value", () => m_intValue1);

            container.Separator();

            container.Label("Next two sliders share the same value, but have different min/max constraints:");
            container.IntSlider(() => m_intValue2)
                     .ShowValue()
                     .MinMax(-100, 100)
                     .OnValueChanged(value => m_intValue2 = value);
            container.IntSlider(() => m_intValue2)
                     .MinMax(-200, 200)
                     .OnValueChanged(value => m_intValue2 = value);
            container.LabelValue("Value", () => m_intValue2);
        }

        public string Name()
        {
            return "Sliders - Int";
        }
    }
}
