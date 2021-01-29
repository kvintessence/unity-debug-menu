using System;
using UnityEngine;

namespace UDM
{
    namespace UI
    {
        public class CustomCallbacksComponent : MonoBehaviour
        {
            public event Action OnStart = null;
            public event Action OnUpdate = null;

            private void Start()
            {
                OnStart?.Invoke();
            }

            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }
    }
}
