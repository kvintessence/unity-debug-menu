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

        [SerializeField]
        private RectTransform m_debugButton;

        [SerializeField]
        private RectTransform m_background;

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

            // create the main selection menu
            var selectionSection = Instantiate(m_registry.debugMenuSection, m_container);
            var selectionContainer = new DefaultContainer(selectionSection, m_registry);

            // create all inner debug menus
            foreach (var menu in m_menus) {
                selectionContainer.Section(menu.Name(), menu.Construct);
            }

            // hide by default
            m_selectionSection = selectionSection.gameObject;
            m_selectionSection.SetActive(false);
        }

        public void OnDebugButtonPressed()
        {
            m_selectionSection.SetActive(!m_selectionSection.activeSelf);
            m_background?.gameObject.SetActive(m_selectionSection.activeSelf);
        }

        public void SetDebugButtonVerticalAnchor(float value)
        {
            var buttonTransform = m_debugButton.gameObject.GetComponent<RectTransform>();
            buttonTransform.anchorMin = new Vector2(buttonTransform.anchorMin.x, value);
            buttonTransform.anchorMax = new Vector2(buttonTransform.anchorMax.x, value);
            buttonTransform.pivot = new Vector2(buttonTransform.pivot.x, value);
        }
    }
}
