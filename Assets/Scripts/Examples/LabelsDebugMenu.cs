using UDM.UI;
using UnityEngine;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class LabelsDebugMenu : UDM.ADebugMenu
    {
        private int m_counter = 0;

        private class LeaderboardEntry
        {
            public int score;
            public string name;
        }

        public override void Construct(IContainer container)
        {
            container.Label("Simple label example.");

            container.Separator();

            container.Label("An example of a very long label that can have several lines of text. " +
                            "You can write here anything you want, and it will be resized automatically.");

            container.Separator();

            container.Label("You can monitor a dynamic value:");
            container.LabelValue("Simple FPS", () => (int) (1f / Time.unscaledDeltaTime));

            container.Separator();

            container.Label("You can also use some of Unity event functions:");
            container.LabelValue("Updates counter", () => m_counter);

            container.Separator();

            var scoreEntry = new LeaderboardEntry {name = "Henry", score = 12};
            container.Label("You can have a custom naming function for a label value:");
            container.LabelValue("Score", scoreEntry)
                     .CustomNaming(v => $"{v.name}/{v.score}");
        }

        public override void Update()
        {
            m_counter++;
        }

        public override string Name()
        {
            return "Labels";
        }
    }
}
