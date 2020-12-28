using UnityEngine;

namespace UDM
{
    namespace UI
    {
        [CreateAssetMenu(fileName = "DefaultContainerRegistry", menuName = "ScriptableObjects/DefaultContainerRegistry")]
        public class DefaultContainerRegistry : ScriptableObject
        {
            public DefaultButton button;
            public DefaultCheckBox checkBox;
            public DefaultFloatSlider floatSlider;
            public DefaultIntSlider intSlider;
            public DefaultLabel label;
            public DefaultLabelWithValue labelWithValue;
            public DefaultDropdown dropdown;

            public GameObject separator;
            public EmptyContainerIf emptyContainerIf;
            public MenuSection debugMenuSection;
        }
    }
}
