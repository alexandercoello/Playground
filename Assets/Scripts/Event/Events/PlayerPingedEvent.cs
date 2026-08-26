using System;

namespace Scripts.Event.Events
{
    /// <summary>
    /// An event that occurs when the player pings something.
    /// </summary>
    public class PlayerPingedEvent : IGameEvent
    {
            public PingType PingType { get; }

            public PlayerPingedEvent(PingType pingType)
            {
                PingType = pingType;
            }

    }

    public enum PingType
    {
        None,
        Enemy,
        Ally,
        Item,
        Location
    }
}