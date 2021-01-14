using System;

namespace UDM
{
    namespace UI
    {
        public interface IIntSlider
        {
            IIntSlider ShowValue(bool show = true);
            IIntSlider OnValueChanged(Action<int> action);
            IIntSlider MinMax(int minValue, int maxValue);
        }
    }
}
