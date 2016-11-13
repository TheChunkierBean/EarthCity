using UnityEngine;

namespace Player
{
	public class PlayerDamageController : DamageController 
	{	
		public float TestHealth = 100.0F;
		public float regenDelay = 3.5F;
		public float regenTime = 2.5F;

		public delegate void TestContract ();
		public TestContract BroadcastDamageTaken;
		public TestContract BroadcastHealthRegenerated;

		public override void ApplyDamage (float damage)
		{			
			base.ApplyDamage(damage);

			TestHealth -= damage;
			TestHealth = Mathf.Clamp(TestHealth, 0, 100);

			BroadcastDamageTaken();
		}

		public void Regenerate ()
		{
			if (Time.time >= base.LastDamageTimeStamp + regenDelay)
			{
				TestHealth += 100 / regenTime * Time.deltaTime;
				TestHealth = Mathf.Clamp(TestHealth, 0, 100);
				BroadcastHealthRegenerated();
			}
		}
	}
}

