using UnityEngine;

//<summary> 
// 
//</summary> 

namespace Player
{
	[RequireComponent(typeof(PlayerAnimations))]
	[RequireComponent(typeof(PlayerDamageController))]
	[RequireComponent(typeof(PlayerMovement))]
	[RequireComponent(typeof(PlayerVehicleController))]
	[RequireComponent(typeof(PlayerWeapons))]
	[RequireComponent(typeof(PlayerController))]
	[RequireComponent(typeof(PlayerState))]
	[RequireComponent(typeof(PlayerMouseLook))]
	[RequireComponent(typeof(HUD))]

	public class PlayerStartup : MonoBehaviour 
	{
		public PlayerState state;
		public PlayerAnimations animations;
		public PlayerController controller;
		public PlayerDamageController damageController;
		public PlayerMovement movement;
		public PlayerVehicleController vehicleController;
		public PlayerWeapons weapons;
		public PlayerMouseLook mouseLook;
		public HUD playerHUD;

		void Awake () 
		{
			bool isMine = state.IsMine;

			animations.enabled = isMine;
			controller.enabled = isMine;
			damageController.enabled = isMine;
			movement.enabled = isMine;
			vehicleController.enabled = isMine;
			weapons.enabled = isMine;
			mouseLook.enabled = isMine;

			if (isMine)
				playerHUD.Initialize();

			vehicleController.Initialize(controller);
		}
	}
}