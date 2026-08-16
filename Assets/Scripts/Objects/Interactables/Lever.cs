using UnityEngine;
using Scripts.Objects.Base;
using System.Collections;

namespace Scripts.Objects.Interactables
{
    /// <summary>
    /// The Lever will activate a DynamicSceneObject or deactivate it based on the Lever's state
    /// </summary>
    public class Lever : Interactable
    {
        Animator animator;

        //Used to prevent the player from flipping the lever on and off too quickly
        public float LeverLockoutTimer = 1f;



        void Start()
        {
            IsInteractable = true;

            animator = GetComponent<Animator>();
        }

        void Update()
        {
            
        }

        void LockLever()
        {
            IsInteractable = false;
        }

        void ResetLever()
        {
            IsInteractable = true;                        
        }

        void UpdateLeverState()
        {
            IsActive = !IsActive;

        }

        public override void OnInteract()
        {
            if (IsInteractable)
            {                        
                LockLever();

                ActivateDynamicSceneObjects();
            
                StartCoroutine(StartLeverLockoutTimer());
            }
        }

        public override void OnIsActiveChanged()
        {
            animator.SetBool("IsActive", IsActive);
        }

        void ActivateDynamicSceneObjects()
        {
            UpdateLeverState();

            foreach(GameObject gameObject in LinkedGameObjects)
            {
                DynamicSceneObject dynamicSceneObject = gameObject.GetComponent<DynamicSceneObject>(); 

                dynamicSceneObject.OnActivate();
            }
        }

        IEnumerator StartLeverLockoutTimer()
        {
            yield return new WaitForSeconds(LeverLockoutTimer);

            ResetLever();
        }
    }
}