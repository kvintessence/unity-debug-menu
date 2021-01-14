using System;
using UnityEngine;
using UnityEngine.UI;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - Label with Value")]
        public class DefaultLabelWithValue : MonoBehaviour, ILabel
        {
            [SerializeField]
            public Text keyText;
            [SerializeField]
            public Text valueText;

            public Action onStart = null;
            public Action onUpdate = null;

            private void Start()
            {
                onStart?.Invoke();
            }

            private void Update()
            {
                onUpdate?.Invoke();
            }
        }

        public class DefaultLabelWithValue<T> : ILabelWithValue<T>
        {
            private Func<T, string> m_valueFunction = v => $"{v}";
            private DefaultLabelWithValue m_label;

            private T m_value = default;
            private Func<T> m_valueGetter = null;
            private string m_key = "?";

            /************************************************************************************************/

            public ILabelWithValue<T> SetRealLabel(DefaultLabelWithValue dropdown)
            {
                m_label = dropdown;
                m_label.onStart = Start;
                m_label.onUpdate = Update;
                return this;
            }

            public ILabelWithValue<T> Init(string key, T value)
            {
                m_key = key;
                m_value = value;
                return this;
            }

            public ILabelWithValue<T> Init(string key, Func<T> valueGetter)
            {
                m_key = key;
                m_valueGetter = valueGetter;
                return this;
            }

            public ILabelWithValue<T> CustomNaming(Func<T, string> valueFunction)
            {
                m_valueFunction = valueFunction;
                return this;
            }

            /************************************************************************************************/

            private void Start()
            {
                m_label.keyText.text = $"{m_key}:";
                m_value = (m_valueGetter != null) ? m_valueGetter() : m_value;
                RecreateValueLabel(m_value);
            }

            private void Update()
            {
                if (m_valueGetter == null)
                    return;

                var realValue = m_valueGetter();
                if (Equals(realValue, m_value))
                    return;

                m_value = realValue;
                RecreateValueLabel(m_value);
            }

            private void RecreateValueLabel(T value)
            {
                m_label.valueText.text = m_valueFunction(value);
            }
        }
    }
}
