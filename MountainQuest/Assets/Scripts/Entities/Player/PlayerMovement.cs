using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 0.2f;
	public float jumpSpeed = 1;
	private bool onGround = true;
	private Vector2 preserveUp;

	// Use this for initialization
	void Start () {
		preserveUp = transform.up;
	}
	
	// Update is called once per frame
	void Update () {

		if (onGround && Input.GetAxis ("Vertical") > 0) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpSpeed);
		}

		rigidbody2D.velocity += Input.GetAxis ("Horizontal") * new Vector2 (1, 0);
		if (rigidbody2D.velocity.x > maxSpeed)
			rigidbody2D.velocity = new Vector2 (maxSpeed, rigidbody2D.velocity.y);
		else if (rigidbody2D.velocity.x < -maxSpeed)
			rigidbody2D.velocity = new Vector2 (-maxSpeed, rigidbody2D.velocity.y);

		transform.up = preserveUp;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform" && coll.gameObject.transform.position.y < transform.position.y)
			onGround = true;
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform")
			onGround = false;
	}
}