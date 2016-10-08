using UnityEngine;

//<summary> 
// A collection and centralized controller of all the components of a player.
// Only basic logic and redistribution of inputs and references to other components.
//</summary> 

namespace Player
{
	public class PlayerController : MonoBehaviour 
	{
		PlayerMovement movement;
		PlayerWeapons weapons;
		PlayerDamageController damageController;
		PlayerAnimations animations;
		PlayerVehicleController vehicleController;
		PlayerState state;
		PlayerMouseLook mouseLook;

		private void Awake ()
		{
			GetReference();

			if (movement == null || weapons == null || damageController == null || animations == null || vehicleController == null)
			{
				Debug.LogError("Unassigned variable(s)");
				Debug.Break();
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
					movement.UpdateMovementState(state, input);
					weapons.UpdateWeaponState(input);
					mouseLook.UpdateLookState(input);
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
			HUD.OnWeaponFired(weapons.Primary);

			// Recoil System 
		}

		// The weapon was aimed by the player
		public void OnWeaponAimed ()
		{
			Debug.Log("OnWeaponAimed");

			HUD.OnWeaponAimed(weapons.Primary);

			// Change sensitivity 
		}

		// The weapon was reloaded by the player
		public void OnWeaponReloaded ()
		{
			Debug.Log("OnWeaponReloaded");
	
			animations.OnWeaponReloaded(weapons.Primary);
			HUD.OnWeaponReloaded(weapons.Primary);
		}

		// The weapon was changed by the player
		public void OnWeaponChanged ()
		{
			Debug.Log("OnWeaponChanged");

			animations.OnWeaponChanged(weapons.Primary);
			HUD.OnWeaponChanged(weapons.Primary, weapons.Secondary);
		}

		// The player meleed
		public void OnWeaponMelee ()
		{
			
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
			mouseLook = GetComponent<PlayerMouseLook>();	
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
			weapons.OnWeaponMeleeEvent += OnWeaponMelee;
		}
	}
}