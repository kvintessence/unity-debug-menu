using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class CheckBoxDebugMenu : UDM.IDebugMenu
    {
        private bool m_flag1 = true;
        private bool m_flag2 = false;

        public void Construct(IContainer container)
        {
            container.Label("Some simple examples of check boxes:");

            container.CheckBox("true by default", m_flag1)
                     .OnValueChanged((value) => m_flag1 = value);
            container.LabelValue("Value", () => m_flag1);

            container.CheckBox("false by default", m_flag2)
                     .OnValueChanged((value) => m_flag2 = value);
            container.LabelValue("Value", () => m_flag2);
        }

        public string Name()
        {
            return "Check Boxes";
        }
    }
}
