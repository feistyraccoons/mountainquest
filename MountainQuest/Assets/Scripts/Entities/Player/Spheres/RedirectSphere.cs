using UnityEngine;
using System.Collections;

public class RedirectSphere : MonoBehaviour {

	public float RotationDirection;
	public Vector3 Direction;
	public float DamageModifier = 1.5f;
	public float AliveTimer = 7;
	// Use this for initialization
	void Start () 
	{
//		Direction = dir;
//		RotationDirection = rdir;
	// Must be given a Direction on creation
	}

	// Update is called once per frame
	void Update () {
		AliveTimer -= Time.deltaTime;
		if (AliveTimer <= 0)
			Destroy (this.gameObject);

		if (AliveTimer<=2) {
			SpriteRenderer mySR = GetComponent<SpriteRenderer>();
			mySR.color = new Color(1,1,1,AliveTimer/2);
		}

	}


	void OnTriggerEnter2D(Collider2D other)
	{
			Projectile proj = other.GetComponent<Projectile> ();

		other.rigidbody2D.position = this.transform.position;
	//	Direction = new Vector3 (0,-1, 0);
		//RotationDirection = 180;
		//Direction.Normalize();
		
		Direction.Normalize ();
		Direction *= other.rigidbody2D.velocity.magnitude;
		other.rigidbody2D.velocity = Direction;
		//other.rigidbody2D.velocity = Direction;
		//if (other.rigidbody2D.ro)


		//other.transform.rotation.Set(other.transform.rotation.x, other.transform.rotation.y, other.transform.rotation.z + RotationDirection, other.transform.rotation.w);
		//other.transform.rotation.Set (0, 0, RotationDirection, 0);
		other.rigidbody2D.rotation = 0;
		other.rigidbody2D.rotation = RotationDirection;
		//other.transform.rotation = Quaternion.Euler (0, 0, RotationDirection);
		if (proj != null)
		{



		proj.m_fDamage *= DamageModifier;
		
		}
		
	}

	public void ChangeDirection(float rdir, Vector3 dir)
	{
		Direction = dir;
		RotationDirection = rdir;

	}


}

