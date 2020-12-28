using System;
using System.Collections.Generic;

namespace UDM
{
    namespace UI
    {
        public interface IContainer
        {
            ILabel Label(string text);
            ILabelWithValue<T> LabelValue<T>(string key, T value);
            ILabelWithValue<T> LabelValue<T>(string key, Func<T> value);

            IFloatSlider FloatSlider(float currentValue);
            IFloatSlider FloatSlider(Func<float> valueGetter);

            IIntSlider IntSlider(int currentValue);
            IIntSlider IntSlider(Func<int> valueGetter);

            IButton Button(string title);
            IButton Button(Func<string> titleGetter);

            ICheckBox CheckBox(string title, bool value);
            ICheckBox CheckBox(string title, Func<bool> valueGetter);

            IDropdown<T> Dropdown<T>(T value) where T : Enum;
            IDropdown<T> Dropdown<T>(Func<T> valueGetter) where T : Enum;
            IDropdown<T> Dropdown<T>(T value, List<T> options);
            IDropdown<T> Dropdown<T>(Func<T> valueGetter, List<T> options);

            void Section(string name, Action<IContainer> sectionConstructor);

            void Separator();
        }
    }
}
