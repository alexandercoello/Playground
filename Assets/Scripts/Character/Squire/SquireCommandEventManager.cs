using Scripts.Event;
using Scripts.Event.Events;
using UnityEngine;
using Zenject;
using UnityEngine.AI;

namespace Scripts.Character.Squire
{
	/// <summary>
	/// Receives commands from the player
	/// </summary>
	public class SquireCommandEventManager : MonoBehaviour
	{
        private IEventManager _eventmanager;	

		[Inject]
		public void Construct(IEventManager eventManager)
		{
			_eventmanager = eventManager;
		}

		public SquireNavigationManager squireNavigationManager;

		void Start()
		{
			// Initialization code here
			_eventmanager.Subscribe<PlayerPingEvent>(OnPlayerPing);
		}

		private void OnPlayerPing(PlayerPingEvent pingEvent)
		{
			Debug.Log($"SquireCommandEventManager received PlayerPingEvent with PingType: {pingEvent.PingType}");
            
			squireNavigationManager.NavigateToRaycastHit(pingEvent.PingedObject);
		}
	}
}