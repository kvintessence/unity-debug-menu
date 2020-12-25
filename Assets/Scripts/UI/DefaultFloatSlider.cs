using System;
using UnityEngine;
using UnityEngine.UI;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - FloatSlider")]
        public class DefaultFloatSlider : MonoBehaviour, IFloatSlider
        {
            [SerializeField]
            private Text m_valueText;

            [SerializeField]
            private Slider m_slider;

            private int m_showValueDigits = 2;
            private Action<float> m_onChanged = null;
            private float m_min = 0.0f;
            private float m_max = 1.0f;

            private float m_previousValue = 0.0f;
            private Func<float> m_valueGetter = null;

            /************************************************************************************************/

            public IFloatSlider SetValue(float value)
            {
                m_previousValue = value;
                return this;
            }

            public IFloatSlider SetValue(Func<float> getter)
            {
                m_valueGetter = getter;
                return this;
            }

            public IFloatSlider ShowValue(int digits)
            {
                m_valueText.gameObject.SetActive(true);
                m_showValueDigits = digits;
                return this;
            }

            public IFloatSlider OnValueChanged(Action<float> action)
            {
                Debug.Log("OnValueChanged");
                m_onChanged = action;
                return this;
            }

            public IFloatSlider MinMax(float minValue, float maxValue)
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
                m_slider.value = m_valueGetter?.Invoke() ?? m_previousValue;
                UpdateValueText(m_slider.value);
            }

            private void Update()
            {
                var realValue = GetRealValue();
                SyncValueEverywhere(realValue, realValue);
            }

            private float GetRealValue()
            {
                return m_valueGetter?.Invoke() ?? m_slider.value;
            }

            public void OnSliderValueChanged(float newValue)
            {
                SyncValueEverywhere(newValue);
            }

            private void SyncValueEverywhere(float newValue, float? realValueOptional = null)
            {
                newValue = Math.Max(m_min, Math.Min(m_max, newValue));
                var realValue = realValueOptional ?? m_valueGetter?.Invoke() ?? m_previousValue;

                if (!Mathf.Approximately(realValue, newValue)) {
                    UpdateValueText(newValue);
                    m_onChanged?.Invoke(newValue);
                }

                if (!Mathf.Approximately(m_slider.value, newValue)) {
                    UpdateValueText(newValue);
                    m_slider.value = realValue;
                }

                m_previousValue = m_slider.value;
            }

            private void UpdateValueText(float value)
            {
                m_valueText.text = $"{Math.Round(value, m_showValueDigits)}";
            }
        }
    }
}
