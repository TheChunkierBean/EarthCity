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
			else
			{

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

				if (state.InVehicle)
				{
					vehicleController.UpdateVehicleState(input);
				}
				else if (!state.InVehicle)
				{
					weapons.UpdateWeaponState(input);
				}
			}
			else
			{
				// Control death camera??

			}
		}

		public void OnWeaponFired (Weapon weapon)
		{
			animations.OnWeaponFired(weapon);
			state.CurrentBattleStatus = PlayerState.BattleStatus.Firing;

			// HUD . Reflect
		}

		public void OnWeaponAimed (Weapon weapon)
		{
			// Change sensitivity 
			// HUD . Reflect
		}

		public void OnWaponReloaded (Weapon weapon)
		{
			animations.OnWeaponReloaded(weapon);

			// HUD . Reflect
		}

		public void OnWeaponChanged (Weapon weapon)
		{
			animations.OnWeaponChanged(weapon);

			//HUD . Reflect
		}
	}
}