using UnityEngine;
using System.Collections;

public class MeleeAttack : Attack
{
	[HideInInspector]
	public Sword sword;

	void Start(){
	
		sword = GetComponentInChildren<Sword> ();
	}

	public void swing(){
		float right = GetComponent<Enemy>().facingRight?1:-1;
		sword.transform.position = rigidbody2D.position + new Vector2 (2f * right, 0);
//		sword.GetComponent<BoxCollider2D> ().enabled = true;
	}
}