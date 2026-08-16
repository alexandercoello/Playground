using UnityEngine;
using Scripts.Objects.Base;

namespace Scripts.Character.Player
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        [Header("Player")]
        public Camera PlayerCamera;
        public float InteractRange = 5f;


        [Header("Player Input Keys")]
        public KeyCode InteractionKey = KeyCode.E;


        [Header("Player Input Bools")]
        bool interactPressed;


        bool interactableInRange;
        public LayerMask interactableLayer;
        RaycastHit focusedInteractable;


        // Update is called once per frame
        void Update()
        {
            GetPlayerInput();

            //Check for interatable in front of player
            InteractableCheck();

            //Check if interactPressed 
            if(interactPressed && interactableInRange)
            {
                Interact();
            }
        }

        void GetPlayerInput()
        {
            interactPressed = Input.GetKeyDown(InteractionKey);
        }

        void InteractableCheck()
        {
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            // Visual debug line in the Scene view
            //Debug.DrawRay(ray.origin, ray.direction * InteractRange, Color.red);

            if (Physics.Raycast(ray, out focusedInteractable, InteractRange, interactableLayer))
            {
                interactableInRange = true;
                return;
            }

            interactableInRange = false;
        }

        void Interact()
        {
            GameObject gameObject = focusedInteractable.transform.gameObject;

            Interactable interactable = gameObject.GetComponent<Interactable>(); 

            interactable.OnInteract();
        }
    }
}