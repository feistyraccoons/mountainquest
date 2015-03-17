using UnityEngine;
using System.Collections;

public class BoostSphere : MonoBehaviour {

	public float DamageModifier = 1.5f;
	public float VelocityModifier = 5;
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
			print (this.name);
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
		other.rigidbody2D.velocity *= VelocityModifier;

if (proj != null)
		{
			proj.m_fDamage *= DamageModifier;
		}
	}


	public void SetOwner(Player owner)
	{
		Owner = owner;
		
	}
	
}
