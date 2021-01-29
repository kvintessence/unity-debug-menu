using System;
using System.Collections.Generic;
using System.Linq;
using UDM.UI;
using Random = UnityEngine.Random;

namespace Examples
{
    [UnityEngine.Scripting.Preserve]
    public class ForEachDebugMenu : UDM.ADebugMenu
    {
        private IList<int> m_items = new List<int>();
        private IShowForEach<int> m_element;

        public override string Name()
        {
            return "Show for Each";
        }

        public override void Construct(IContainer container)
        {
            container.Label(
                "You can provide a list of objects and a debug menu constructor for any of them and UDM will handle them itself:");
            container.Separator();
            container.Section(new SimpleForEachDebugMenu());
            container.Section(new ComplexForEachDebugMenu());
        }

        private class SimpleForEachDebugMenu : UDM.ADebugMenu
        {
            private IList<int> m_items = new List<int>();
            private IShowForEach<int> m_element;

            public override string Name()
            {
                return "Simple";
            }

            public override void Construct(IContainer container)
            {
                container.Label("Items:");
                container.Separator();
                m_element = container.ShowForEach(m_items, ConstructItem);
                container.Separator();
                container.CheckBox("Show All Items", false).OnValueChanged(all => m_element.ShowEverything(all));
                container.Horizontal(CollectionButtons);
            }

            private void ConstructItem(IContainer container, int item)
            {
                container.LabelValue("Value", () => item);
            }

            private void CollectionButtons(IContainer container)
            {
                container.Button("Add").OnClick(() => m_items.Add(m_items.Count + 1));
                container.Button("Remove").OnClick(() => {
                    if (m_items.Count > 0)
                        m_items.RemoveAt(m_items.Count - 1);
                });
            }
        }

        private class ComplexForEachDebugMenu : UDM.ADebugMenu
        {
            private class Person
            {
                public string name;
                public int age = 20;
                public Color favouriteColor = Color.Red;
            }

            private enum Color
            {
                Red,
                Green,
                Blue,
            }

            private IList<Person> m_people = new List<Person>();
            private IList<Color> m_colors = new List<Color> {Color.Red, Color.Green, Color.Blue};

            private IList<string> m_names = new List<string>
                {"John", "Robert", "Cassandra", "Phillip", "Anna", "Bob", "Alice"};

            private IShowForEach<Person> m_element;

            public override string Name()
            {
                return "Complex";
            }

            public override void Construct(IContainer container)
            {
                container.Label("People:");
                container.Separator();
                m_element = container.ShowForEach(m_people, ConstructItem)
                                     .CustomNaming(p => $"{p.name}/{p.age}");
                container.Separator();
                container.CheckBox("Show All People", false).OnValueChanged(all => m_element.ShowEverything(all));
                container.Horizontal(CollectionButtons);
            }

            private void ConstructItem(IContainer container, Person item)
            {
                container.LabelValue("Name", () => item.name);
                container.Label("Age:");
                container.IntSlider(() => item.age).MinMax(15, 65).OnValueChanged(v => item.age = v);
                container.Dropdown(() => item.favouriteColor).OnValueChanged(v => item.favouriteColor = v);
            }

            private void CollectionButtons(IContainer container)
            {
                container.Button("Add").OnClick(() => {
                    m_people = new List<Person>(m_people).Append(new Person {
                        name = m_names[Random.Range(0, m_names.Count)],
                        favouriteColor = m_colors[Random.Range(0, m_colors.Count)],
                        age = Random.Range(15, 65),
                    }).ToList();
                    m_element.ProvideNewOptions(m_people);
                });
                container.Button("Remove").OnClick(() => {
                    m_people = m_people.Take(Math.Max(1, m_people.Count - 1)).ToList();
                    m_element.ProvideNewOptions(m_people);
                });
            }
        }
    }
}
