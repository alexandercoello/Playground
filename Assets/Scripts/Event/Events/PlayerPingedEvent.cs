using System;
using UnityEngine;

namespace Scripts.Event.Events
{
    /// <summary>
    /// An event that occurs when the player pings something.
    /// </summary>
    public class PlayerPingEvent : IGameEvent
    {
            public PingType PingType { get; }
            public RaycastHit PingedObject { get; }

            public PlayerPingEvent(PingType pingType, RaycastHit pingedObject)
            {
                PingType = pingType;
                PingedObject = pingedObject;
            }

    }

    public enum PingType
    {
        None,
        Location,
        Interactable,
        Pickup,
        Enemy
    }
}