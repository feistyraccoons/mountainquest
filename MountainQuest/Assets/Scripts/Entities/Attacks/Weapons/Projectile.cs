using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Projectile : Weapon
{

	public AudioClip explode, create;
	private AudioSource aSource;
	// Use this for initialization
	void Start ()
	{
		aSource = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnDestroy ()
	{
		if (explode != null)
			aSource.PlayOneShot (explode);	
	}

	public void Shoot(Vector2 _velocity)
	{
		rigidbody2D.velocity = _velocity * m_fSpeed * Time.deltaTime;
				if (create != null) {
			aSource.PlayOneShot (create);
		}
	}

}
