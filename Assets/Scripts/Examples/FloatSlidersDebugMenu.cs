using UDM.UI;

[UnityEngine.Scripting.Preserve]
public class FloatSlidersDebugMenu : UDM.IDebugMenu
{
    private float m_value1 = 0.0f;
    private float m_value2 = 0.0f;

    public void Construct(IContainer container)
    {
        container.Label("Slider without value text");

        container.FloatSlider(m_value1)
                 .OnValueChanged(value => m_value1 = value);

        container.Label(() => $"Slider1 Value: {m_value1}");

        container.Label("Slider with value text");

        container.FloatSlider(() => m_value2)
                 .ShowValue()
                 .MinMax(-100.0f, 100.0f)
                 .OnValueChanged(value => m_value2 = value);

        container.Label(() => $"Slider2 Value: {m_value2}");

        container.Label("Slider with same value");

        container.FloatSlider(() => m_value2)
                 .MinMax(-200.0f, 200.0f)
                 .OnValueChanged(value => m_value2 = value);

        container.Label(() => $"Slider2 Value: {m_value2}");
    }

    public string Name()
    {
        return "Sliders";
    }
}
