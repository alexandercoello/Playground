using Scripts.PlayerUI;
using UnityEngine;

namespace Scripts.Character
{
    public abstract class BaseHealthManager : MonoBehaviour
    {
        [Header("Health")]
        public int MaxHealth;
        public int CurrentHealth;
        public int HealthRegen;


        [Header("Statuses")]
        protected bool IsBurning;
        protected bool IsPoisoned;
        protected bool IsStunned;
        protected bool IsDead;


        protected virtual void Start()
        {
            CurrentHealth = MaxHealth;
            CheckAndUpdateHealthUI();
        }

        private void Update()
        {
            //Check for statuses (burn) that may update health using a timer checked on update
            //Can I apply burning for a set time and trigger a health update on an interval?

            //Regen stamina and health using a timer checked on update?
        }

        protected abstract void CheckAndUpdateHealthUI();
        protected abstract void TriggerDeath();

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Bullet"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                DecreaseHealth(bullet.Damage);
            } 
        }

        public void DecreaseHealth(int healthConsumed)
        {
            if(healthConsumed >= CurrentHealth)
            {
                CurrentHealth = 0;
                TriggerDeath();
            }
            else
            {
                CurrentHealth -= healthConsumed;
            }

            CheckAndUpdateHealthUI();
        }

        public void IncreaseHealth(int healthGained)
        {
            if(healthGained + CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            else
            {
                CurrentHealth += healthGained;
            }

            CheckAndUpdateHealthUI();
        }



    }
}