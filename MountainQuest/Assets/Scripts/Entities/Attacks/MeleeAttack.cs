using UnityEngine;
using System.Collections;

public class MeleeAttack : Attack
{

	public Sword sword;
	private GameObject attackCollider;

	// Use this for initialization
	void Start ()
	{
	
	}

	// Update is called once per frame
	public override void Update ()
	{

	}

	public void Swing ()
	{
		if (attackTimer <= 0) {
			if (attackCollider != null)
				Destroy (attackCollider.gameObject);
			float right = GetComponent<Enemy>().facingRight?1:-1;
			attackCollider = Instantiate (sword,rigidbody2D.position + new Vector2(1.5f*right,0) ,transform.rotation) as GameObject;
//			if(attackCollider.transform.parent.Equals(null))
//			attackCollider.transform.parent = GetComponent <Transform>();
//			
		}
	}
}