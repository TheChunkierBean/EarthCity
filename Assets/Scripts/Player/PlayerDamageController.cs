namespace Player
{
	public class PlayerDamageController : DamageController 
	{	
		public float testHealth = 100.0F;

		public delegate void DamageEvent ();
        public DamageEvent OnPlayerDamagedEvent;

		public override void ApplyDamage (float damage)
		{			
			// Do this from PlayerCOntroller instead using event?
			testHealth -= damage;

			OnPlayerDamagedEvent();
		}
	}
}

