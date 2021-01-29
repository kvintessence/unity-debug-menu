using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - Dropdown")]
        public class DefaultDropdown : MonoBehaviour
        {
            [SerializeField]
            public Dropdown dropdown;

            public Action onStart = null;
            public Action onUpdate = null;
            public Action<int> onIndexChanged = null;

            private void Start()
            {
                onStart?.Invoke();
            }

            private void Update()
            {
                onUpdate?.Invoke();
            }

            public void OnSelectedIndexChanged(int newIndex)
            {
                onIndexChanged?.Invoke(newIndex);
            }
        }

        public class DefaultDropdown<T> : IDropdown<T>
        {
            private Action<T> m_onChanged = null;
            private Func<T, string> m_namingFunction = null;
            private DefaultDropdown m_dropdown;

            private T m_value = default;
            private Func<T> m_valueGetter = null;
            private IList<T> m_options = null;
            private IList<T> m_cachedOptions = null;

            /************************************************************************************************/

            public IDropdown<T> SetRealDropdown(DefaultDropdown dropdown)
            {
                m_dropdown = dropdown;
                m_dropdown.onStart = Start;
                m_dropdown.onUpdate = Update;
                m_dropdown.onIndexChanged = OnSelectedIndexChanged;
                return this;
            }

            public IDropdown<T> Init(T value, IList<T> options)
            {
                m_value = value;
                m_options = options;
                return this;
            }

            public IDropdown<T> Init(Func<T> valueGetter, IList<T> options)
            {
                m_valueGetter = valueGetter;
                m_options = options;
                return this;
            }

            public IDropdown<T> CustomNaming(Func<T, string> namingFunction)
            {
                m_namingFunction = namingFunction;
                return this;
            }

            public IDropdown<T> OnValueChanged(Action<T> action)
            {
                m_onChanged = action;
                return this;
            }

            public IDropdown<T> ProvideNewOptions(IList<T> options)
            {
                m_options = options;

                if (m_cachedOptions != null)
                    RecreateDropdown(m_dropdown.dropdown.value);

                return this;
            }

            /************************************************************************************************/

            private void Start()
            {
                var defaultValue = (m_valueGetter != null) ? m_valueGetter() : m_value;
                RecreateDropdown(index: GetIndexOfValue(defaultValue));
                OnSelectedIndexChanged(newIndex: m_dropdown.dropdown.value);
            }

            private void Update()
            {
                CheckIfOptionsChanged();

                if (m_valueGetter == null)
                    return;

                var realValue = m_valueGetter();
                if (Equals(realValue, m_value))
                    return;

                m_value = realValue;
                m_dropdown.dropdown.value = GetIndexOfValue(realValue);
                m_onChanged?.Invoke(m_value);
            }

            private bool HasOptionsChanged()
            {
                if (m_cachedOptions == null)
                    return false;

                if (m_cachedOptions.Count != m_options.Count)
                    return true;

                for (var i = 0; i < m_cachedOptions.Count; ++i)
                    if (!m_options[i].Equals(m_cachedOptions[i]))
                        return true;

                return false;
            }

            private void CheckIfOptionsChanged()
            {
                if (!HasOptionsChanged())
                    return;

                RecreateDropdown(m_dropdown.dropdown.value);
                OnSelectedIndexChanged(m_dropdown.dropdown.value);
            }

            private void OnSelectedIndexChanged(int newIndex)
            {
                if (m_options.Count <= newIndex)
                    return;

                var realValue = (m_valueGetter != null) ? m_valueGetter() : m_value;
                m_value = m_options[newIndex];

                if (Equals(realValue, m_value))
                    return;

                m_onChanged?.Invoke(m_value);
            }

            private int GetIndexOfValue(T value)
            {
                return m_options.Contains(value) ? m_options.IndexOf(value) : 0;
            }

            private void RecreateDropdown(int index)
            {
                m_cachedOptions = new List<T>(m_options);
                m_dropdown.dropdown.options = m_options.Select((v) => {
                    var option = new Dropdown.OptionData();
                    option.text = (m_namingFunction != null) ? m_namingFunction(v) : $"{v}";
                    return option;
                }).ToList();

                if (index < 0 || index >= m_dropdown.dropdown.options.Count)
                    index = 0;

                m_dropdown.dropdown.value = index;
            }
        }
    }
}
