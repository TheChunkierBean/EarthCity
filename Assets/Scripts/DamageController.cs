using UnityEngine;

public abstract class DamageController : MonoBehaviour 
{
	public float LastDamageTimeStamp;

	public virtual void ApplyDamage (float damage)
	{
		LastDamageTimeStamp = Time.time;
	}


	//public abstract void Regenerate ();
}
