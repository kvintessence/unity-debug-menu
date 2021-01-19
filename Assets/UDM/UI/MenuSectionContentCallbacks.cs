using UnityEngine;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - Menu Section - Content Callbacks")]
        public class MenuSectionContentCallbacks : MonoBehaviour
        {
            public ADebugMenu debugMenu = null;

            private void Update()
            {
                debugMenu?.Update();
            }

            private void OnEnable()
            {
                debugMenu?.OnEnable();
                debugMenu?.OnEnabledChanged(true);
            }

            private void OnDisable()
            {
                debugMenu?.OnEnabledChanged(false);
                debugMenu?.OnDisable();
            }
        }
    }
}
