﻿using System.Collections.Generic;
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

        public override void Construct(IContainer container)
        {
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

        public override string Name()
        {
            return "Dropdown";
        }
    }
}
