using System;
using UnityEngine;
using Bolt;

//<summary> 
// A collection and centralized controller of all the components of a player.
// Only basic logic and redistribution of inputs and references to other components.
//</summary> 

namespace Player
{	
	public class PlayerController : EntityBehaviour<INewPlayerState>  
	{
		PlayerMovement Movement {get; set;}
		PlayerInventory Inventory {get; set;}
		PlayerDamageController DamageController {get; set;}
		PlayerAnimations Animation {get; set;}
		PlayerVehicleController VehicleController {get; set;}
		PlayerState State {get; set;}
		PlayerCamera Look {get; set;}
		HUDRelay Relay {get; set;}
		PlayerInput.Input Input { get { return PlayerInput.GetInput(); } }
		public Camera PlayerCamera;

		public override void Attached ()
		{
			if (State.IsMine)
			{
				Relay.Initialize();
				PlayerCamera.enabled = true;
			}

			//state.ChangeTransforms(state.Transform, transform);
			state.SetTransforms(state.Transform, transform);
			/*if (State.IsMine)
			{
				
			}*/
		}

		private void Awake ()
		{
			GetReference();
		}

		private void Update ()
		{
			if (State.IsAlive)
				UpdateState();
		}

		private void UpdateState ()
		{
			DamageController.Regenerate();

			VehicleController.UpdateVehicleState(Input);

			if (State.InVehicle)
				return;
			
			Movement.UpdateMovementState(State, Input);
			Inventory.UpdateWeaponState(Input);
			Look.UpdateLookState(Input);
		}

		// The weapon was fired by the player
		private void ReceiveWeaponFired (Weapon w)
		{
			Debug.Log("ReceiveWeaponFired");
			HUDRelay.OnWeaponFired(w);
		}

		// The weapon was aimed by the player
		private void ReceivedWeaponAimed (Weapon w)
		{
			Debug.Log("ReceivedWeaponAimed");
			Look.OnWeaponAimed(this, w);
			HUDRelay.OnWeaponAimed(w);
		}

		// The weapon was reloaded by the player
		private void ReceivedWeaponReloaded (Weapon w)
		{	
			Debug.Log("ReceivedWeaponReloaded");
		}

		// The player meleed
		private void ReceivedWeaponMeleed (Weapon w)
		{
			Debug.Log("ReceivedWeaponMeleed");
		}

		// The weapon was changed by the player
		private void ReceivedWeaponChanged ()
		{
			Debug.Log("ReceivedWeaponChanged");
			HUDRelay.OnWeaponChanged(Inventory);
		}

		// The player equipped a new weapon 
		private void ReceivedWeaponEquipped ()
		{
			Debug.Log("ReceivedWeaponEquipped");
			HUDRelay.OnWeaponChanged(Inventory);
		}

		// The player took damage
		private void ReceivedDamageTaken ()
		{
			HUDRelay.OnShieldChanged(DamageController.TestHealth);
		}

		private void ReceivedHealthRegenerated ()
		{
			HUDRelay.OnShieldChanged(DamageController.TestHealth);
		}

		// Acquires a reference to all components of the player
		private void GetReference ()
		{
			try 
			{
				Movement = GetComponent<PlayerMovement>();
				Inventory = GetComponent<PlayerInventory>();
				DamageController = GetComponent<PlayerDamageController>();
				Animation = GetComponent<PlayerAnimations>();
				VehicleController = GetComponent<PlayerVehicleController>();
				State = GetComponent<PlayerState>();
				Look = GetComponent<PlayerCamera>();
				Relay = GetComponent<HUDRelay>();
			}
			catch (NullReferenceException)
			{
				Debug.LogError("Not all references was found for the Player: " + name);
			}

			Subscribe();
		}

		// Subscribes to all events raised by the player
		private void Subscribe ()
		{
			foreach (Weapon weapon in Inventory._weapons)
			{
				weapon.BroadcastWeaponFired += ReceiveWeaponFired;
				weapon.BroadcastWeaponAimed += ReceivedWeaponAimed;
				weapon.BroadcastWeaponReloaded += ReceivedWeaponReloaded;
				weapon.BroadcastWeaponMeleed += ReceivedWeaponMeleed;
			}

			Inventory.BroadcastWeaponChanged += ReceivedWeaponChanged;
			Inventory.BroadcastWeaponEquipped += ReceivedWeaponEquipped;

			DamageController.BroadcastDamageTaken += ReceivedDamageTaken;
			DamageController.BroadcastHealthRegenerated += ReceivedHealthRegenerated;
		}
	}
}