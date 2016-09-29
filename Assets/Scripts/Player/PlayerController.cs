using UnityEngine;
using System.Collections;

//<summary> 
// 
//</summary> 

namespace Player
{
	public class PlayerController : MonoBehaviour 
	{
		public PlayerMovement movement;
		public PlayerWeapons weapons;
		public PlayerDamageController damageController;
		public PlayerAnimations animations;
		public PlayerVehicleController vehicleController;
		public PlayerState state;

		private void Awake ()
		{
			if (movement == null || weapons == null || damageController == null || animations == null || vehicleController == null)
			{
				Debug.Break();
				Debug.LogError("Unassigned variable(s)");
			}
		}

		private void Update ()
		{
			UpdateState();
		}

		private void UpdateState ()
		{
			if (state.IsAlive)
			{
				PlayerInput.Input input = PlayerInput.GetInput();

				movement.UpdateMovementState(state, input);
				weapons.UpdateWeaponState(input);
				animations.UpdateAnimationState();
			}
		}
	}
}