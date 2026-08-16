using UnityEngine;
using Scripts.Objects.Base;
using System.Collections;

namespace Scripts.Objects.Interactables
{
    /// <summary>
    /// The button will keep a DynamicSceneObject active for a set amount of time and then deactivate it and that time has passed.
    /// </summary>
    public class Button : Interactable
    {

        Animator animator;
        //The amount of time that the button will stay active before deactivating and resetting
        public float ButtonActiveTime = 5f;
        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            IsInteractable = true;
            animator = GetComponent<Animator>();
            animator.Play("IdleButton", 0, 0f); 
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void LockButton()
        {
            IsInteractable = false;
            IsActive = true;
        }

        void ResetButton()
        {
            IsInteractable = true;
            IsActive = false;
            ActivateDynamicSceneObjects();                          
        }

        public override void OnInteract()
        {
            if (IsInteractable)
            {                        
                LockButton();

                ActivateDynamicSceneObjects();
            
                StartCoroutine(StartButtonLockoutTimer());
            }
        }

        public override void OnIsActiveChanged()
        {
            animator.SetBool("IsActive", IsActive);
        }

        void ActivateDynamicSceneObjects()
        {
            foreach(GameObject gameObject in LinkedGameObjects)
            {
                DynamicSceneObject dynamicSceneObject = gameObject.GetComponent<DynamicSceneObject>(); 

                dynamicSceneObject.OnActivate();
            }
        }

        IEnumerator StartButtonLockoutTimer()
        {
            yield return new WaitForSeconds(ButtonActiveTime);

            ResetButton();
        }
    }
}