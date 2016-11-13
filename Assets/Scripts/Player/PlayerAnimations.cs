using UnityEngine;

//<summary> 
// 
//</summary> 

namespace Player 
{
	public class PlayerAnimations : MonoBehaviour 
	{
		public void OnWeaponFired (PlayerController weapon)
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

		public void Subscribe ()
		{
			PlayerController PC = GetComponent<PlayerController>();
			//PC.WeaponFiredEvent + OnWeaponFired;


		}
	}
}