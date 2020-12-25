using System;
using UnityEngine;

namespace UDM
{
    namespace UI
    {
        public class DefaultContainer : IContainer
        {
            private readonly Transform m_parent;
            private readonly MenuSection m_section;
            private readonly DefaultContainerRegistry m_registry;

            public DefaultContainer(MenuSection section, DefaultContainerRegistry registry)
            {
                m_section = section;
                m_registry = registry;
                m_parent = m_section.content;
            }

            public DefaultContainer(DefaultContainer anotherContainer)
            {
                m_section = anotherContainer.m_section;
                m_registry = anotherContainer.m_registry;
                m_parent = m_section.content;
            }

            public ILabel Label(string text)
            {
                var instance = UnityEngine.Object.Instantiate(m_registry.label, m_parent);
                return instance.SetText(text);
            }

            public ILabel Label(Func<string> text)
            {
                var instance = UnityEngine.Object.Instantiate(m_registry.label, m_parent);
                return instance.SetText(text);
            }

            public IFloatSlider FloatSlider(float currentValue)
            {
                var instance = UnityEngine.Object.Instantiate(m_registry.floatSlider, m_parent);
                return instance.SetValue(currentValue);
            }

            public IFloatSlider FloatSlider(Func<float> valueGetter)
            {
                var instance = UnityEngine.Object.Instantiate(m_registry.floatSlider, m_parent);
                return instance.SetValue(valueGetter);
            }

            public IButton Button(string title)
            {
                var instance = UnityEngine.Object.Instantiate(m_registry.button, m_parent);
                return instance.Title(title);
            }

            public IButton Button(Func<string> titleGetter)
            {
                var instance = UnityEngine.Object.Instantiate(m_registry.button, m_parent);
                return instance.Title(titleGetter);
            }
        }
    }
}
