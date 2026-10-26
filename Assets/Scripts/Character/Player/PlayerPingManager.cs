using System;
using UnityEngine;
using Scripts.Event;
using Zenject;
using Scripts.Event.Events;
using UnityEngine.AI;

namespace Scripts.Character.Player
{
    /// <summary>
    /// Manages the player's pinging actions and notifies listeners when a ping occurs.
    /// </summary>
    public class PlayerPingManager : MonoBehaviour
    {

        private IEventManager _eventmanager;

        [Inject]
        public void Construct(IEventManager eventmanager)
        {
            _eventmanager = eventmanager;
        }


        [Header("Player Input Keys")]
        public KeyCode PingKey = KeyCode.Mouse3; 
        public float LongPressDuration = 0.5f;
        private bool IsLongPress;
        private float pingPressedAtTime;

        [Header("Player")]
        [SerializeField] private Camera PlayerCamera;
        [SerializeField] private CameraControls cameraControls;
        public float PingRange = 100f;

        
        [Header("Environment Layers")]
        RaycastHit pingedObject;
        [SerializeField] public LayerMask groundLayer;
        [SerializeField] public LayerMask interactableLayer;

        [Header("UI")]
        public Canvas commandRadialMenu;



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckForPingInput();
        }

        void CheckForPingInput()
        {
            if (Input.GetKeyDown(PingKey))
            {
                pingPressedAtTime = Time.time;
            }

            if(Input.GetKey(PingKey))
            {
                if (Time.time - pingPressedAtTime >= LongPressDuration)
                {
                    IsLongPress = true;
                    OpenPingRadialMenu();
                }
            }

            if(Input.GetKeyUp(PingKey))
            {
                if (IsLongPress)
                {
                    ClosePingRadialMenu();
                }
                else
                {
                    if(PingTargetLayer(interactableLayer, PingType.Interactable))
                    {
                        //Debug.Log("Pinged an interactable object.");
                    }
                    else if(PingTargetLayer(groundLayer, PingType.GoTo))
                    {
                        //Debug.Log("Pinged an ground object.");                
                    }
                }

                IsLongPress = false;
            }
        }

        void OpenPingRadialMenu()
        {
            if(commandRadialMenu.gameObject is not null)
            {
                commandRadialMenu.enabled = true;
                cameraControls.LockCameraAndShowCursor();
            }
        }

        void ClosePingRadialMenu()
        {
            if(commandRadialMenu.gameObject is not null)
            {
                commandRadialMenu.enabled = false;
                cameraControls.UnlockCameraAndHideCursor();
            }
        }

        bool PingTargetLayer(LayerMask targetLayer, PingType pingType)
        {
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            // Visual debug line in the Scene view
            //Debug.DrawRay(ray.origin, ray.direction * PingRange, Color.red);

            if (Physics.Raycast(ray, out pingedObject, PingRange, targetLayer))
            {
                //Ping hit something in target Layer
                //Debug.Log($"Pinged object: {pingedObject.collider.gameObject.name} at position: {pingedObject.point}");
                _eventmanager.Publish(new PlayerPingEvent(pingType, pingedObject));
                return true;
            }

            return false;
        }

    }


}
