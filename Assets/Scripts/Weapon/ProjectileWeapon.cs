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
	}
}
