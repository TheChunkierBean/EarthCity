using UnityEngine;

namespace Player
{
	public class PlayerDamageController : DamageController 
	{	
		public float testHealth = 100.0F;
		public override void ApplyDamage (float damage)
		{
			Debug.Log("Player received damage " + damage);
			testHealth -= damage;

			HUD.OnShieldChanged(testHealth);
		}
	}
}

