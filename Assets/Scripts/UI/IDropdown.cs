using System;

namespace UDM
{
    namespace UI
    {
        public interface IDropdown<out T>
        {
            IDropdown<T> CustomNaming(Func<T, string> namingFunction);
            IDropdown<T> OnValueChanged(Action<T> action);
        }
    }
}
