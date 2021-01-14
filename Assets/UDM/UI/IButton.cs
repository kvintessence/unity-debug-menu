using System;

namespace UDM
{
    namespace UI
    {
        public interface IButton
        {
            IButton OnClick(Action action);
        }
    }
}
