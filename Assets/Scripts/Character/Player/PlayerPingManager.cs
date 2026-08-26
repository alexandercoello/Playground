using UnityEngine;
using Scripts.Event;

namespace Scripts.Character.Player
{
    /// <summary>
    /// Manages the player's pinging actions and notifies listeners when a ping occurs.
    /// </summary>
    public class PlayerPingManager : MonoBehaviour
    {

        private readonly EventManager _events;
        public PlayerPingManager(EventManager events)
        {
            _events = events;
        }


        [Header("Player Input Keys")]
        public KeyCode PingKey = KeyCode.Mouse3; 
            
        [Header("Player Input Bools")]
        bool pingPressed;



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            
        }

        // Update is called once per frame
        void Update()
        {
            pingPressed = Input.GetKeyDown(PingKey);

            //If the ping key is pressed bring up the radial menu and allow the player to select a command for the squire to execute
            if (pingPressed)
            {
                //Debug.Log("Ping key pressed");
            } 
            
        }
    }
}
