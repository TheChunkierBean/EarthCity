using UnityEngine;

public abstract class Explosive : MonoBehaviour 
{
	public Transform explosion;
	public AudioClip explosionSound;
	public abstract void Explode (float timer);
	public abstract void Move (float force);

	private void Awake ()
	{
		//Explode(20);
	}

	protected void Explode ()
	{
		if (explosion)
			Instantiate(explosion, transform.position, Quaternion.identity);
		
		AudioSource.PlayClipAtPoint(explosionSound, transform.position);
		
		Destroy(gameObject);
	}
}
