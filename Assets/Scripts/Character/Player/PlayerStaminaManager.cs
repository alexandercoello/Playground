using System;
using Unity.VisualScripting;
using UnityEngine;
using Scripts.PlayerUI;

namespace Scripts.Character.Player
{
    public class PlayerStaminaManager : BaseStaminaManager
    {
        
        public PlayerUIText PlayerUIStaminaText;

        protected override void Start()
        {
            base.Start();
            ShowPlayerStaminaUI();
        }

        private void ShowPlayerStaminaUI()
        {
            if(PlayerUIStaminaText != null)
            {
                PlayerUIStaminaText.ShowPlayerUI();
            }
        }

        private void HidePlayerStaminaUI()
        {
            if(PlayerUIStaminaText != null)
            {
                PlayerUIStaminaText.HidePlayerUI();
            }                
        }

        protected override void CheckAndUpdateStaminaUI()
        {
            if(PlayerUIStaminaText != null)
            {
                PlayerUIStaminaText.UpdatePlayerUI($"{Current}/{Max}");           
            }
        }
        
        
    }
}