using System;
using UnityEngine;
using UnityEngine.UI;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - IntSlider")]
        public class DefaultIntSlider : MonoBehaviour, IIntSlider
        {
            [SerializeField]
            private Text m_valueText;

            [SerializeField]
            private Slider m_slider;

            private Action<int> m_onChanged = null;
            private int m_min = 0;
            private int m_max = 100;
            private bool m_initialized = false;

            private int m_previousValue = 0;
            private Func<int> m_valueGetter = null;

            /************************************************************************************************/

            public IIntSlider SetValue(int value)
            {
                m_previousValue = value;
                return this;
            }

            public IIntSlider SetValue(Func<int> getter)
            {
                m_valueGetter = getter;
                return this;
            }

            public IIntSlider ShowValue(bool show)
            {
                m_valueText.gameObject.SetActive(show);
                return this;
            }

            public IIntSlider OnValueChanged(Action<int> action)
            {
                m_onChanged = action;
                return this;
            }

            public IIntSlider MinMax(int minValue, int maxValue)
            {
                if (minValue > maxValue) {
                    var temp = minValue;
                    minValue = maxValue;
                    maxValue = temp;
                }

                m_min = minValue;
                m_max = maxValue;

                return this;
            }

            /************************************************************************************************/

            private void Start()
            {
                m_slider.minValue = m_min;
                m_slider.maxValue = m_max;
                m_previousValue = Math.Max(m_min, Math.Min(m_max, m_previousValue));
                m_initialized = true;
                m_slider.value = m_valueGetter?.Invoke() ?? m_previousValue;
                UpdateValueText((int)m_slider.value);
            }

            private void Update()
            {
                var realValue = GetRealValue();
                SyncValueEverywhere(realValue, realValue);
            }

            private int GetRealValue()
            {
                return m_valueGetter?.Invoke() ?? (int)m_slider.value;
            }

            public void OnSliderValueChanged(float newValue)
            {
                if (!m_initialized)
                    return;

                SyncValueEverywhere((int)newValue);
            }

            private void SyncValueEverywhere(int newValue, int? realValueOptional = null)
            {
                newValue = Math.Max(m_min, Math.Min(m_max, newValue));
                var realValue = realValueOptional ?? m_valueGetter?.Invoke() ?? m_previousValue;
                var sliderValue = (int) m_slider.value;

                if (!Mathf.Approximately(realValue, newValue)) {
                    UpdateValueText(newValue);
                    m_onChanged?.Invoke(newValue);
                }

                if (!Mathf.Approximately(sliderValue, newValue)) {
                    UpdateValueText(newValue);
                    m_slider.value = realValue;
                }

                m_previousValue = sliderValue;
            }

            private void UpdateValueText(int value)
            {
                m_valueText.text = $"{value}";
            }
        }
    }
}
