using System;

namespace UDM
{
    namespace UI
    {
        public interface ILabelWithValue<out T>
        {
            ILabelWithValue<T> CustomNaming(Func<T, string> namingFunction);
        }
    }
}
