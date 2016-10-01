using UnityEngine;
using System.Collections;

//<summary> 
// A collection and centralized controller of all the components of a player.
// Only basic logic and redistribution of inputs and references to other components.
//</summary> 

namespace Player
{
	public class PlayerController : MonoBehaviour 
	{
		private PlayerMovement movement;
		private PlayerWeapons weapons;
		private PlayerDamageController damageController;
		private PlayerAnimations animations;
		private PlayerVehicleController vehicleController;
		private PlayerState state;

		private void Awake ()
		{
			GetReference();

			if (movement == null || weapons == null || damageController == null || animations == null || vehicleController == null)
			{
				Debug.Break();
				Debug.LogError("Unassigned variable(s)");
			}
			else
			{
				Subscribe();
			}
		}

		private void Update ()
		{
			UpdateState();
		}


		// 
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

		// The weapon was fired by the player
		public void OnWeaponFired ()
		{
			Debug.Log("OnWeaponFired");

			animations.OnWeaponFired(weapons.Primary);
			//state.CurrentBattleStatus = PlayerState.BattleStatus.Firing;
			// Recoil System 

			// HUD . Reflect
		}

		// The weapon was aimed by the player
		public void OnWeaponAimed ()
		{
			Debug.Log("OnWeaponAimed");

			// Change sensitivity 
			// HUD . Reflect
		}

		// The weapon was reloaded by the player
		public void OnWeaponReloaded ()
		{
			Debug.Log("OnWeaponReloaded");
	
			animations.OnWeaponReloaded(weapons.Primary);

			// HUD . Reflect
		}

		// The weapon was changed by the player
		public void OnWeaponChanged ()
		{
			Debug.Log("OnWeaponChanged");

			animations.OnWeaponChanged(weapons.Primary);

			//HUD . Reflect
		}

		// Acquires a reference to all components of the player
		private void GetReference ()
		{
			movement = GetComponent<PlayerMovement>();
			weapons = GetComponent<PlayerWeapons>();
			damageController = GetComponent<PlayerDamageController>();
			animations = GetComponent<PlayerAnimations>();
			vehicleController = GetComponent<PlayerVehicleController>();
			state = GetComponent<PlayerState>();		
		}

		// Subscribes to all events raised by the player
		private void Subscribe ()
		{
			foreach (Weapon w in weapons.weapons)
			{
		        w.OnWeaponFiredEvent += OnWeaponFired;
        		w.OnWeaponAimedEvent += OnWeaponAimed;
        		w.OnWeaponReloadedEvent += OnWeaponReloaded;
			}

        	weapons.OnWeaponChangedEvent += OnWeaponChanged;
		}
	}
}