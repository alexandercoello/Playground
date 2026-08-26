using System;
using Scripts.Event;
using Scripts.Event.Events;
using UnityEngine;
using Zenject;

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

		void Start()
		{
			// Initialization code here
			_eventmanager.Subscribe<PlayerPingedEvent>(OnPlayerPinged);
		}

		private void OnPlayerPinged(PlayerPingedEvent pingEvent)
		{
			Debug.Log($"SquireCommandEventManager received PlayerPingedEvent with PingType: {pingEvent.PingType}");



		}
	}
}