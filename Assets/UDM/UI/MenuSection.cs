using UnityEngine;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - Menu Section")]
        public class MenuSection : MonoBehaviour
        {
            [SerializeField]
            public Transform content;
            [SerializeField]
            public Transform subSections;
            [SerializeField]
            public MenuSectionContentCallbacks contentCallbacks;
        }
    }
}
