using UnityEngine;
using System.Collections;

public class BoostSphere : MonoBehaviour {

	public float DamageModifier = 1.5f;
	public float VelocityModifier = 1.5f;
	public float AliveTimer = 7;
	public bool isAiming = false;
	public GameObject Sphere;
	public Player Owner;
	
	void Start ()
	{

	}
	
	void Update ()
	{
	


		AliveTimer -= Time.deltaTime;
		if (AliveTimer <= 0) {
			Destroy (this.gameObject); 		
			Owner.GetComponent<Player>().RemoveBSphere();
			
		}
		
		if (AliveTimer<=2) {
			SpriteRenderer mySR = GetComponent<SpriteRenderer>();
			
			mySR.color = new Color(1,1,1,AliveTimer/2);
		}

	}
	void OnTriggerEnter2D(Collider2D other)
	{

		Projectile proj = other.GetComponent<Projectile> ();
		if (proj != null)
			proj.m_fDamage *= DamageModifier;

		if (other.rigidbody2D!=null) {
			if ((other.rigidbody2D.velocity.x < 32 && other.rigidbody2D.velocity.y < 32) && (other.rigidbody2D.velocity.x > -32 && other.rigidbody2D.velocity.y > -32)) 
				other.rigidbody2D.velocity *= VelocityModifier;	
		}

	}


	public void SetOwner(Player owner)
	{
		Owner = owner;
		
	}
	
}
