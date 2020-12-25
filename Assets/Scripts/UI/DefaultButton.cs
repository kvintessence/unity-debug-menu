using System;
using UnityEngine;
using UnityEngine.UI;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - Button")]
        public class DefaultButton : MonoBehaviour, IButton
        {
            [SerializeField]
            private Text m_titleText;

            private string m_title = "";
            private Func<string> m_titleGetter = null;
            private Action m_onClick = null;

            /************************************************************************************************/

            public IButton Title(string title)
            {
                m_title = title;
                return this;
            }

            public IButton Title(Func<string> titleGetter)
            {
                m_titleGetter = titleGetter;
                return this;
            }

            public IButton OnClick(Action action)
            {
                m_onClick = action;
                return this;
            }

            /************************************************************************************************/

            private void Awake()
            {
                m_titleText.text = m_titleGetter?.Invoke() ?? m_title;
            }

            private void Update()
            {
                if (m_titleGetter == null)
                    return;

                var newText = m_titleGetter();
                if (newText.Equals(m_titleText.text))
                    return;

                m_titleText.text = newText;
            }

            public void OnButtonClicked()
            {
                m_onClick?.Invoke();
            }
        }
    }
}
