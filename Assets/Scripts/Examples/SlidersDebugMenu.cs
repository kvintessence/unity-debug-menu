using UDM.UI;

[UnityEngine.Scripting.Preserve]
public class FloatSlidersDebugMenu : UDM.IDebugMenu
{
    private float m_floatValue1 = 0.0f;
    private float m_floatValue2 = 0.0f;

    private int m_intValue1 = 0;
    private int m_intValue2 = 0;

    public void Construct(IContainer container)
    {
        container.Section("Float Sliders", FloatSlidersSection);
        container.Section("Int Sliders", IntSlidersSection);
    }

    private void IntSlidersSection(IContainer container)
    {
        container.Label("Some simple example of value slider (without value text) that can be either 0 or 1:");
        container.IntSlider(m_intValue1)
                 .MinMax(0, 1)
                 .ShowValue(false)
                 .OnValueChanged(value => m_intValue1 = value);
        container.Label(() => $"Value: {m_intValue1}");

        container.Separator();

        container.Label("Next two sliders share the same value, but have different min/max constraints:");
        container.IntSlider(() => m_intValue2)
                 .ShowValue()
                 .MinMax(-100, 100)
                 .OnValueChanged(value => m_intValue2 = value);
        container.IntSlider(() => m_intValue2)
                 .MinMax(-200, 200)
                 .OnValueChanged(value => m_intValue2 = value);
        container.Label(() => $"Value: {m_intValue2}");
    }

    private void FloatSlidersSection(IContainer container)
    {
        container.Label("Some simple example of value slider (without value text) between 0 and 1:");
        container.FloatSlider(m_floatValue1)
                 .MinMax(0.0f, 1.0f)
                 .ShowValue(false)
                 .OnValueChanged(value => m_floatValue1 = value);
        container.Label(() => $"Value: {m_floatValue1}");

        container.Separator();

        container.Label("Next two sliders share the same value, but have different min/max constraints:");
        container.FloatSlider(() => m_floatValue2)
                 .ShowValue()
                 .MinMax(-100.0f, 100.0f)
                 .OnValueChanged(value => m_floatValue2 = value);
        container.FloatSlider(() => m_floatValue2)
                 .MinMax(-200.0f, 200.0f)
                 .OnValueChanged(value => m_floatValue2 = value);
        container.Label(() => $"Value: {m_floatValue2}");
    }

    public string Name()
    {
        return "Sliders";
    }
}
