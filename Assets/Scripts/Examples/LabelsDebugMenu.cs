using UDM.UI;
using UnityEngine;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class LabelsDebugMenu : UDM.IDebugMenu
    {
        private class LeaderboardEntry
        {
            public int score;
            public string name;
        }

        public void Construct(IContainer container)
        {
            container.Label("Simple label example.");

            container.Separator();

            container.Label("An example of a very long label that can have several lines of text. " +
                            "You can write here anything you want, and it will be resized automatically.");

            container.Separator();

            container.Label("You can monitor a dynamic value:");
            container.LabelValue("Simple FPS", () => (int) (1f / Time.unscaledDeltaTime));

            container.Separator();

            var scoreEntry = new LeaderboardEntry {name = "Henry", score = 12};
            container.Label("You can have a custom naming function for a label value:");
            container.LabelValue("Score", scoreEntry)
                     .CustomNaming(v => $"{v.name}/{v.score}");
        }

        public string Name()
        {
            return "Labels";
        }
    }
}
