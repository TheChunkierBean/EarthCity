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
			set { _primary = weapons.IndexOf(value); }
		} 

		public Weapon Secondary
		{
			get { return weapons[_secondary]; }
			set { _secondary = weapons.IndexOf(value); }
		} 

		#region Events

			public delegate void PlayerWeaponsEvent ();
	    	public PlayerWeaponsEvent OnWeaponChangedEvent;
	    	public PlayerWeaponsEvent OnAmmoEquippedEvent;
			public PlayerWeaponsEvent OnWeaponMeleeEvent;

    	#endregion Events

		public void UpdateWeaponState (PlayerInput.Input input)
		{
			if (input.Shoot)
			{
				Primary.TryFire();
			}
			else if (input.Aim)
			{
				Primary.Aim();
			}
			else if (input.Reload)
			{
				Primary.TryReload();
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
			OnWeaponChangedEvent();

			int p = _primary;
			_primary = _secondary;
			_secondary = p;

			Secondary.gameObject.SetActive(false);
			Primary.gameObject.SetActive(true);

			Secondary.Dequipped();
			Primary.Equipped();
		}

		private void Melee ()
		{
			OnWeaponMeleeEvent();
		}

		private void EquipAmmo ()
		{
			OnAmmoEquippedEvent();
		}
	}
}

