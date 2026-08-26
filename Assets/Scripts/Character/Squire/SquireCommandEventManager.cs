using System;

namespace Scripts.Character.Squire
{
	/// <summary>
	/// Receives commands from the player and notifies interested listeners.
	/// </summary>
	public static class SquireCommandEventManager
	{
		/// <summary>
		/// C# event for scripts that need to react to player commands.
		/// </summary>
		public static event Action<string> CommandReceived;

		/// <summary>
		/// Sends a command received from the player to all registered listeners.
		/// </summary>
		public static void ReceiveCommand(string command)
		{
			if (string.IsNullOrWhiteSpace(command))
			{
				return;
			}

			command = command.Trim();
			CommandReceived?.Invoke(command);
		}
	}
}