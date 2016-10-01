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

		private int _primary;
		private int _secondary;

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

    	#endregion Events

		public void UpdateWeaponState (PlayerInput.Input input)
		{
			if (input.Shoot)
			{
				Primary.Fire();
			}
			else if (input.Aim)
			{
				Primary.Aim();
			}
			else if (input.Reload)
			{
				Primary.Reload();
			}
			else if (input.Melee)
			{
				Melee();
			}
			else if (input.Shift)
			{
				ChangeWeapons();
			}
		}

		private void ChangeWeapons ()
		{
			OnWeaponChangedEvent();
		}

		private void Melee ()
		{
			
		}

		private void EquipAmmo ()
		{
			OnAmmoEquippedEvent();
		}
	}
}

