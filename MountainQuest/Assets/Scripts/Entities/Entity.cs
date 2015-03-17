using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public abstract class Entity : MonoBehaviour {

	public Health health;
	public bool showHealthBar=true;
	public bool facingRight=false;
	// Use this for initialization
	public virtual void Start () {
		if (health==null) {
			Debug.LogError("Health not set");
		}
		gameObject.AddComponent<HealthBar>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (rigidbody2D.velocity.x > 0)
			facingRight = true;
		else if (rigidbody2D.velocity.x < 0)
			facingRight = false;

		if (rigidbody2D.position.x < -2 || rigidbody2D.position.y < -2 ) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		Projectile proj = coll.GetComponent<Projectile>();
		if (proj!=null) {
			health.takeDamage (proj.m_fDamage  );
		}
	}

	protected void die()
	{
		// TODO die animation, sound, points ? etcetcetc
		Destroy (this.gameObject);
	}

}
