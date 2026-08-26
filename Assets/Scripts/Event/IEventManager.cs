using System;


namespace Scripts.Event
{
    public interface IEventManager
    {
        public void Subscribe<T>(Action<T> handler) where T : IGameEvent;

        public void Unsubscribe<T>(Action<T> handler) where T : IGameEvent;

        public void Publish<T>(T gameEvent) where T : IGameEvent;
    }
}
