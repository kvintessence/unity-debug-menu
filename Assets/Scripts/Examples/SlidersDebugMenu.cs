using UDM.UI;

[UnityEngine.Scripting.Preserve]
public class FloatSlidersDebugMenu : UDM.IDebugMenu
{
    private float m_value1 = 0.0f;
    private float m_value2 = 0.0f;

    public void Construct(IContainer container)
    {
        container.Section("Float Sliders", FloatSlidersSection);
        container.Section("Int Sliders", IntSlidersSection);
    }

    private void IntSlidersSection(IContainer container)
    {
        container.Label("nothing here yet!");
    }

    private void FloatSlidersSection(IContainer container)
    {
        container.Label("Some simple example of float value slider (without value text):");
        container.FloatSlider(m_value1)
                 .OnValueChanged(value => m_value1 = value);
        container.Label(() => $"Value: {m_value1}");

        container.Separator();

        container.Label("Next two sliders share the same value, but have different min/max constraints:");
        container.FloatSlider(() => m_value2)
                 .ShowValue()
                 .MinMax(-100.0f, 100.0f)
                 .OnValueChanged(value => m_value2 = value);
        container.FloatSlider(() => m_value2)
                 .MinMax(-200.0f, 200.0f)
                 .OnValueChanged(value => m_value2 = value);
        container.Label(() => $"Value: {m_value2}");
    }

    public string Name()
    {
        return "Sliders";
    }
}
