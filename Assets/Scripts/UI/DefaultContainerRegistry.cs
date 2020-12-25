using UnityEngine;

namespace UDM
{
    namespace UI
    {
        [CreateAssetMenu(fileName = "DefaultContainerRegistry", menuName = "ScriptableObjects/DefaultContainerRegistry")]
        public class DefaultContainerRegistry : ScriptableObject
        {
            public DefaultButton button;
            public DefaultFloatSlider floatSlider;
            public DefaultIntSlider intSlider;
            public DefaultLabel label;
            public DefaultDropdown dropdown;

            public GameObject separator;
            public MenuSection debugMenuSection;
        }
    }
}
