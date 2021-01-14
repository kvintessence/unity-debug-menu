using UnityEngine;

namespace UDM
{
    public class DebugMenuInit : MonoBehaviour
    {
        [SerializeField]
        private DebugMenuController m_menu;

        private void Awake()
        {
            var menuInstance = Instantiate(m_menu);
            menuInstance.SetDebugButtonVerticalAnchor(0.33f);
            DontDestroyOnLoad(menuInstance);
        }
    }
}
