using System;
using UnityEngine;
using UnityEngine.UI;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - Empty Container - If")]
        public class EmptyContainerIf : EmptyContainer
        {
            [SerializeField]
            public LayoutElement layoutElement;

            public Func<bool> condition = null;
            private bool m_isActive = true;

            public void Start()
            {
                m_isActive = content.gameObject.activeSelf;
                layoutElement.ignoreLayout = !m_isActive;
                Update();
            }

            public void Update()
            {
                if (condition == null)
                    return;

                var isActive = condition();
                if (isActive == m_isActive)
                    return;

                m_isActive = isActive;
                content.gameObject.SetActive(m_isActive);
                layoutElement.ignoreLayout = !m_isActive;
            }
        }
    }
}
