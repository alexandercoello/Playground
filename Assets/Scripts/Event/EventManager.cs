using System;
using UnityEngine;
using System.Collections.Generic;


namespace Scripts.Event
{
    /// <summary>
    /// A simple event manager for handling game events.
    /// </summary>
    public class EventManager : MonoBehaviour, IEventManager
    {
        private readonly Dictionary<Type, Delegate> _handlers = new();
        
        public void Subscribe<T>(Action<T> handler) where T : IGameEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            if (_handlers.TryGetValue(typeof(T), out var existing))
                _handlers[typeof(T)] = Delegate.Combine(existing, handler);
            else
                _handlers[typeof(T)] = handler;
        }

        public void Unsubscribe<T>(Action<T> handler) where T : IGameEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            if (!_handlers.TryGetValue(typeof(T), out var existing)) return;

            var updated = Delegate.Remove(existing, handler);
            if (updated == null)
                _handlers.Remove(typeof(T));
            else
                _handlers[typeof(T)] = updated;
        }

        public void Publish<T>(T gameEvent) where T : IGameEvent
        {
            if (_handlers.TryGetValue(typeof(T), out var existing))
                ((Action<T>)existing).Invoke(gameEvent);
        }
    }
}
