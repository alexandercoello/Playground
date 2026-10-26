using UnityEngine;
using Scripts.Objects.Base;

namespace Scripts.Character.Squire
{
    public class SquireInteractionManager : MonoBehaviour
    {
        [SerializeField] private SquireNavigationManager squireNavigationManager;
        [SerializeField] private float InteractRange = 1.5f;
        RaycastHit? targetInteractable;
        bool isNavigatingToInteractable = false;


        // Update is called once per frame
        void Update()
        {
            if(targetInteractable != null)
            {
                if(!isNavigatingToInteractable)
                {
                    NavigateToInteractable();
                }

                if(InteractableRangeCheck())
                {
                    Interact();                    
                }
            }
        }

        public void InteractablePinged(RaycastHit pingedObject)
        {
            if(targetInteractable != null)
                Reset();

            targetInteractable = pingedObject;            
        }

        //Check if the Interactable is in range of the Squire, if it is then return true, else return false
        bool InteractableRangeCheck()
        {
            if(targetInteractable != null)
            {
                float distanceToInteractable = Vector3.Distance(transform.position, targetInteractable.Value.transform.position);
                if(distanceToInteractable <= InteractRange)
                {
                    return true;
                }
            }

            return false;
        }

        void NavigateToInteractable()
        {
            if(targetInteractable != null)
            {
                squireNavigationManager.NavigateNearObject(targetInteractable.Value.transform, 10f);
                isNavigatingToInteractable = true;
            }
        }

        void Interact()
        {
            GameObject gameObject = targetInteractable.Value.transform.gameObject;
            Interactable interactable = gameObject.GetComponent<Interactable>(); 
            interactable.OnInteract();
            Reset();
        }

        void Reset()
        {
            targetInteractable = null;
            isNavigatingToInteractable = false;
        }
    }
}