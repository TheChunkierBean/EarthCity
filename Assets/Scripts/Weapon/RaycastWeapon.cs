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

	public override void Fire ()
	{
		RaycastHit hit = base.CastRay();

		if (hit.collider == null)
			return;

		Destroy(Instantiate(decals.DEBUGDecal, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject, 2);

		DamageController dControl = GetComponent<DamageController>();

		if (dControl == null)
			return;

		// TODO: We should only apply precision damage if we hit a "precision" spot, like a head
		float totalDamage = damage.baseDamage + (damage.isPrecision? damage.additivePrecisionDamage : 0);

		dControl.ApplyDamage(totalDamage);
	}
}