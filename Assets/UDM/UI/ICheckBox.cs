using System;

namespace UDM
{
    namespace UI
    {
        public interface ICheckBox
        {
            ICheckBox OnValueChanged(Action<bool> action);
        }
    }
}
