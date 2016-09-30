using UnityEngine;
using System.Collections;

//<summary> 
// 
//</summary> 

namespace Player
{
	public class PlayerWeapons : MonoBehaviour 
	{
		PlayerController controller;
		public Weapon DEBUGWeapon;

		public void Initialize (PlayerController pController)
		{
			controller = pController;
			DEBUGWeapon.Initialize(pController);
		}

		public void UpdateWeaponState (PlayerInput.Input input)
		{
			if (input.Shoot)
			{
				DEBUGWeapon.Shoot();
				//Current weapon . shoot
			}
			else if (input.Aim)
			{
				DEBUGWeapon.Aim();
				//Current weapon . aim
			}
			else if (input.Shift)
			{
				ChangeWeapons();
			}
		}

		private void ChangeWeapons ()
		{
			controller.OnWeaponChanged(DEBUGWeapon);
		}
	}
}

