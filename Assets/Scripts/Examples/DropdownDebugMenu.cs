using System;
using System.Collections.Generic;
using System.Linq;
using UDM.UI;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class DropdownDebugMenu : UDM.ADebugMenu
    {
        private enum Color
        {
            Red,
            Green,
            Blue,
        }

        private class LeaderboardValue
        {
            public int score;
            public string name;
        }

        private Color m_color = Color.Green;
        private string m_stringValue = "";
        private LeaderboardValue m_score = null;

        private IList<int> m_dynamicOptions1 = new List<int> {1, 2, 3};
        private int m_dynamicOption1 = 1;

        private IDropdown<string> m_dynamicDropdown2 = null;
        private IList<string> m_dynamicOptions2 = new List<string> {"1", "2", "3"};
        private string m_dynamicOption2 = "1";

        public override void Construct(IContainer container)
        {
            container.Section("Dynamic dropdown", EditingOptions);

            container.Separator();

            container.Label("Here's an example of selecting Enum values:");
            container.Dropdown(() => m_color)
                     .OnValueChanged(value => m_color = value);

            container.Label("Applying custom naming rule:");
            container.Dropdown(() => m_color)
                     .CustomNaming(value => $"Color::{value}")
                     .OnValueChanged(value => m_color = value);
            container.LabelValue("Color", () => m_color);

            container.Separator();

            container.Label("It should be fine to create dropdown with zero options (no sense in it though):");
            container.Dropdown(5, new List<int>());

            container.Separator();

            var options = new List<string> {"option a", "option b", "option c"};
            container.Label("Picking a string out of the list of strings " +
                            "(notice that the empty string will be automatically initialised to the first option):");
            container.Dropdown(m_stringValue, options)
                     .OnValueChanged(value => m_stringValue = value);
            container.LabelValue("String value", () => m_stringValue);

            container.Separator();

            var customOptions = new List<LeaderboardValue> {
                new LeaderboardValue {name = "Henry", score = 12},
                new LeaderboardValue {name = "Tom", score = 24},
                new LeaderboardValue {name = "William", score = 36},
            };

            container.Label("Testing custom types:");
            container.Dropdown(m_score, customOptions)
                     .CustomNaming(value => value.name)
                     .OnValueChanged(value => m_score = value);
            container.LabelValue("Score", () => m_score)
                     .CustomNaming(v => $"{v.name}/{v.score}");
        }

        private void EditingOptions(IContainer container)
        {
            container.Label("Dropdown may hold dynamic list of options that can be edited during runtime:");
            container.Dropdown(() => m_dynamicOption1, m_dynamicOptions1)
                     .OnValueChanged(v => m_dynamicOption1 = v);
            container.LabelValue("Int value", () => m_dynamicOption1);
            container.Horizontal(c => {
                c.Button("Add").OnClick(() => m_dynamicOptions1.Add(m_dynamicOptions1.Count + 1));
                c.Button("Remove").OnClick(() => {
                    if (m_dynamicOptions1.Count > 0)
                        m_dynamicOptions1.RemoveAt(m_dynamicOptions1.Count - 1);
                });
            });

            container.Separator();

            container.Label("You can also manually provide a new list of options:");
            m_dynamicDropdown2 = container.Dropdown(() => m_dynamicOption2, m_dynamicOptions2)
                                         .OnValueChanged(v => m_dynamicOption2 = v);
            container.LabelValue("String value", () => m_dynamicOption2);
            container.Horizontal(c => {
                c.Button("Add").OnClick(() => {
                    m_dynamicOptions2 = new List<string>(m_dynamicOptions2).Append($"{m_dynamicOptions2.Count + 1}").ToList();
                    m_dynamicDropdown2.ProvideNewOptions(m_dynamicOptions2);
                });
                c.Button("Remove").OnClick(() => {
                    m_dynamicOptions2 = m_dynamicOptions2.Take(Math.Max(0, m_dynamicOptions2.Count - 1)).ToList();
                    m_dynamicDropdown2.ProvideNewOptions(m_dynamicOptions2);
                });
            });
        }

        public override string Name()
        {
            return "Dropdown";
        }
    }
}
