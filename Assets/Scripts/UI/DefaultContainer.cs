using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

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

            public ILabel Label(string text)
            {
                var instance = Object.Instantiate(m_registry.label, m_parent);
                return instance.SetText(text);
            }

            public ILabel Label(Func<string> text)
            {
                var instance = Object.Instantiate(m_registry.label, m_parent);
                return instance.SetText(text);
            }

            public IFloatSlider FloatSlider(float currentValue)
            {
                var instance = Object.Instantiate(m_registry.floatSlider, m_parent);
                return instance.SetValue(currentValue);
            }

            public IFloatSlider FloatSlider(Func<float> valueGetter)
            {
                var instance = Object.Instantiate(m_registry.floatSlider, m_parent);
                return instance.SetValue(valueGetter);
            }

            public IIntSlider IntSlider(int currentValue)
            {
                var instance = Object.Instantiate(m_registry.intSlider, m_parent);
                return instance.SetValue(currentValue);
            }

            public IIntSlider IntSlider(Func<int> valueGetter)
            {
                var instance = Object.Instantiate(m_registry.intSlider, m_parent);
                return instance.SetValue(valueGetter);
            }

            public IButton Button(string title)
            {
                var instance = Object.Instantiate(m_registry.button, m_parent);
                return instance.Title(title);
            }

            public IButton Button(Func<string> titleGetter)
            {
                var instance = Object.Instantiate(m_registry.button, m_parent);
                return instance.Title(titleGetter);
            }

            public IDropdown<T> Dropdown<T>(T value) where T : Enum
            {
                var instance = Object.Instantiate(m_registry.dropdown, m_parent);
                var dropdown = new DefaultDropdown<T>();
                dropdown.SetRealDropdown(instance);
                dropdown.Init(value, Enum.GetValues(typeof(T)).Cast<T>().ToList());
                return dropdown;
            }

            public IDropdown<T> Dropdown<T>(Func<T> valueGetter) where T : Enum
            {
                var instance = Object.Instantiate(m_registry.dropdown, m_parent);
                var dropdown = new DefaultDropdown<T>();
                dropdown.SetRealDropdown(instance);
                dropdown.Init(valueGetter, Enum.GetValues(typeof(T)).Cast<T>().ToList());
                return dropdown;
            }

            public IDropdown<T> Dropdown<T>(T value, List<T> options)
            {
                var instance = Object.Instantiate(m_registry.dropdown, m_parent);
                var dropdown = new DefaultDropdown<T>();
                dropdown.SetRealDropdown(instance);
                dropdown.Init(value, options);
                return dropdown;
            }

            public IDropdown<T> Dropdown<T>(Func<T> valueGetter, List<T> options)
            {
                var instance = Object.Instantiate(m_registry.dropdown, m_parent);
                var dropdown = new DefaultDropdown<T>();
                dropdown.SetRealDropdown(instance);
                dropdown.Init(valueGetter, options);
                return dropdown;
            }

            public void Separator()
            {
                Object.Instantiate(m_registry.separator, m_parent);
            }

            public void Section(string name, Action<IContainer> sectionConstructor)
            {
                var innerSection = Object.Instantiate(m_registry.debugMenuSection, m_section.subSections);
                var container = new DefaultContainer(innerSection, m_registry);
                sectionConstructor(container);

                var outerSection = m_section;
                Button(name).OnClick(() => {
                    // disable all other sections
                    foreach (Transform subSection in outerSection.subSections) {
                        if (!ReferenceEquals(innerSection.gameObject, subSection.gameObject))
                            subSection.gameObject.SetActive(false);
                    }

                    // enable this particular section
                    innerSection.gameObject.SetActive(!innerSection.gameObject.activeSelf);
                });

                innerSection.gameObject.SetActive(false);
            }
        }
    }
}
