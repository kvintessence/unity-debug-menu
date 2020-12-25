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
            public DefaultLabel label;

            public GameObject separator;
            public MenuSection debugMenuSection;
        }
    }
}
