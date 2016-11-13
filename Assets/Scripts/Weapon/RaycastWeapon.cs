using UnityEngine;

public class RaycastWeapon : Weapon 
{
	[System.Serializable]
	public struct Damage
	{
		public bool isPrecision;
		public float baseDamage;
		public float additivePrecisionDamage;
	}

	[System.Serializable]
	public struct SurfaceDecal
	{
		public Transform DEBUGDecal;
	}

	public Damage damage;

	public SurfaceDecal decals;

	public float projectileForce = 5.0F; 

	public override void Fire ()
	{
		RaycastHit hit = base.CastRay();

		if (hit.collider == null)
			return;

		Destroy(Instantiate(decals.DEBUGDecal, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject, 2);

		DamageController dControl = hit.collider.GetComponent<DamageController>();
		Rigidbody body = hit.collider.GetComponent<Rigidbody>();

		if (body != null)
		{
			body.AddForceAtPosition(transform.forward * projectileForce, hit.point, ForceMode.Acceleration);
		}

		if (dControl == null)
			return;

		// TODO: We should only apply precision damage if we hit a "precision" spot, like a head
		float totalDamage = damage.baseDamage + (damage.isPrecision? damage.additivePrecisionDamage : 0);

		dControl.ApplyDamage(totalDamage);

		BroadcastWeaponFired(this);
	}
}