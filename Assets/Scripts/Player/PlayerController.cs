using UnityEngine;

//<summary> 
// A collection and centralized controller of all the components of a player.
// Only basic logic and redistribution of inputs and references to other components.
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
	[RequireComponent(typeof(HUDRelay))]
	
	public class PlayerController : MonoBehaviour 
	{
		PlayerMovement movement;
		PlayerWeapons weapons;
		PlayerDamageController damageController;
		PlayerAnimations animations;
		PlayerVehicleController vehicleController;
		PlayerState state;
		PlayerMouseLook mouseLook;
		public Camera playerCamera;

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

				if (state.IsMine)
				{
					// Don't show graphics
				}
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
		private void OnWeaponFired ()
		{
			animations.OnWeaponFired(weapons.Primary);
			HUDRelay.OnWeaponFired(weapons.Primary);

			// Recoil System 
		}

		// The weapon was aimed by the player
		private void OnWeaponAimed ()
		{
			float FOVRatio = weapons.Primary.ScopeToFOVRatio;
			float currentFieldOfView = 90.0F / 100.0F * FOVRatio;			// 90 is the default FOV. This should be changeable!
			
			playerCamera.fieldOfView = currentFieldOfView;
			mouseLook.OnWeaponAimed(FOVRatio);

			HUDRelay.OnWeaponAimed(weapons.Primary);
		}

		// The weapon was reloaded by the player
		private void OnWeaponReloaded ()
		{	
			animations.OnWeaponReloaded(weapons.Primary);
			HUDRelay.OnWeaponReloaded(weapons.Primary);
		}

		// The weapon was changed by the player
		private void OnWeaponChanged ()
		{
			animations.OnWeaponChanged(weapons.Primary);
			HUDRelay.OnWeaponChanged(weapons.Primary, weapons.Secondary);
		}

		// The player meleed
		private void OnWeaponMelee ()
		{
			
		}

		private void OnPlayerDamaged ()
		{
			HUDRelay.OnShieldChanged(damageController.testHealth);
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

			damageController.OnPlayerDamagedEvent += OnPlayerDamaged;
		}
	}
}