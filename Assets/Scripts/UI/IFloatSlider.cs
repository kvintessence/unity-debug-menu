using System;

namespace UDM
{
    namespace UI
    {
        public interface IFloatSlider
        {
            IFloatSlider ShowValue(int digits = 2);
            IFloatSlider OnValueChanged(Action<float> action);
            IFloatSlider MinMax(float minValue, float maxValue);
        }
    }
}
