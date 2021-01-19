using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class SectionsDebugMenu : UDM.ADebugMenu
    {
        private bool m_visible = false;
        private readonly InnerSectionDebugMenu m_innerSection = new InnerSectionDebugMenu();

        public override void Construct(IContainer container)
        {
            container.Label("You can create inner sections:");
            container.Section(m_innerSection);
            container.LabelValue("Is active:", () => m_innerSection.isActive);

            container.Separator();

            container.Label("They can nest several times:");
            container.Section("Extra section", ExtraSection);

            container.Separator();

            container.Label("Sections can be hidden:");
            container.CheckBox("Show secret section", () => m_visible)
                     .OnValueChanged((value) => m_visible = value);
            container.ShowIf(() => m_visible, (c1) => {
                c1.Section("Secret section", (c2) => {
                    c2.Label("Wow, so secret!");
                });
            });
        }

        private void ExtraSection(IContainer container)
        {
            container.Label("You can nest even further:");
            container.Section("Even further", (innerContainer) => { innerContainer.Label("Hi!"); });
        }

        public override string Name()
        {
            return "Sections";
        }

        private class InnerSectionDebugMenu : UDM.ADebugMenu
        {
            public bool isActive = false;

            public override void Construct(IContainer container)
            {
                container.Label("I'm inside inner section!");
            }

            public override void OnEnabledChanged(bool enabled)
            {
                isActive = enabled;
            }

            public override string Name()
            {
                return "Inner Section";
            }
        }
    }
}
