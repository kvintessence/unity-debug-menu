using UnityEngine;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - Custom Container")]
        public class CustomContainer : MonoBehaviour
        {
            [SerializeField]
            public Transform content;
        }
    }
}
