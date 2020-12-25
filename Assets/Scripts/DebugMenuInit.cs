using System;
using System.Collections.Generic;
using System.Linq;
using UDM.UI;
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
            DontDestroyOnLoad(menuInstance);
        }
    }
}
