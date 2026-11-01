using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Scripts.Objects.Base
{

    /// <summary>
    /// Base Class Object that a player is able to Intertact with using their Interact button.
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        public bool IsInteractable;
        [SerializeField] private bool ResetsAfterLockout;
        [SerializeField] private float InteractableLockoutTimer = 1f;
        private bool isActive;                
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                OnIsActiveChanged();
            }
        }

        public abstract void OnIsActiveChanged();

        public void OnInteract()
        {
            if (IsInteractable)
            {                        
                LockInteractable();

                UpdateInteractableState();
            
                StartCoroutine(StartInteractableLockoutTimer());
            }
        }

        public void UpdateInteractableState()
        {
            IsActive = !IsActive;
        }

        public void LockInteractable()
        {
            IsInteractable = false;
        }

        public void ResetInteractable()
        {
            IsInteractable = true;

            if(ResetsAfterLockout)
            {
                IsActive = false;
            }
        }

        public IEnumerator StartInteractableLockoutTimer()
        {
            yield return new WaitForSeconds(InteractableLockoutTimer);

            ResetInteractable();
        }
    }
}