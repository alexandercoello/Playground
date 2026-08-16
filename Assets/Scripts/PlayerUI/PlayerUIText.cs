using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.PlayerUI
{
    public class PlayerUIText : MonoBehaviour
    {
        private TextMeshProUGUI uiText;

        private void Start()
        {
            uiText = GetComponent<TextMeshProUGUI>();
        }

        public void UpdatePlayerUI(string newText)
        {
            if(uiText == null)
            {
                Start();
            }
           
            uiText.SetText(newText);
        }

        public void ShowPlayerUI()
        {
            if(!uiText.IsUnityNull())
            {
                uiText.enabled = true;
            }
        }

        public void HidePlayerUI()
        {
            if(!uiText.IsUnityNull())
            {
                uiText.enabled = false;
            }
        }
    }
}