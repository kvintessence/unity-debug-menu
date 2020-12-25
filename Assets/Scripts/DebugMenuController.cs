using System;
using System.Collections.Generic;
using System.Linq;
using UDM.UI;
using UnityEngine;

namespace UDM
{
    public class DebugMenuController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_container;

        [SerializeField]
        private DefaultContainerRegistry m_registry;

        private GameObject m_selectionSection = null;
        private List<IDebugMenu> m_menus = new List<IDebugMenu>();

        private void Awake()
        {
            // create all debug menus existing in app
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (var type in assembly.GetTypes()) {
                    if (type.IsInterface || !type.IsPublic || !type.GetInterfaces().Contains(typeof(IDebugMenu)))
                        continue;

                    var instance = Activator.CreateInstance(type);
                    m_menus.Add(instance as IDebugMenu);
                }
            }

            // sort by name
            m_menus.Sort((d1, d2) => string.Compare(d1.Name(), d2.Name(), StringComparison.Ordinal));

            // first create all debug menus
            foreach (var menu in m_menus) {
                var section = Instantiate(m_registry.debugMenuSection, m_container);
                var container = new DefaultContainer(section.content, m_container, m_registry);
                menu.Construct(container);
            }

            // now create the main selection menu
            var selectionSection = Instantiate(m_registry.debugMenuSection, m_container);
            var selectionContainer = new DefaultContainer(selectionSection.content, m_container, m_registry);

            foreach (var menu in m_menus) {
                selectionContainer.Button(menu.Name);
            }

            m_selectionSection = selectionSection.gameObject;
        }

        public void OnDebugButtonPressed()
        {
            m_selectionSection.SetActive(!m_selectionSection.activeSelf);
        }
    }
}
