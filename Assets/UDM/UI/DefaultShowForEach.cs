using System;
using System.Collections.Generic;
using System.Linq;

namespace UDM
{
    namespace UI
    {
        public class DefaultShowForEach<T> : IShowForEach<T>
        {
            private Action<T> m_onChanged = null;
            private Func<T, string> m_namingFunction = null;
            private CustomCallbacksComponent m_visual = default;
            private Action<IContainer, T> m_sectionConstructor = default;
            private bool m_showEverything = false;

            private DefaultContainer m_parentContainer = default;
            private DefaultContainer m_itemsContainer = null;

            private IDropdown<T> m_dropdown = null;
            private Dictionary<T, EmptyContainerIf> m_itemContainers = new Dictionary<T, EmptyContainerIf>();

            private T m_selectedItem = default;
            private IList<T> m_items = default;
            private IList<T> m_cachedItems = null;

            /************************************************************************************************/

            public DefaultShowForEach<T> SetVisualElement(CustomCallbacksComponent visualElement)
            {
                m_visual = visualElement;
                m_visual.OnUpdate += Update;
                return this;
            }

            public DefaultShowForEach<T> SetContainer(DefaultContainer parentContainer)
            {
                m_parentContainer = parentContainer;
                return this;
            }

            public DefaultShowForEach<T> Init(IList<T> items, Action<IContainer, T> sectionConstructor)
            {
                m_items = items;
                m_sectionConstructor = sectionConstructor;

                m_parentContainer.ShowIf(() => !m_showEverything, c => {
                    m_dropdown = c.Dropdown(() => m_selectedItem, m_items).OnValueChanged(newValue => {
                        m_selectedItem = newValue;
                        m_onChanged?.Invoke(newValue);
                    });
                });
                m_itemsContainer = m_parentContainer.WithContent(m_parentContainer.EmptyContainerVertical().content);

                RecreateItems();

                return this;
            }

            public IShowForEach<T> CustomNaming(Func<T, string> namingFunction)
            {
                m_dropdown.CustomNaming(namingFunction);
                m_namingFunction = namingFunction;
                return this;
            }

            public IShowForEach<T> OnValueChanged(Action<T> action)
            {
                m_onChanged = action;
                return this;
            }

            public IShowForEach<T> ProvideNewOptions(IList<T> items)
            {
                m_items = items;
                m_dropdown.ProvideNewOptions(items);

                if (m_cachedItems != null)
                    RecreateItems();

                return this;
            }

            public IShowForEach<T> ShowEverything(bool everything = true)
            {
                m_showEverything = everything;
                return this;
            }

            /************************************************************************************************/

            private void Update()
            {
                if (HasItemsChanged())
                    RecreateItems();
            }

            private bool HasItemsChanged()
            {
                if (m_cachedItems == null)
                    return false;

                if (m_cachedItems.Count != m_items.Count)
                    return true;

                for (var i = 0; i < m_cachedItems.Count; ++i)
                    if (!m_items[i].Equals(m_cachedItems[i]))
                        return true;

                return false;
            }

            private void RecreateItems()
            {
                m_cachedItems = new List<T>(m_items);

                var toDelete = m_itemContainers.Keys.Where(k => !m_items.Contains(k)).ToList();
                var toAdd = m_items.Where(i => !m_itemContainers.ContainsKey(i)).ToList();

                foreach (var item in toDelete) {
                    var visual = m_itemContainers[item];
                    m_itemContainers.Remove(item);
                    UnityEngine.Object.Destroy(visual.gameObject);
                }

                foreach (var item in toAdd) {
                    var newElement =
                        m_itemsContainer.ShowIfEx(() => m_showEverything || Equals(m_selectedItem, item), c1 => {
                            c1.WithBackground(c2 => { m_sectionConstructor(c2, item); });
                        });
                    m_itemContainers[item] = newElement;
                }

                // reorder children
                for (var i = 0; i < m_items.Count; ++i) {
                    var item = m_items[i];
                    m_itemContainers[item]?.transform.SetSiblingIndex(i);
                }
            }
        }
    }
}
