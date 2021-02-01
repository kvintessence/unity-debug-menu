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

            public DefaultContainer(MenuSection section, Transform content, DefaultContainerRegistry registry)
            {
                m_section = section;
                m_registry = registry;
                m_parent = content;
            }

            public DefaultContainer WithContent(Transform content)
            {
                return new DefaultContainer(m_section, content, m_registry);
            }

            public ILabel Label(string text)
            {
                var instance = Object.Instantiate(m_registry.label, m_parent);
                return instance.SetText(text);
            }

            public ILabelWithValue<T> LabelValue<T>(string key, Func<T> value)
            {
                var instance = Object.Instantiate(m_registry.labelWithValue, m_parent);
                var labelWithValue = new DefaultLabelWithValue<T>();
                labelWithValue.SetRealLabel(instance);
                labelWithValue.Init(key, value);
                return labelWithValue;
            }

            public ILabelWithValue<T> LabelValue<T>(string key, T value)
            {
                var instance = Object.Instantiate(m_registry.labelWithValue, m_parent);
                var labelWithValue = new DefaultLabelWithValue<T>();
                labelWithValue.SetRealLabel(instance);
                labelWithValue.Init(key, value);
                return labelWithValue;
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
                instance.SetHideMenuOnClickAction(HideMenu);
                return instance.Title(title);
            }

            public ICheckBox ToggleButton(string title, bool value)
            {
                var instance = Object.Instantiate(m_registry.toggleButton, m_parent);
                instance.SetTitle(title);
                return instance.SetValue(value);
            }

            public ICheckBox ToggleButton(string title, Func<bool> valueGetter)
            {
                var instance = Object.Instantiate(m_registry.toggleButton, m_parent);
                instance.SetTitle(title);
                return instance.SetValue(valueGetter);
            }

            public ICheckBox CheckBox(string title, bool value)
            {
                var instance = Object.Instantiate(m_registry.checkBox, m_parent);
                instance.SetTitle(title);
                return instance.SetValue(value);
            }

            public ICheckBox CheckBox(string title, Func<bool> valueGetter)
            {
                var instance = Object.Instantiate(m_registry.checkBox, m_parent);
                instance.SetTitle(title);
                return instance.SetValue(valueGetter);
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

            public IDropdown<T> Dropdown<T>(T value, IList<T> options)
            {
                var instance = Object.Instantiate(m_registry.dropdown, m_parent);
                var dropdown = new DefaultDropdown<T>();
                dropdown.SetRealDropdown(instance);
                dropdown.Init(value, options);
                return dropdown;
            }

            public IDropdown<T> Dropdown<T>(Func<T> valueGetter, IList<T> options)
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

            public void Section(ADebugMenu debugMenu)
            {
                Section(debugMenu.Name(), debugMenu.Construct, debugMenu);
            }

            public void Section(string name, Action<IContainer> sectionConstructor)
            {
                Section(name, sectionConstructor, debugMenu: null);
            }

            private void Section(string name, Action<IContainer> sectionConstructor, ADebugMenu debugMenu)
            {
                var innerSection = Object.Instantiate(m_registry.debugMenuSection, m_section.subSections);
                innerSection.contentCallbacks.debugMenu = debugMenu;
                var container = new DefaultContainer(innerSection, m_registry);
                sectionConstructor(container);

                var outerSection = m_section;
                var innerSectionObject = innerSection.gameObject;
                innerSectionObject.SetActive(false);

                var toggleButton = Object.Instantiate(m_registry.toggleButton, m_parent);
                toggleButton.SetTitle(name);
                toggleButton.SetValue(() => innerSectionObject.activeSelf);
                toggleButton.OnValueChanged((active) => {
                    // disable all other sections
                    if (active) {
                        foreach (Transform subSection in outerSection.subSections) {
                            if (!ReferenceEquals(innerSectionObject, subSection.gameObject))
                                subSection.gameObject.SetActive(false);
                        }
                    }

                    // enable/disable this particular section
                    innerSectionObject.SetActive(active);
                });
                toggleButton.onDisable += () => {
                    // force hide when the button is unavailable BUT the menu is visible
                    if (m_section.gameObject.activeInHierarchy)
                        innerSectionObject.SetActive(false);
                };
            }

            public IShowForEach<T> ShowForEach<T>(IList<T> values, Action<IContainer, T> sectionConstructor)
            {
                var visual = Object.Instantiate(m_registry.customCallbacks, m_parent);
                var instance = new DefaultShowForEach<T>();
                instance.SetVisualElement(visual);
                instance.SetContainer(this);
                instance.Init(values, sectionConstructor);
                return instance;
            }

            public EmptyContainer EmptyContainerVertical()
            {
                return Object.Instantiate(m_registry.emptyContainerVertical, m_parent);
            }

            public void ShowIf(Func<bool> condition, Action<IContainer> sectionConstructor)
            {
                ShowIfEx(condition, sectionConstructor);
            }

            public EmptyContainerIf ShowIfEx(Func<bool> condition, Action<IContainer> sectionConstructor)
            {
                var innerSection = Object.Instantiate(m_registry.emptyContainerIf, m_parent);
                innerSection.condition = condition;

                var container = new DefaultContainer(m_section, innerSection.content, m_registry);
                sectionConstructor(container);
                return innerSection;
            }

            public void Horizontal(Action<IContainer> sectionConstructor)
            {
                var innerSection = Object.Instantiate(m_registry.emptyContainerHorizontal, m_parent);
                var container = new DefaultContainer(m_section, innerSection.content, m_registry);
                sectionConstructor(container);
            }

            public void WithBackground(Action<IContainer> sectionConstructor)
            {
                var innerSection = Object.Instantiate(m_registry.emptyContainerBackground, m_parent);
                var container = new DefaultContainer(m_section, innerSection.content, m_registry);
                sectionConstructor(container);
            }

            public void HideMenu()
            {
                foreach (var debugMenu in Object.FindObjectsOfType<DebugMenuController>())
                    debugMenu.SetDebugMenuShown(false);
            }
        }
    }
}
