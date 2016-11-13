using UnityEngine;
using System.Collections.Generic;

//<summary> 
// 
//</summary> 

namespace Player
{
	public class PlayerInventory : MonoBehaviour
	{
		public List<Weapon> _weapons = new List<Weapon>();
		int _primary = 0;
		int _secondary = 1;

		public Weapon PrimaryWeapon
		{
			get { return _weapons[_primary]; }
			private set { _primary = _weapons.IndexOf(value); }
		} 

		public Weapon SecondaryWeapon
		{
			get { return _weapons[_secondary]; }
			private set { _secondary = _weapons.IndexOf(value); }
		}

		public delegate void PlayerWeaponsEvent ();
		public PlayerWeaponsEvent BroadcastWeaponChanged;
		public PlayerWeaponsEvent BroadcastWeaponEquipped;

		public void UpdateWeaponState (PlayerInput.Input input)
		{
			PrimaryWeapon.UpdateState();

			if (input.Shoot)
			{
				PrimaryWeapon.TryFire();
			}
			else
			{
				PrimaryWeapon._isTriggerReleased = true;
			}

			if (input.Aim)
			{
				PrimaryWeapon.Aim();
			}
			else if (input.Reload)
			{
				PrimaryWeapon.BeginReload();
			}
			else if (input.Melee)
			{
				//Melee();
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

			SecondaryWeapon.Dequipped();

			SecondaryWeapon.gameObject.SetActive(false);
			PrimaryWeapon.gameObject.SetActive(true);

			PrimaryWeapon.Equipped();

			BroadcastWeaponChanged();
		}

		private void EquipAmmo ()
		{

		}
	}
}

