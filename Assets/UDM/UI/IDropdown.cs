using System;
using System.Collections.Generic;

namespace UDM
{
    namespace UI
    {
        public interface IDropdown<T>
        {
            IDropdown<T> ProvideNewOptions(IList<T> options);
            IDropdown<T> CustomNaming(Func<T, string> namingFunction);
            IDropdown<T> OnValueChanged(Action<T> action);
        }
    }
}
