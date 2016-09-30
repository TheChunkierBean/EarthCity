using UnityEngine;
using System.Collections;

//<summary> 
// 
//</summary> 

namespace Player 
{
	public class PlayerAnimations : MonoBehaviour 
	{
		PlayerController controller;

		public void Initialize (PlayerController pController)
		{
			controller = pController;
		}

		public void OnWeaponFired (Weapon weapon)
		{
			// Player weapon animations
			// Player player animations
		}

		public void OnWeaponReloaded (Weapon weapon)
		{
			// Player weapon animations
			// Player player animations
		}

		public void OnWeaponChanged (Weapon weapon)
		{
			// Player weapon animations
			// Player player animations
		}

		public void UpdateAnimationState ()
		{
			
		}
	}
}