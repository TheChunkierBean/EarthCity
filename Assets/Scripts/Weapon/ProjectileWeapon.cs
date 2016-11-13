using UnityEngine;

public class ProjectileWeapon : Weapon 
{
	public Explosive projectile;
	public Transform projectileSpawn;

	public float projectileVelocity = 20.0F;

	// Shoot projectile
	public override void Fire ()
	{
		RaycastHit hit = base.CastRay();
		Vector3 diff = base.cam.transform.forward;

		if (hit.collider)
			diff = (hit.point - projectileSpawn.position).normalized;

		Quaternion rotation = Quaternion.FromToRotation(projectile.transform.forward, diff);

		GameObject pro = Instantiate(projectile.gameObject, projectileSpawn.position, rotation) as GameObject;

		pro.GetComponent<Explosive>().Move(projectileVelocity);

		BroadcastWeaponFired(this);

		// If the camera is closer to a surface than the bullet spawn point, this indicates that we are standing too close to a wall
		// The projectile is very likely to go through the geometry. Instead we blow it up!
		if (Vector3.Distance(base.cam.transform.position, hit.point) < Vector3.Distance(projectileSpawn.position, hit.point))
		{
			Debug.LogError("Too close to surface");
			pro.GetComponent<Explosive>().ApplyDamage(Mathf.Infinity);
		}
	}
}
