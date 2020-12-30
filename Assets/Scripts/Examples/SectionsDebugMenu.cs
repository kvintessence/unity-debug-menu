using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class SectionsDebugMenu : UDM.IDebugMenu
    {
        private bool m_visible = false;

        public void Construct(IContainer container)
        {
            container.Label("You can create inner sections:");
            container.Section("Section 1", (innerContainer) => {
                innerContainer.Label("I'm inside inner section!");
            });
            container.Section("Section 2", ExtraSection);

            container.Separator();

            container.Label("Sections can be hidden:");
            container.CheckBox("Show secret section", () => m_visible)
                     .OnValueChanged((value) => m_visible = value);
            container.ShowIf(() => m_visible, (c1) => {
                c1.Section("Secret section", (c2) => {
                    c2.Label("You are inside the secret section!");
                });
            });
        }

        private void ExtraSection(IContainer container)
        {
            container.Label("You can nest even further:");
            container.Section("Even further", (innerContainer) => { innerContainer.Label("Hi!"); });
        }

        public string Name()
        {
            return "Sections";
        }
    }
}
