using UnityEngine;

public class TestExplosive : Explosive 
{
	public override void Explode (float timer)
	{
		base.Explode();
	}

	public override void Move (float force)
	{
		GetComponent<Rigidbody>().AddForce(force * transform.forward);
	}

	private void OnTriggerEnter ()
	{
		Explode(0);
	}
}
