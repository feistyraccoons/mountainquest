using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public abstract class Entity : MonoBehaviour {

	public Health health;
	public bool showHealthBar=true;
	// Use this for initialization
	void Start () {
			Debug.LogError("Start hit");
		
		health = gameObject.AddComponent<Health>();
		if (health==null) {
			Debug.LogError("Health not set");
		}
	}
	
	// Update is called once per frame
	public virtual void Update () {
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

	void die()
	{
		// TODO die animation, sound, points ? etcetcetc
		Destroy (this);
	}

}
