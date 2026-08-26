using Zenject;
using UnityEngine;
using Scripts.Event;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IEventManager>().To<EventManager>().AsSingle();
    }
}