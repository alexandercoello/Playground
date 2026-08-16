using System;
using Unity.VisualScripting;
using UnityEngine;
using Scripts.PlayerUI;

namespace Scripts.Character.Player
{
    public class PlayerHealthManager : BaseHealthManager
    {
        
        public PlayerUIText PlayerUIHealthText;

        protected override void Start()
        {
            base.Start();
            ShowPlayerHealthUI();
        }

        private void ShowPlayerHealthUI()
        {
            if(PlayerUIHealthText != null)
            {
                PlayerUIHealthText.ShowPlayerUI();
            }
        }

        private void HidePlayerHealthUI()
        {
            if(PlayerUIHealthText != null)
            {
                PlayerUIHealthText.HidePlayerUI();
            }                
        }

        protected override void CheckAndUpdateHealthUI()
        {
            if(PlayerUIHealthText != null)
            {
                PlayerUIHealthText.UpdatePlayerUI($"{CurrentHealth}/{MaxHealth}");                
            }
        }

        protected override void TriggerDeath()
        {            
            IsDead = true;
            //Death animation?

            //Trigger Death screen with respawn button *if player*

        }
        
    }
}