using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack : Attack {
	[HideInInspector]
	public Sword sword;
	private GameObject attackCollider;
	private float colliderTimer = 0;
	public float colliderDuration = 0.75f;
	private bool movingRight = true;


	// Use this for initialization
	void Start () {
		theWeapons = new List<Weapon>();
	}

	// Update is called once per frame
	public override void Update () {
		attackTimer -= Time.deltaTime;
		colliderTimer -= Time.deltaTime;

		if (this.CompareTag ("Player")) {
			if (rigidbody2D.velocity.x < 0)
				movingRight = false;
			else
				movingRight = true;
		} else {
			if(this.rigidbody2D.velocity.x < 0)
				movingRight = false;
			else
				movingRight = true;
		}
//
//		if (attackTimer <= 0) {
//			attackCollider = new GameObject();
//
//
//			BoxCollider2D col = attackCollider.AddComponent<BoxCollider2D>();
//			col.isTrigger = true;
//			attackCollider.transform.localScale = new Vector3(sword.range, this.collider2D.bounds.min.y - this.collider2D.bounds.max.y, 0.0f);
//
//			if(movingRight)
//				attackCollider.transform.position = new Vector3(this.collider2D.bounds.max.x + sword.range / 2.0f,
//				                                                (this.collider2D.bounds.min.y + this.collider2D.bounds.max.y) / 2.0f,
//				                                                0.0f);
//			else
//				attackCollider.transform.position = new Vector3(this.collider2D.bounds.min.x - sword.range / 2.0f,
//				                                                (this.collider2D.bounds.min.y + this.collider2D.bounds.max.y) / 2.0f,
//				                                                0.0f);
//
//			attackTimer = attackSpeed;
//			colliderTimer = colliderDuration;
//		}
//
//		if (colliderTimer <= 0) {
//			GameObject.Destroy(attackCollider);
//		}
	}
}