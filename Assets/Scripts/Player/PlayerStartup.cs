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

	public class PlayerStartup : MonoBehaviour 
	{
		public PlayerState state;

		public PlayerAnimations animations;
		public PlayerController controller;
		public PlayerDamageController damageController;
		public PlayerMovement movement;
		public PlayerVehicleController vehicleController;
		public PlayerWeapons weapons;

		void Start () 
		{
			bool isMine = state.IsMine;

			animations.enabled = isMine;
			controller.enabled = isMine;
			damageController.enabled = isMine;
			movement.enabled = isMine;
			vehicleController.enabled = isMine;
			weapons.enabled = isMine;

			damageController.Initialize(controller);
			damageController.Initialize(controller);
			vehicleController.Initialize(controller);
		}
	}
}