using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class SectionsDebugMenu : UDM.IDebugMenu
    {
        public void Construct(IContainer container)
        {
            container.Label("You can create inner sections:");
            container.Section("Section 1", (innerContainer) => {
                innerContainer.Label("I'm inside inner section!");
            });
            container.Section("Section 2", ExtraSection);
        }

        private void ExtraSection(IContainer container)
        {
            container.Label("You can nest even further:");
            container.Section("Even further", (innerContainer) => {
                innerContainer.Label("Hi!");
            });
        }

        public string Name()
        {
            return "Sections";
        }
    }
}
