using UnityEngine;

/// <summary> 
/// Enables the Player Controller based on ownership.
/// </summary> 

namespace Player
{
	public class PlayerStartup : MonoBehaviour 
	{
		public PlayerState state;
		public PlayerController controller;
		public HUDRelay playerHUD;

		void Awake () 
		{
			bool isMine = state.IsMine;
			controller.enabled = isMine;

			if (isMine)
				playerHUD.Initialize();
		}
	}
}