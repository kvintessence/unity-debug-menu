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

            ICheckBox ToggleButton(string title, bool value);
            ICheckBox ToggleButton(string title, Func<bool> valueGetter);

            ICheckBox CheckBox(string title, bool value);
            ICheckBox CheckBox(string title, Func<bool> valueGetter);

            IDropdown<T> Dropdown<T>(T value) where T : Enum;
            IDropdown<T> Dropdown<T>(Func<T> valueGetter) where T : Enum;
            IDropdown<T> Dropdown<T>(T value, IEnumerable<T> options);
            IDropdown<T> Dropdown<T>(Func<T> valueGetter, IEnumerable<T> options);

            void Section(ADebugMenu section);
            void Section(string name, Action<IContainer> sectionConstructor);

            void ShowIf(Func<bool> condition, Action<IContainer> sectionConstructor);
            void Horizontal(Action<IContainer> sectionConstructor);

            void Separator();

            void HideMenu();
        }
    }
}
