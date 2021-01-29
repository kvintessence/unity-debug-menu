using System;
using System.Collections.Generic;

namespace UDM
{
    namespace UI
    {
        public interface IShowForEach<T>
        {
            IShowForEach<T> ProvideNewOptions(IList<T> options);
            IShowForEach<T> CustomNaming(Func<T, string> namingFunction);
            IShowForEach<T> OnValueChanged(Action<T> action);
            IShowForEach<T> ShowEverything(bool everything = true);
        }
    }
}
