using UnityEngine;
using System.Collections;

public class MeleeAttack : Attack {

	public Sword sword;
	private GameObject attackCollider;
	private float colliderTimer = 0;
	public float colliderDuration = 0.25f;
	private bool movingRight = true;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	public override void Update () {
		attackTimer -= Time.deltaTime;
		colliderTimer -= Time.deltaTime;

			if(this.GetComponent<Movement>().rigidbody2D.velocity.x < 0)
				movingRight = false;
			else
				movingRight = true;

		if (attackTimer <= 0) {
			Debug.Log("Garrett was here.");
			attackCollider = new GameObject();
			attackCollider.AddComponent<BoxCollider2D>();

			BoxCollider2D col = attackCollider.GetComponent<BoxCollider2D>();
			col.isTrigger = true;
			attackCollider.transform.localScale = new Vector3(sword.range, this.collider2D.bounds.min.y - this.collider2D.bounds.max.y, 0.0f);

			if(movingRight)
				attackCollider.transform.position = new Vector3(this.collider2D.bounds.max.x + sword.range / 2.0f,
				                                                (this.collider2D.bounds.min.y + this.collider2D.bounds.max.y) / 2.0f,
				                                                0.0f);
			else
				attackCollider.transform.position = new Vector3(this.collider2D.bounds.min.x - sword.range / 2.0f,
				                                                (this.collider2D.bounds.min.y + this.collider2D.bounds.max.y) / 2.0f,
				                                                0.0f);

			attackTimer = attackSpeed;
			colliderTimer = colliderDuration;
		}

		if (colliderTimer <= 0) {
			GameObject.Destroy(attackCollider);
		}
	}
}