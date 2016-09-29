using UnityEngine;
using System.Collections;

//<summary> 
// 
//</summary> 

namespace Player
{
	public class PlayerWeapons : MonoBehaviour 
	{
		public void UpdateWeaponState (PlayerInput.Input input)
		{
			if (input.Shoot)
			{
				//Current weapon . shoot
			}
			if (input.Aim)
			{
				//Current weapon . aim
			}
		}
	}
}

