using System;
using UnityEngine;
using UnityEngine.UI;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - CheckBox")]
        public class DefaultCheckBox : MonoBehaviour, ICheckBox
        {
            [SerializeField]
            private Text m_title;

            [SerializeField]
            private Toggle m_toggle;

            private Action<bool> m_onChanged = null;

            private Func<bool> m_valueGetter = null;
            private bool m_value = false;

            /************************************************************************************************/

            public ICheckBox SetValue(bool value)
            {
                m_value = value;
                return this;
            }

            public ICheckBox SetValue(Func<bool> valueGetter)
            {
                m_valueGetter = valueGetter;
                return this;
            }

            public ICheckBox SetTitle(string title)
            {
                m_title.text = title;
                return this;
            }

            public ICheckBox OnValueChanged(Action<bool> action)
            {
                m_onChanged = action;
                return this;
            }

            /************************************************************************************************/

            private void Start()
            {
                m_value = m_valueGetter?.Invoke() ?? m_value;
                m_toggle.SetIsOnWithoutNotify(m_value);
            }

            private void Update()
            {
                if (m_valueGetter == null)
                    return;

                var realValue = m_valueGetter();
                if (Equals(realValue, m_value))
                    return;

                m_value = realValue;
                m_toggle.SetIsOnWithoutNotify(m_value);
                m_onChanged?.Invoke(m_value);
            }

            public void OnToggleValueChanged(bool newValue)
            {
                m_value = newValue;
                m_onChanged?.Invoke(m_value);
            }
        }
    }
}
