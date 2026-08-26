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
            
        [Header("Player Input Bools")]
        bool pingPressed;

        [Header("Player")]
        public Camera PlayerCamera;
        public float PingRange = 100f;

        
        
        RaycastHit pingedObject;
        public LayerMask groundLayer;
        public LayerMask interactableLayer;



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            
        }

        // Update is called once per frame
        void Update()
        {
            pingPressed = Input.GetKeyDown(PingKey);

            if (pingPressed)
            {
                PingTargetCheck(groundLayer);
            } 
            
        }

        void PingTargetCheck(LayerMask targetLayer)
        {
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            // Visual debug line in the Scene view
            Debug.DrawRay(ray.origin, ray.direction * PingRange, Color.red);

            if (Physics.Raycast(ray, out pingedObject, PingRange, targetLayer))
            {
                //Ping hit something in target Layer
                Debug.Log($"Pinged object: {pingedObject.collider.gameObject.name} at position: {pingedObject.point}");
                _eventmanager.Publish(new PlayerPingEvent(PingType.Location, pingedObject));
                return;
            }

        }

    }
}
