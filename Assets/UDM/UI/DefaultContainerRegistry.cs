﻿using UnityEngine;

namespace UDM
{
    namespace UI
    {
        [CreateAssetMenu(fileName = "DefaultContainerRegistry", menuName = "ScriptableObjects/DefaultContainerRegistry")]
        public class DefaultContainerRegistry : ScriptableObject
        {
            public DefaultButton button;
            public DefaultCheckBox checkBox;
            public DefaultCheckBox toggleButton;
            public DefaultFloatSlider floatSlider;
            public DefaultIntSlider intSlider;
            public DefaultLabel label;
            public DefaultLabelWithValue labelWithValue;
            public DefaultDropdown dropdown;

            public GameObject separator;
            public EmptyContainerIf emptyContainerIf;
            public EmptyContainer emptyContainerBackground;
            public EmptyContainer emptyContainerHorizontal;
            public EmptyContainer emptyContainerVertical;
            public CustomCallbacksComponent customCallbacks;
            public MenuSection debugMenuSection;
        }
    }
}
