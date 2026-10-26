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

		[SerializeField] private SquireNavigationManager squireNavigationManager;
		[SerializeField] private SquireInteractionManager squireInteractionManager;

		void Start()
		{
			// Initialization code here
			_eventmanager.Subscribe<PlayerPingEvent>(OnPlayerPing);
		}

		private void OnPlayerPing(PlayerPingEvent pingEvent)
		{
			switch (pingEvent.PingType)
			{
				case PingType.GoTo:
					squireNavigationManager.NavigateToRaycastHit(pingEvent.PingedObject);
					break;
				case PingType.Interactable:
					Debug.Log("SquireCommandEventManager received an Interactable ping.");
					squireInteractionManager.InteractablePinged(pingEvent.PingedObject);
					break;
				default:
					Debug.Log($"SquireCommandEventManager received an unhandled ping type: {pingEvent.PingType}");
					break;
			}
		}


	}
}