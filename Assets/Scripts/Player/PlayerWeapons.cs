using UnityEngine;
using System.Collections.Generic;

//<summary> 
// 
//</summary> 

namespace Player
{
	public class PlayerWeapons : MonoBehaviour 
	{
		public List<Weapon> weapons = new List<Weapon>();
		int _primary = 0;
		int _secondary = 1;

		public Weapon Primary
		{
			get { return weapons[_primary]; }
			private set { _primary = weapons.IndexOf(value); }
		} 

		public Weapon Secondary
		{
			get { return weapons[_secondary]; }
			private set { _secondary = weapons.IndexOf(value); }
		} 

		#region Events

			public delegate void PlayerWeaponsEvent ();
	    	public PlayerWeaponsEvent OnWeaponChangedEvent;
	    	public PlayerWeaponsEvent OnAmmoEquippedEvent;
			public PlayerWeaponsEvent OnWeaponMeleeEvent;

    	#endregion Events

		public void UpdateWeaponState (PlayerInput.Input input)
		{
			Primary.UpdateState();

			if (input.Shoot)
			{
				Primary.TryFire();
			}
			else
			{
				Primary._isTriggerReleased = true;
			}

			if (input.Aim)
			{
				Primary.Aim();
			}
			else if (input.Reload)
			{
				Primary.BeginReload();
			}
			else if (input.Melee)
			{
				Melee();
			}
			
			if (input.Shift)
			{
				ChangeWeapons();
			}
		}

		private void ChangeWeapons ()
		{
			int p = _primary;
			_primary = _secondary;
			_secondary = p;

			Secondary.Dequipped();

			Secondary.gameObject.SetActive(false);
			Primary.gameObject.SetActive(true);

			Primary.Equipped();

			OnWeaponChangedEvent();
		}

		private void Melee ()
		{
			if (Primary is MeleeWeapon)
			{
				Primary.Fire();
			}

			OnWeaponMeleeEvent();
		}

		private void EquipAmmo ()
		{
			OnAmmoEquippedEvent();
		}
	}
}

