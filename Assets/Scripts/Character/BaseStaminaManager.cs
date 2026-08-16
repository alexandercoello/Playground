using System.Timers;
using Scripts.PlayerUI;
using UnityEngine;


namespace Scripts.Character
{
    public abstract class BaseStaminaManager : MonoBehaviour
    {
        [Header("Stamina")]
        public int Max = 100;
        public int Current = 100;
        public float RegenDelay = 2.5f;

        [Header("Regen")]
        //Amount of time before regen can begin
        public float regenBeginDelay = 2.5f;
        //Tracks the time passed since the last event to delay beginning regen
        public float regenCooldownTimer = 0f;
        //Amount regenerated each regen tick
        public int RegenRate = 10;



        protected virtual void Start()
        {
            Current = Max;
            CheckAndUpdateStaminaUI();        
        }

        void Update()
        {
            regenCooldownTimer += Time.deltaTime;
            if(regenCooldownTimer >= regenBeginDelay && Current < Max)
            {
                RegenStamina();
            }
        }

        protected abstract void CheckAndUpdateStaminaUI();

        public void DecreaseStamina(int staminaConsumed)
        {
            if(staminaConsumed >= Current)
            {
                Current = 0;
            }
            else
            {
                Current -= staminaConsumed;
            }

            CheckAndUpdateStaminaUI();           
            ResetRegenTimer();
        }
        
        public void IncreaseStamina(int staminaGained)
        {
            if(staminaGained + Current > Max)
            {
                Current = Max;
            }
            else
            {
                Current += staminaGained;
            }

            CheckAndUpdateStaminaUI();
        }

        private void RegenStamina()
        {
            IncreaseStamina(RegenRate);
            ResetRegenTimer();            
        }
        
        private void ResetRegenTimer()
        {
            regenCooldownTimer = 0f;
        }
    }
}