using System;
using UnityEngine;
using UnityEngine.UI;

namespace UDM
{
    namespace UI
    {
        [AddComponentMenu("UDM/UDM - Label")]
        public class DefaultLabel : MonoBehaviour, ILabel
        {
            [SerializeField]
            private Text m_text;

            private string m_initialText = "";
            private Func<string> m_textGetter;

            /************************************************************************************************/

            public ILabel SetText(string text)
            {
                m_initialText = text;
                return this;
            }

            public ILabel SetText(Func<string> textGetter)
            {
                m_textGetter = textGetter;
                return this;
            }

            /************************************************************************************************/

            private void Start()
            {
                m_text.text = GetText();
            }

            private void Update()
            {
                if (m_textGetter == null)
                    return;

                var newText = GetText();

                if (!newText.Equals(m_text.text)) {
                    m_text.text = newText;
                }
            }

            private string GetText()
            {
                return m_textGetter?.Invoke() ?? m_initialText;
            }
        }
    }
}
