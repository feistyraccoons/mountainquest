using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(Rigidbody2D))]
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
	public override void Update ()
	{
	}

	void OnDestroy ()
	{
		if (explode != null)
			aSource.PlayOneShot (explode);	
	}

	public void Shoot(Vector3 _velocity)
	{
		rigidbody2D.velocity = _velocity *  m_fSpeed ;
		if (create != null) {
			aSource.PlayOneShot (create);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
		other.gameObject.SendMessage ("takeDamage", this.m_fDamage);
		Destroy (gameObject);
	}


}
