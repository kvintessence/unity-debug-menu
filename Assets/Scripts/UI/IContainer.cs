﻿using System;

namespace UDM
{
    namespace UI
    {
        public interface IContainer
        {
            ILabel Label(string text);
            ILabel Label(Func<string> text);

            IFloatSlider FloatSlider(float currentValue);
            IFloatSlider FloatSlider(Func<float> valueGetter);

            IButton Button(string title);
            IButton Button(Func<string> titleGetter);

            void Section(string name, Action<IContainer> sectionConstructor);
        }
    }
}
