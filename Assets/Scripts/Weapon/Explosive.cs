using UnityEngine;

public class Explosive : MonoBehaviour 
{
	public float explosiveTimer = 20; 
	public float explosiveRadius = 10;
	public float explosiveDamage = 2000;
	public float explosiveForce = 2000;
	public Transform explosion;
	public AudioClip explosionSound;

	Rigidbody body;
	float _fuseTimer;
	RaycastHit _hit;

	private void Awake ()
	{
		body = GetComponent<Rigidbody>();

		_fuseTimer = Time.time;
	}

	private void Update ()
	{
		if (Time.time > _fuseTimer + explosiveTimer)
		{
			Explode();
		}

		if (CheckForForwardInterception())
		{
			Explode();
		}
	}

	public virtual void Move (float force)
	{
		body.AddForce(force * transform.forward);
	}

	protected virtual void Explode ()
	{
		if (explosion)
			Instantiate(explosion, transform.position, Quaternion.identity);
		
		AudioSource.PlayClipAtPoint(explosionSound, transform.position);

		CheckForDamageControllers();
		
		Destroy(gameObject);
	}

	protected bool CheckForForwardInterception ()
	{	
		// The distance traveled over 2 frames. Only calculating for 1 frame is not 
		// enough accuracy for high velocity objects.
		float forwardTrace = body.velocity.magnitude * (Time.deltaTime * 2);
		bool isIntercepted = false;

		Ray r = new Ray(transform.position, transform.forward);

		Debug.DrawRay(r.origin, r.direction * forwardTrace , Color.red);

		if (Physics.Raycast(r, out _hit, forwardTrace))
		{
			// We don't want to explode on triggers
			if (!_hit.collider.isTrigger)
			{
				isIntercepted = true;
			}
		}

		return isIntercepted;
	}

	protected void CheckForDamageControllers ()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRadius);

		foreach (Collider c in hitColliders)
		{
			RaycastHit r;

			if (Physics.Linecast(transform.position, c.transform.position, out r))
			{
				DamageController dControl = c.GetComponent<DamageController>();
				Rigidbody body = c.GetComponent<Rigidbody>();

				// Returns 0 - 1 based on the distance between 0 and the explosiveRadius
				float normDistance = Mathf.InverseLerp(0, explosiveRadius, r.distance);
				
				// Add explosive force
				if (c.GetComponent<Rigidbody>())
				{
					body.AddExplosionForce(explosiveForce, transform.position, explosiveRadius, 3.0F);
				}

				// Calculate damage
				if (dControl != null)
				{
					Debug.DrawLine(transform.position, r.point, Color.red, 1.5F);
					float damage = explosiveDamage - (explosiveDamage * normDistance);

					dControl.ApplyDamage(damage);
				}
			}
		}
	}

    void OnDrawGizmos () 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosiveRadius);
    }
}
